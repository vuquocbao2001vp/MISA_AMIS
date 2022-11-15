<template>
  <div class="multi-action">
    <div v-click-outside-element="closeMultiAction" @click="showMultiAction"><m-button-dropdown :enable="enableMultiAction" buttonName="Thực hiện hàng loạt" buttonType="white"></m-button-dropdown></div>
    <div @click="deleteEmployeesOnClick(DELETE_MODE.Multi)" v-show="isShowMultiAction" id="btnMultiDelete">Xóa</div>
  </div>
  <div class="content-table">
    <table>
      <thead>
        <tr>
          <th class="td-checkbox">
            <m-checkbox
              @click="checkAllCheckbox"
              :isCheck="isCheckAll"
            />
          </th>
          <th v-for="header in tableHeaders" :key="header.caption" :title="header.title" :class="header.align" :style="header.style">{{header.caption}}</th>
        </tr>
      </thead>
      <tbody>
        <tr v-for="(employee, index) in employees" :key="employee.EmployeeID" @dblclick="showEmployeeSelectedDetail(employee.EmployeeID)" :class="isCheck[index] ? 'row-selected' : ''">
          <td class="td-checkbox">
            <m-checkbox
              @click="checkCheckbox(employee,index)"
              :isCheck="isCheck[index]"
            />
          </td>
          <td>{{employee.EmployeeCode}}</td>
          <td>{{employee.FullName}}</td>
          <td>{{gender[employee.Gender - 1]}}</td>
          <td class="text-center">{{formatDate(employee.DateOfBirth)}}</td>
          <td>{{employee.IdentityNumber}}</td>
          <td>{{employee.PositionName}}</td>
          <td>{{employee.OrganizationName}}</td>
          <td>{{employee.BankAccount}}</td>
          <td>{{employee.BankName}}</td>
          <td style="border-right: 0">{{employee.BankBranch}}</td>
          <td class="td-expand">
            <div class="flex-expand flex">
              <span id="btnUpdate" @click="showEmployeeSelectedDetail(employee.EmployeeID)">Sửa</span>
              <div @click="expandFunctionRow(employee)" class="selection-expand flex"><div class="icon-expand"></div></div>
            </div>
          </td>
        </tr>
      </tbody>
    </table>
    <div class="table-paging flex">
      <div class="paging-total">Tổng số: <b>{{totalRecords}}</b> bản ghi</div>
      <div class="paging-right flex">
          <div class="paging-selection">
            <DxSelectBox
              placeholder=""
              :data-source="pageSelection"
              display-expr="selection"
              value-expr="id"
              v-model:value="dPageSize"
            />
          </div>
          <div class="paging-nav flex">
            <div class="paging-prev" :class="{'disable-text': pageNumberSelected == 1}" @click="toPrevPage">Trước</div>
            <div class="paging-number flex">
              <div class="number" :class="{'number-border': pageNumberSelected == 1}" @click="selectPageNumber(1)">1</div>
              <div v-if="totalPages > 2" class="flex"><div v-for="(pNumber, index) in pageNumberArray" :key="pNumber" class="number" :class="{'number-border': pNumber == pageNumberSelected}" @click="selectPageNumber(pNumber, index)">{{pNumber}}</div></div>
              <div v-if="totalPages > 1" class="number" :class="{'number-border': pageNumberSelected == totalPages}" @click="selectPageNumber(totalPages)">{{totalPages}}</div>
            </div>
            <div class="paging-next" :class="{'disable-text': pageNumberSelected == totalPages}" @click="toNextPage">Sau</div>
          </div>
      </div>
    </div>
  </div>
  <div
    class="detail-function-table" v-show="isExpand" :style="'top: ' + expandPosition.top + 'px; left: ' + expandPosition.left + 'px'"
  >
    <div class="detail-btn" @click="duplicateEmployee">Nhân bản</div>
    <div class="detail-btn" @click="deleteEmployeesOnClick(DELETE_MODE.Single)">Xóa</div>
    <div class="detail-btn">Ngừng sử dụng</div>
  </div>
  <m-popup v-show="showWarning">
    <template #content>
      <div class="noti-popup">
        <div class="noti-icon flex"><div class="icon-warning"></div></div>
        <span class="noti-text">{{message}}</span>
      </div>
      <div class="content-control flex">
        <div class="control-left">
          <div @click="cancelDelete"><m-button buttonName="Không" buttonType="white"></m-button></div>
        </div>
        <div class="control-right">
          <div @click="deleteEmployees(ids)"><m-button buttonName="Có" buttonType="green"></m-button></div>
        </div>
      </div>
    </template>
  </m-popup>
</template>

<script>
import axios from 'axios'
import ConstApi from '../../../constants/api/constApi'
import MISAResource from '../../../constants/resources/MISAResource'
import MCheckbox from '../checkbox/MCheckbox.vue'
import formatDate from '../../../constants/extensions/formatDate'
import MPopup from '../popup/MPopup.vue'
import { mapMutations, mapActions, mapGetters } from "vuex";
import MISAEnum from '@/constants/enums/MISAEnum'

export default {
  components: { MCheckbox, MPopup },
  props: ['employees', 'totalRecords', 'totalPages', 'pageSize', 'pageNumber', 'searchKey'],
  emits: ['hideDetail', 'loadData', 'showToast', 'hideLoader'],
  data() {
    return {
      pageSelection: [
        {'id': 10, 'selection': '10 bản ghi trên 1 trang'},
        {'id': 20, 'selection':'20 bản ghi trên 1 trang'},
        {'id': 30, 'selection':'30 bản ghi trên 1 trang'},
        {'id': 50, 'selection':'50 bản ghi trên 1 trang'},
        {'id': 100, 'selection':'100 bản ghi trên 1 trang'}
      ],
      tableHeaders: [
        {
          "caption": "MÃ NHÂN VIÊN",
          "align": "text-left",
          "style": "min-width: 154px"
        },
        {
          "caption": "TÊN NHÂN VIÊN",
          "align": "text-left",
          "style": "min-width: 212px"
        },
        {
          "caption": "GIỚI TÍNH",
          "align": "text-left",
          "style": "min-width: 115px"
        },
        {
          "caption": "NGÀY SINH",
          "align": "text-center",
          "style": "min-width: 150px"
        },
        {
          "caption": "SỐ CMND",
          "align": "text-left",
          "style": "min-width: 200px",
          "title": "Số chứng minh nhân dân"
        },
        {
          "caption": "CHỨC DANH",
          "align": "text-left",
          "style": "min-width: 250px"
        },
        {
          "caption": "TÊN ĐƠN VỊ",
          "align": "text-left",
          "style": "min-width: 250px"
        },
        {
          "caption": "SỐ TÀI KHOẢN",
          "align": "text-left",
          "style": "min-width: 150px"
        },
        {
          "caption": "TÊN NGÂN HÀNG",
          "align": "text-left",
          "style": "min-width: 250px"
        },
        {
          "caption": "CHI NHÁNH NGÂN HÀNG",
          "align": "text-left",
          "style": "min-width: 250px; border-right: 0;"
        },
        {
          "caption": "CHỨC NĂNG",
          "align": "text-center td-expand",
          "style": "min-width: 120px"
        },
      ],
      gender: ["Nam", "Nữ", ""],
      dPageSize: 20,
      isExpand: false,
      expandPosition: {},
      isCheckAll: false,
      isCheck: [],
      employeeSelectedIds: [],
      message: "",
      showWarning: false,
      enableMultiAction: false,
      isShowMultiAction: false,
      ids: null,
      pageNumberSelected: 1,
      pageNumberArray: [],
    }
  },
  mounted(){
    this.DELETE_MODE = MISAEnum.DELETE_MODE;
    // Gán giá trị các checkbox = false
    for (let i = 0; i < this.employees.length; i++) {
      this.isCheck[i] = false;
    }
    this.selectPageNumber(1);
  },
  computed: {
    ...mapGetters(["employeeSelected"])
  },
  watch: {
    // Khi prop pageSize thay đổi, gán vào data dPageSize
    pageSize: function(value){
      this.dPageSize = value;
    },
    // Khi từ khóa tìm kiếm thay đổi, load lại phân trang
    searchKey: function(value){
      this.loadPaging( this.dPageSize, 1, value);
      this.resetAllCheckbox();
    },
    // Khi số lượng bản ghi trên 1 trang thay đổi, load lại phân trang
    dPageSize: function(value){
      this.loadPaging( value, this.pageNumber, this.searchKey);
      this.resetAllCheckbox();
    },
    // Khi prop pageNumber thay đổi, gán giá trị vào data pageNumberSelected
    pageNumber: function(value){
      this.pageNumberSelected = value;
    },
    // Khi chọn số thứ tự trang hiển thị, load lại phân trang
    pageNumberSelected: function(value){
      this.loadPaging( this.dPageSize, value, this.searchKey);
      this.resetAllCheckbox();
    }
  },
  methods: {
    // Hàm định dạng lại hiển thị ngày tháng
    formatDate,

    ...mapMutations(["setEmployeeById"]),
    ...mapActions(["getEmployeeById"]),

    /**
     * Phân trang lại dữ liệu
     * Author: VQBao - 12/10/2022
     */
    loadPaging(pageSize, pageNumber, searchKey){
      this.$emit("loadData", pageSize, pageNumber, searchKey);
      this.selectPageNumber(pageNumber);
    },
    /**
     * Khi bấm vào mũi tên ở ô chức năng thì hiện tùy chọn các chức năng
     * Author: VQBao - 11/10/2022
     */
    expandFunctionRow(emp){
      if(!this.isExpand){
        this.isExpand = true;
        var pos = event.target.getBoundingClientRect();
        this.expandPosition.top = pos.top + 20;
        this.expandPosition.left = pos.left - 80;
        this.expandPosition.employeeId = emp.EmployeeID;
        this.expandPosition.employeeCode = emp.EmployeeCode;
      } else {
        this.isExpand = false;
        this.expandPosition = {};
      }
    },

    /**
     * Thực hiện chọn tất cả nhân viên
     * Author: VQBao - 11/10/2022
     */
    checkAllCheckbox(){
      this.employeeSelectedIds = [];
      if(!this.isCheckAll){
        this.isCheckAll = true;
        for (let i = 0; i < this.employees.length; i++) {
          this.isCheck[i] = true;
          this.employeeSelectedIds.push(this.employees[i].EmployeeID);
        }
        this.enableActions(true);
      } else {
        this.resetAllCheckbox();
        this.enableActions(false);
      }
    },

    /**
     * Thực hiện chọn từng nhân viên
     * Author: VQBao - 11/10/2022
     */
    checkCheckbox(employee, index){
      if(!this.isCheck[index]){
        this.isCheck[index] = true;
        this.employeeSelectedIds.push(employee.EmployeeID);
        if(this.employeeSelectedIds.length == this.employees.length){
          this.isCheckAll = true;
        }
        this.enableActions(true);
      } else {
        this.isCheck[index] = false;
        this.isCheckAll = false;
        let id = this.employeeSelectedIds.indexOf(employee.EmployeeID);
        this.employeeSelectedIds.splice(id, 1);
        if(this.employeeSelectedIds.length == 0){
          this.enableActions(false);
        }
      }
    },
    
    /**
     * Đặt lại các checkbox về giá trị false
     * Author: VQBao - 11/10/2022
     */
    resetAllCheckbox(){
      for (let i = 0; i < this.isCheck.length; i++) {
        this.isCheck[i] = false;
      }
      this.employeeSelectedIds = [];
      this.isCheckAll = false;
      this.isShowMultiAction = false;
      this.enableMultiAction = false;
    },

    /**
     * Khi chọn xóa, hiện popup cảnh báo
     * Author: VQBao - 11/10/2022
     */
    deleteEmployeesOnClick(mode){
      this.showWarning = true;
      if(mode == MISAEnum.DELETE_MODE.Single){
        this.ids = this.expandPosition.employeeId;
        this.message = MISAResource.POP_UP.SingleDelete + this.expandPosition.employeeCode +"> không?"
      } else if(mode == MISAEnum.DELETE_MODE.Multi){
        this.ids = this.employeeSelectedIds.toString();
        this.message = MISAResource.POP_UP.MuliDelete;
      }
      this.isExpand = false;
    },

    /**
     * Hủy xóa nhân viên
     * Author: VQBao - 11/10/2022
     */
    cancelDelete(){
      this.showWarning = false;
      this.message = "";
    },

    /**
     * Gọi api xóa nhân viên
     * Author: VQBao - 11/10/2022
     */
    deleteEmployees(ids){
      var me = this;
      try {
        axios.delete(ConstApi.EmployeeApi, {
          params: {ids: ids}
        })
          .then(() => {
            me.expandPosition = {};
            me.showWarning = false;
            me.loadPaging( 20, 1);
            me.resetAllCheckbox();
            me.$emit("showToast", MISAResource.TOAST.Delete, 'success')
          })
          .catch((error) => {
            me.$emit("showToast", error.response.data.errorMsg, 'error');
          })
      } catch (error) {
        console.log(error);
      }
    },

    /**
     * Gọi api lấy dữ liệu nhân viên theo id rồi binding vào form chi tiết
     * Author: VQBao - 12/10/2022
     */
    async showEmployeeSelectedDetail(id){
      await this.getEmployeeById(id)
      this.$emit("hideDetail", MISAEnum.SAVE_MODE.Edit, false, this.employeeSelected);
      this.isExpand = false;
    },

    /**
     * Thực hiện nhân bản nhân viên
     * Author: VQBao - 12/10/2022
     */
    async duplicateEmployee(){
      await this.getEmployeeById(this.expandPosition.employeeId);
      this.$emit("hideDetail", MISAEnum.SAVE_MODE.Duplicate, false, this.employeeSelected);
      this.isExpand = false;
    },

    /**
     * Thực hiện disable, enable button thực hiện hàng loạt
     * Author: VQBao - 12/10/2022
     */
    enableActions(isEnable){
      this.enableMultiAction = isEnable;
    },

    /**
     * Thực hiện đóng mở chức năng mở rộng khi click thực hiện hàng loạt
     * Author: VQBao - 12/10/2022
     */
    showMultiAction(){
      if(this.enableMultiAction && !this.isShowMultiAction){
        this.isShowMultiAction = true;
      } else if(this.enableMultiAction && this.isShowMultiAction) {
        this.isShowMultiAction = false;
      }
    },

    /**
     * Thực hiện ẩn các chức năng hàng loạt
     * Author: VQBao - 12/10/2022
     */
    closeMultiAction(){
      this.isShowMultiAction = false;
    },

    /**
     * Chuyển tới trang tiếp theo
     * Author: VQBao - 12/10/2022
     */
    toNextPage(){
      if(this.pageNumberSelected < this.totalPages){
        this.selectPageNumber(this.pageNumberSelected + 1)
      }
    },

    /**
     * Chuyển tới trang kế trước
     * Author: VQBao - 12/10/2022
     */
    toPrevPage(){
      if(this.pageNumberSelected > 1){
        this.selectPageNumber(this.pageNumberSelected - 1)
      }
    },

    /**
     * Chọn trang
     * Author: VQBao - 12/10/2022
     */
    selectPageNumber(number, index){
      if(this.totalPages < 6 && this.totalPages > 2){
        this.pageNumberSelected = number;
        this.pageNumberArray = [];
        for (let i = 2; i < this.totalPages; i++) {
          this.pageNumberArray.push(i);
        }
      }
      else {
        // Chọn số thứ tự trang
        if(number != '...'){
          if(number == 2 || number == 1){
            this.pageNumberArray= [2, 3, '...'];
          }
          else if(number == this.totalPages){
            this.pageNumberArray= ['...', number - 2, number - 1];
          }
          else if(number == this.totalPages - 1 || number == this.totalPages - 2){
            this.pageNumberArray= ['...', this.totalPages - 2, this.totalPages - 1];
          }
          else {
            this.pageNumberArray= ['...', number, number + 1, '...'];
          }
          this.pageNumberSelected = number;
        }
        // Chọn lại số trang hiển thị để người dùng lựa chọn
        else if(number == '...'){
          var stringArray = this.pageNumberArray.toString();
          var numberSelected = this.pageNumberSelected;
          if(numberSelected == 1 || numberSelected == this.totalPages || numberSelected == 2 || numberSelected == this.totalPages - 1){
            if(stringArray == "2,3,..."){
              this.pageNumberArray = ['...', this.totalPages - 2, this.totalPages - 1];
            } else {
              this.pageNumberArray= [2, 3, '...'];
            }
          }
          else {
            if(index < 1 && (this.pageNumberArray[1] == 3 || this.pageNumberArray[1] == 4)){
              this.pageNumberArray = [2, 3, '...'];
            }
            else if(index < 1 && this.pageNumberArray[1] > 4){
              this.pageNumberArray = [ '...', this.pageNumberArray[1] - 2, this.pageNumberArray[1] - 1, '...'];
            }
            else if(index > 2 && this.pageNumberArray[1] == this.totalPages - 4){
              this.pageNumberArray = ['...', this.totalPages - 2, this.totalPages - 1];
            }
            else if(index > 2 && this.pageNumberArray[1] < this.totalPages - 4){
              this.pageNumberArray = [ '...', this.pageNumberArray[1] + 2, this.pageNumberArray[1] + 3, '...'];
            }
          }
        }
      }
    }
  },
}
</script>

<style scoped>
table {
  width: 100%;
  border-spacing: 0;
  box-sizing: border-box;
}

table thead th{
  position: sticky;
  top: 0;
  height: 34px;
  background-color: #f4f5f8;
  width: 100%;
  box-sizing: border-box;
  font-weight: bold;
  z-index: 2;
}

table tbody tr {
  height: 44px;
  box-sizing: border-box;
}
table tr,
td {
  overflow: hidden;
  text-overflow: ellipsis;
  white-space: nowrap;
}
tbody tr:hover {
  background-color: #E5F3FF;
}
tbody tr:hover .td-checkbox, tbody tr:hover .td-expand {
  background-color: #E5F3FF;
}

.row-selected {
  background-color: #E5F3FF !important;
}

.row-selected .td-checkbox, .row-selected .td-expand {
  background-color: #E5F3FF;
}

table tr td,
th {
  border-bottom: 1px solid #ccc;
  border-right: 1px solid #ccc;
  padding: 0 10px;
}
tbody tr td:last-child,
thead tr th:last-child {
  border-right: none;
}
.content-table {
  padding: 0 0 68px 0;
  height: 100%;
  width: 100%;
  overflow: scroll;
  box-sizing: border-box;
}
.text-left {
    text-align: left;
}
.text-center {
    text-align: center;
}
.table-paging {
    height: 68px;
    width: calc(100% - 50px);
    padding: 0 18px;
    position: absolute;
    left: 20px;
    bottom: 10px;
    background-color: #fff;
    z-index: 10;
    box-sizing: border-box;
}
.paging-selection {
    width: 200px;
}
.paging-nav {
    margin-left: 16px;
    cursor: pointer;
}
.paging-number {
    margin: 0 13px;
}
.number {
    padding: 0.5px 5.5px;
    margin: 0 1.5px;
    cursor: pointer;
}
.paging-total {
    width: 200px;
}
.paging-right {
    position: absolute;
    right: 50px;
}
.td-expand {
  position: sticky !important;
  right: 0;
  border-left: 1px solid #ccc;
  width: 100%;
  background-color: #fff;
}
.selection-expand {
  margin-left: 15px;
  justify-content: center;
  height: 16px;
  width: 16px;
  cursor: pointer;
}
.td-checkbox {
  position: sticky !important;
  left: 0;
  z-index: 3;
  background-color: #fff;
}
th.td-checkbox, th.td-expand {
  background-color: #f4f5f8;
  z-index: 4;
}

#btnUpdate {
  color: #0075C0;
  font-weight: 600;
  cursor: pointer;
}
.flex-expand {
  width: fit-content;
  padding: 0 23px;
  position: relative;
}

.detail-function-table {
  display: flex;
  flex-direction: column;
  position: fixed;
  z-index: 20;
  background-color: #fff;
  border-radius: 2px;
  border: 1px solid #babec5;
  top: 200px;
}
.detail-btn {
  padding: 5px 10px 5px;
  cursor: pointer !important;
  height: 20px;
}
.detail-btn:hover {
  background-color: #e8e9ec;
  color: #2ca01c;
}

.noti-popup {
  margin-top: 32px;
  display: flex;
  width: 380px;
  height: 80px;
  border-bottom: 1px solid #b8bcc3;
}
.noti-icon {
  width: 48px;
  height: 48px;
  justify-content: center;
}
.noti-text {
  padding: 12px 0 0 16px;
}
.content-control {
  padding: 20px 0;
  position: relative;
}
.control-right {
  position: absolute;
  top: 20px;
  right: -10px;
}

.multi-action {
  position: absolute;
  top: -52px;
  left: 8px;
}

#btnMultiDelete {
    display: flex;
    align-items: center;
    position: absolute;
    width: 100px;
    height: 34px;
    box-sizing: border-box;
    border: 1px solid #babec5;
    border-radius: 2px;
    background-color: #fff;
    padding: 2px 12px;
    z-index: 20;
    right: 0;
    top: 38px;
    cursor: pointer;
}
#btnMultiDelete:hover {
    background-color: #eceef1;
}

.disable-text {
  color: rgb(150, 150, 150) !important;
}
.number-border {
  border: 1px solid #3b3c3f;
  font-weight: 700;
}
</style>