<template>
  <div class="header flex">
    <div class="header-title">Nhân viên</div>
    <div class="header-right">
        <div @click="hideDetail(SAVE_MODE.Add, false, {})"><m-button buttonName="Thêm mới nhân viên" buttonType="green"></m-button></div>
    </div>
  </div>
  <div class="toolbar flex">
    <div class="toolbar-right flex">
        <div class="input-search">
            <DxTextBox 
                class="control-textbox"
                placeholder="Tìm theo mã, tên nhân viên"
                value-change-event="keyup"
                @value-changed="getTextSearch"
            />
            <div class="icon-textbox flex"><div class="icon-search"></div></div>
        </div>
        <div class="toolbar-icon flex" @click="getEmployeePaging({pageSize: employeePaging.pageSize, pageNumber: 1, searchKey: searchKey})"><div class="icon-reload" title="Lấy lại dữ liệu"></div></div>
        <div class="toolbar-icon flex" @click="exportExcelFile"><div class="icon-export" title="Xuất ra Excel"></div></div>
        <div class="toolbar-icon flex"><div class="icon-setting" title="Tùy chỉnh giao diện"></div></div>
    </div>
  </div>
  <div class="grid">
    <m-table
        :employees="employees"
        :searchKey="searchKey"
        :totalRecords="employeePaging.totalRecords"
        :totalPages="employeePaging.totalPages"
        :pageSize="employeePaging.pageSize"
        :pageNumber="employeePaging.pageNumber"
        @hideDetail="hideDetail"
        @loadData="loadData"
        @showToast="showToast"
        @hideLoader="hideLoader"
    >
    </m-table>
  </div>
  <EmployeeDetail
    :isHide="isHideDetail"
    @hideDetail="hideDetail"
    @loadData="loadData"
    @showToast="showToast"
    @hideLoader="hideLoader"
    :saveMode="saveMode"
    :employeeSelected="employeeSelected"
  />
  <m-toast :isHideToast="isHideToast" :text="toastText" :type="toastType"></m-toast>
  <m-loader :isHide="isHideLoader"></m-loader>
</template>

<script>
import axios from 'axios'
import ConstApi from '../../constants/api/constApi'
import ConstEndpoint from '../../constants/api/constEndpoint'
import MTable from '@/components/base/table/MTable.vue'
import EmployeeDetail from './EmployeeDetail.vue'
import { mapMutations, mapActions, mapGetters } from "vuex";
import MISAResource from '@/constants/resources/MISAResource'
import MISAEnum from '@/constants/enums/MISAEnum'

export default {
  components: { MTable, EmployeeDetail},
  data() {
    return {
      isHideDetail: true,
      timeout: null,
      searchKey: "",
      saveMode: null,
      employeeSelected: {},
      isHideToast: true,
      toastText: "",
      toastType: "",
      isHideLoader: false,
    }
  },
  mounted(){
    this.SAVE_MODE = MISAEnum.SAVE_MODE;
    this.getEmployeePaging({pageSize: 20, pageNumber: 1});
    this.hideLoader(true);
  },
  computed: {
    ...mapGetters(["employees", "employeePaging"])
  },
  methods: {
    ...mapMutations(["setEmployeePaging"]),
    ...mapActions(["getEmployeePaging"]),

    /**
     * Ẩn hiện loader
     * Author: VQBao - 10/10/2022
     */
    hideLoader(isHide){
      this.isHideLoader = isHide;
    },

    /**
     * Ẩn hiện dialog chi tiết nhân viên
     * Author: VQBao - 11/10/2022
     */
    hideDetail(mode, isHide, emp){
      this.isHideDetail = isHide;
      this.saveMode = mode;
      this.employeeSelected = emp;
    },

    /**
     * Lấy ra từ khóa tìm kiếm sau khi nhập xong input
     * Author; VQBao - 11/10/2022
     */
    getTextSearch(data) {
      clearTimeout(this.timeout);
      var self = this;
      self.timeout = setTimeout(function () {
        self.searchKey = data.value;
      }, 500);
    },

    /**
     * Load lại dữ liệu
     * Author; VQBao - 11/10/2022
     */
    async loadData(pageSize, pageNumber, searchKey){
      this.hideLoader(false);
      await this.getEmployeePaging({pageSize: pageSize, pageNumber: pageNumber, searchKey: searchKey});
      this.hideLoader(true);
    },

    /**
     * Hiện toast mesage
     * Author: VQBao - 11/10/2022
     */
    showToast(text, type) {
      var me = this;
      me.toastType = type;
      me.toastText = text;
      me.isHideToast = false;
      setTimeout(function () {
        me.isHideToast = true;
      }, 3000);
    },

    /**
     * Xuất khẩu nhân viên
     * Author: VQBao - 12/10/2022
     */
    exportExcelFile(){
      this.hideLoader(false);
      try {
        axios({
          url: ConstApi.EmployeeApi + ConstEndpoint.EmployeeExcel,
          method: "GET",
          responseType: "blob",
        }).then((response) => {
          var fileURL = window.URL.createObjectURL(new Blob([response.data]));
          var fileLink = document.createElement("a");

          fileLink.href = fileURL;
          fileLink.setAttribute("download", "Danh_sach_nhan_vien.xlsx");
          document.body.appendChild(fileLink);
          fileLink.click();
          this.hideLoader(true);
        })
        .catch((error) => {
          this.hideLoader(true);
          this.showToast(error.response.data.errorMsg, 'error');
        })
      } catch (error) {
        this.hideLoader(true);
        this.showToast(MISAResource.TOAST.UnsuccessExport, 'error');
      }
    }
  }
}
</script>

<style scoped>
.header {
    padding: 24px 0;
}
.header-title {
    font-size: 24px;
    font-weight: 700;
    color: #111;
    font-family: Misa Font Bold;
    min-width: 125px;
}
.header-right {
  position: absolute;
  right: 10px;
}
.toolbar {
    padding: 16px 16px 16px 4px;
    height: 68px;
    background-color: #fff;
    position: relative;
    box-sizing: border-box;
}
.input-search {
    width: 240px;
    height: 32px;
    box-sizing: border-box;
    position: relative;
    margin-right: 10px;
}
.control-textbox {
    padding-right: 28px;
    width: 100% !important;
}
.icon-textbox {
  position: absolute;
  content: "";
  top: 8px;
  right: 10px;
  width: 18px;
  height: 18px;
  z-index: 1;
  justify-content: center;
}
.toolbar-icon {
    width: 24px;
    height: 24px;
    justify-content: center;
    cursor: pointer;
    margin: 0 6px;
}
.icon-reload {
    background: url(./../../assets/img/Sprites.64af8f61.svg) no-repeat -425px -201px;
	width: 21px;
	height: 23px;
}
.icon-reload:hover {
    background: url(./../../assets/img/Sprites.64af8f61.svg) no-repeat -1098px -90px;
	width: 20px;
	height: 21px;
}
.icon-export {
    background: url(./../../assets/img/Sprites.64af8f61.svg) no-repeat -705px -202px;
	width: 23px;
	height: 20px;
}
.icon-export:hover {
    background: url(./../../assets/img/Sprites.64af8f61.svg) no-repeat -1265px -90px;
	width: 23px;
	height: 20px;
}
.icon-setting {
    background: url(./../../assets/img/Sprites.64af8f61.svg) no-repeat -90px -201px;
	width: 20px;
	height: 22px;
}
.icon-setting:hover {
    background: url(./../../assets/img/Sprites.64af8f61.svg) no-repeat -33px -144px;
	width: 23px;
	height: 24px;
}
.toolbar-right {
    justify-content: center;
    position: absolute;
    right: 16px;
}
.grid {
    background-color: #fff;
    height: calc(100vh - 48px - 84px - 68px);
    box-sizing: border-box;
    padding: 1px 30px 0px 20px;
    position: relative;
}

</style>