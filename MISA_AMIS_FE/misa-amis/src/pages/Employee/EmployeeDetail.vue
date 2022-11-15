<template>
<div v-if="isHide == false" id="employeeDetail" :class="{'hide': isHide}">
  <m-popup>
    <template #header>
      <div class="header flex">
        <div class="header-title">Thông tin nhân viên</div>
        <div @click="checkCheckbox(1)" class="header-checkbox"><m-checkbox :isCheck="employee.IsCustomer" title="Là khách hàng" /></div>
        <div @click="checkCheckbox(2)" class="header-checkbox"><m-checkbox :isCheck="employee.IsGroup" title="Là nhà cung cấp" /></div>
      </div>
      <div class="header-right flex">
        <div class="header-icon flex"><div class="icon-help" title="Giúp (F1)"></div></div>
        <div class="header-icon flex" @click="hideDialogDetail" @keydown.esc="doShortcut"><div class="icon-close" title="Đóng (Esc)"></div></div>
      </div>
    </template>
    <template #content>
      <div class="flex">
        <div class="content-left">
          <div class="content-left-row flex">
            <div class="item">
              <div class="item-title">
                Mã <span class="require-input">*</span>
              </div>
              <div @mouseover="isShowError[0] = true" @mouseleave="isShowError[0] = false" id="employeeCodeInput" class="item-input" style="width: 150px; margin-right: 6px">
                <DxTextBox
                  v-model="employee.EmployeeCode"
                  :on-focus-out="validateEmployeeCode"
                  :on-initialized="onInitialized"
                  value-change-event="keyup"
                  max-length="20"
                />
                <div v-if="errorText[0]" class="validate-error" v-show="isShowError[0]">
                  <div class="arrow"></div>
                  <div class="error-text">{{errorText[0]}}</div>
                </div>
              </div>
            </div>
            <div class="item">
              <div class="item-title">
                Tên <span class="require-input">*</span>
              </div>
              <div @mouseover="isShowError[1] = true" @mouseleave="isShowError[1] = false" id="fullNameInput" class="item-input" style="width: 235px">
                <DxTextBox
                  v-model="employee.FullName"
                  :on-focus-out="validateFullName"
                  max-length="100"
                  value-change-event="keyup"
                />
                <div v-if="errorText[1]" class="validate-error" v-show="isShowError[1]">
                  <div class="arrow"></div>
                  <div class="error-text">{{errorText[1]}}</div>
                </div>
              </div>
            </div>
          </div>
          <div class="content-left-row flex">
            <div class="item">
              <div class="item-title">
                Đơn vị <span class="require-input">*</span>
              </div>
              <div @mouseover="isShowError[2] = true" @mouseleave="isShowError[2] = false" id="organizationInput" class="item-input custom" style="width: 392px">
                <DxSelectBox
                  :search-enabled="true"
                  v-model:value="employee.OrganizationID"
                  :on-focus-out="validateOrganization"
                  :data-source="organizations"
                  display-expr="OrganizationName"
                  value-expr="OrganizationID"
                  no-data-text="Không có dữ liệu"
                  placeholder=""
                  item-template="item"
                  value-change-event="change"
                >
                  <template #item="{ data }">
                    <div class="custom-item">
                      <div class="org-code">{{data.OrganizationCode}}</div>
                      <div class="org-name">{{data.OrganizationName}}</div>
                    </div>
                  </template>
                </DxSelectBox>
                <div v-if="errorText[2]" class="validate-error" v-show="isShowError[2]">
                  <div class="arrow"></div>
                  <div class="error-text">{{errorText[2]}}</div>
                </div>
              </div>
            </div>
          </div>
          <div class="content-left-row flex">
            <div class="item">
              <div class="item-title">Chức danh</div>
              <div class="item-input" style="width: 392px">
                <DxSelectBox
                  :search-enabled="true"
                  v-model:value="employee.PositionID"
                  :data-source="positions"
                  no-data-text="Không có dữ liệu"
                  display-expr="PositionName"
                  value-expr="PositionID"
                  placeholder=""
                  value-change-event="change"
                />
              </div>
            </div>
          </div>
        </div>
        <div class="content-right">
          <div class="content-left-row flex">
            <div class="item">
              <div class="item-title">Ngày sinh</div>
              <div @mouseover="isShowError[3] = true" @mouseleave="isShowError[3] = false" id="dobInput" class="item-input" style="width: 168px; margin-right: 16px">
                <DxDateBox
                  type="date"
                  display-format="dd/MM/yyyy"
                  :use-mask-behavior="true"
                  placeholder="DD/MM/YYYY"
                  invalidDateMessage=""
                  v-model="dateOfBirth"
                  :on-focus-out="validateDateOfBirth"
                  value-change-event="change"
                />
                <div v-if="errorText[3]" class="validate-error" v-show="isShowError[3]">
                  <div class="arrow"></div>
                  <div class="error-text">{{errorText[3]}}</div>
                </div>
              </div>
            </div>
            <div class="item">
              <div class="item-title">Giới tính</div>
              <div class="item-input gender-input" style="width: 250px">
                <div @click="checkRadio(1)"><m-radio-checkbox :isCheck="employee.Gender == Gender.Male || employee.Gender == null" title="Nam"></m-radio-checkbox></div>
                <div @click="checkRadio(2)"><m-radio-checkbox :isCheck="employee.Gender == Gender.Female" title="Nữ"></m-radio-checkbox></div>
                <div @click="checkRadio(3)"><m-radio-checkbox :isCheck="employee.Gender == Gender.Other" title="Khác"></m-radio-checkbox></div>
              </div>
            </div>
          </div>
          <div class="content-left-row flex">
            <div class="item">
              <div class="item-title" title="Số Chứng minh nhân dân">Số CMND</div>
              <div class="item-input" style="width: 245px; margin-right: 6px">
                <DxTextBox
                  placeholder=""
                  v-model="employee.IdentityNumber"
                  max-length="20"
                  value-change-event="keyup"
                />
              </div>
            </div>
            <div class="item">
              <div class="item-title">Ngày cấp</div>
              <div @mouseover="isShowError[4] = true" @mouseleave="isShowError[4] = false" id="doiInput" class="item-input" style="width: 168px">
                <DxDateBox
                  type="date"
                  display-format="dd/MM/yyyy"
                  :use-mask-behavior="true"
                  placeholder="DD/MM/YYYY"
                  invalidDateMessage=""
                  v-model="dateOfICIC"
                  :on-focus-out="validateDateOfIssuanceCIC"
                  value-change-event="change"
                />
                <div v-if="errorText[4]" class="validate-error" v-show="isShowError[4]">
                  <div class="arrow"></div>
                  <div class="error-text">{{errorText[4]}}</div>
                </div>
              </div>
            </div>
          </div>
          <div class="content-left-row flex">
            <div class="item">
              <div class="item-title">Nơi cấp</div>
              <div class="item-input" style="width: 419px">
                <DxTextBox
                  v-model="employee.PlaceOfIssuanceCIC"
                  placeholder=""
                  max-length="255"
                  value-change-event="keyup"
                />
              </div>
            </div>
          </div>
        </div>
      </div>
      <div class="content-bottom">
        <div class="content-left-row flex">
          <div class="item">
            <div class="item-title">Địa chỉ</div>
            <div class="item-input" style="width: 838px">
              <DxTextBox
                v-model="employee.Address"
                placeholder=""
                max-length="255"
                value-change-event="keyup"
              />
            </div>
          </div>
        </div>
        <div class="content-left-row flex">
          <div class="item">
            <div class="item-title" title="Điện thoại di động">ĐT di động</div>
            <div class="item-input" style="width: 200px; margin-right: 6px">
              <DxTextBox
                placeholder=""
                v-model="employee.Mobile"
                max-length="11"
                value-change-event="keyup"
              />
            </div>
          </div>
          <div class="item">
            <div class="item-title" title="Điện thoại cố định">ĐT cố định</div>
            <div class="item-input" style="width: 200px; margin-right: 6px">
              <DxTextBox
                placeholder=""
                v-model="employee.LandPhone"
                max-length="11"
                value-change-event="keyup"
              />
            </div>
          </div>
          <div class="item">
            <div class="item-title">Email</div>
            <div @mouseover="isShowError[5] = true" @mouseleave="isShowError[5] = false" id="emailInput" class="item-input" style="width: 200px">
              <DxTextBox
                placeholder=""
                v-model="employee.Email"
                :on-focus-out="validateEmail"
                max-length="100"
                value-change-event="keyup"
              />
              <div v-if="errorText[5]" class="validate-error" v-show="isShowError[5]">
                <div class="arrow"></div>
                <div class="error-text">{{errorText[5]}}</div>
              </div>
            </div>
          </div>
        </div>
        <div class="content-left-row flex">
          <div class="item">
            <div class="item-title">Tài khoản ngân hàng</div>
            <div class="item-input" style="width: 200px; margin-right: 6px">
              <DxTextBox
                placeholder=""
                v-model="employee.BankAccount"
                max-length="20"
                value-change-event="keyup"
              />
            </div>
          </div>
          <div class="item">
            <div class="item-title">Tên ngân hàng</div>
            <div class="item-input" style="width: 200px; margin-right: 6px">
              <DxTextBox
                placeholder=""
                v-model="employee.BankName"
                max-length="255"
                value-change-event="keyup"
              />
            </div>
          </div>
          <div class="item">
            <div class="item-title">Chi nhánh</div>
            <div class="item-input" style="width: 200px">
              <DxTextBox
                placeholder=""
                v-model="employee.BankBranch"
                max-length="255"
                value-change-event="keyup"
              />
            </div>
          </div>
        </div>
      </div>
      <div class="content-control flex">
        <div class="control-left" @click="hideDialogDetail">
          <m-button buttonName="Hủy" buttonType="white"></m-button>
        </div>
        <div class="control-right flex">
          <div @click="saveOnClick" @keydown.ctrl.s.prevent.stop="doShortcut" title="Cất (Ctrl + S)"><m-button buttonName="Cất" buttonType="white"></m-button></div>
          <div @click="refillFormOnClick" @keydown.ctrl.s.prevent.stop="doShortcut" title="Cất và thêm (Ctrl + Shift + S)"><m-button buttonName="Cất và thêm" buttonType="green"></m-button></div>
        </div>
      </div>
    </template>
  </m-popup>
  <m-popup v-show="showErrorPopup">
    <template #content>
      <div class="noti-popup">
        <div class="noti-icon flex"><div class="icon-error"></div></div>
        <div class="noti-text">
          <div v-for="errtext in errorText" :key="errtext" v-show="errtext" class="error-text-line">{{errtext}}</div>
        </div>
      </div>
      <div class="popup-content-control flex">
        <div @click="closePopup"><m-button buttonName="Đóng" buttonType="green"></m-button></div>
      </div>
    </template>
  </m-popup>
  <m-popup v-show="showInfoPopup">
    <template #content>
      <div class="noti-popup">
        <div class="noti-icon flex"><div class="icon-info"></div></div>
        <span class="noti-info-text"><div>{{POP_UP.ChangeData}}</div></span>
      </div>
      <div class="popup-info-control flex">
        <div class="control-left">
          <div @click="closePopup"><m-button buttonName="Hủy" buttonType="white"></m-button></div>
        </div>
        <div class="control-right flex">
          <div @click="cancelSaveAction"><m-button buttonName="Không" buttonType="white"></m-button></div>
          <div @click="confirmSaveAction"><m-button buttonName="Có" buttonType="green"></m-button></div>
        </div>
      </div>
    </template>
  </m-popup>
  <m-popup v-show="showDuplicatePopup">
    <template #content>
      <div class="noti-popup">
        <div class="noti-icon flex"><div class="icon-warning"></div></div>
        <span class="noti-info-text"><div>Mã nhân viên &lt;{{employee.EmployeeCode}}&gt; đã tồn tại trong hệ thống, vui lòng kiểm tra lại.</div></span>
      </div>
      <div class="popup-info-control flex">
        <div class="control-right"><div @click="closePopup"><m-button buttonName="Đồng ý" buttonType="green"></m-button></div></div>
      </div>
    </template>
  </m-popup>
</div>
</template>

<script>
import axios from 'axios'
import ConstApi from '../../constants/api/constApi'
import ConstEndpoint from '../../constants/api/constEndpoint'
import compareTwoObjects from '../../constants/extensions/compareTwoObjects'
import MPopup from "@/components/base/popup/MPopup.vue";
import MCheckbox from "@/components/base/checkbox/MCheckbox.vue";
import MRadioCheckbox from '@/components/base/checkbox/MRadioCheckbox.vue';
import MButton from '@/components/base/button/MButton.vue';
import { mapMutations, mapActions, mapGetters } from "vuex";
import MISAResource from '../../constants/resources/MISAResource'
import MISAEnum from '@/constants/enums/MISAEnum'

export default {
  components: { MPopup, MCheckbox, MRadioCheckbox, MButton },
  props: ['isHide', 'saveMode', 'employeeSelected'],
  data() {
    return {
      employee: {},
      isCustomer: false,
      isGroup: false,
      isShowError: [],
      errorText: [],
      isValid: true,
      elementFocus: null,
      isSuccess: false,
      addMode: null,
      showErrorPopup: false,
      showInfoPopup: false,
      showDuplicatePopup: false,
      dateOfBirth: null,
      dateOfICIC: null,
      oldEmployee: {},
      axiosMethod: null,
      toastText: null,
    }
  },
  computed: {
    ...mapGetters(["organizations", "positions"])
  },
  mounted(){
    this.Gender = MISAEnum.Gender;
    this.POP_UP = MISAResource.POP_UP;
    this.getOrganizations();
    this.getPositions();
    // tạo sự kiện sử dụng phím tắt
    document.addEventListener("keydown", this.doShortcut);
  },
  beforeUnmount() {
    // hủy sự kiện sử dụng phím tắt
    document.removeEventListener("keydown", this.doShortcut);
  },
  watch: {
    // Khi nhân viên được chọn thay đổi, gán giá trị vào data employee
    employeeSelected: function(value){
      this.employee = value;
      this.oldEmployee = {...value};
      if(value){
        this.dateOfBirth = value.DateOfBirth;
        this.dateOfICIC = value.DateOfIssuanceCIC;
      }
    },
    // Mỗi khi mở popup detail, nếu là thêm mới hoặc nhân bản, lấy mã nhân viên mới
    isHide: function(value){
      if(!value && this.saveMode == MISAEnum.SAVE_MODE.Add){
        this.getNewEmployeeCode();
        this.employee.Gender = MISAEnum.Gender.Male;
      } else if(!value && this.saveMode == MISAEnum.SAVE_MODE.Duplicate){
        this.getNewEmployeeCode();
      }
      this.errorText = [],
      this.isShowError = [],
      this.isSuccess = false;
    },
    // Gán giá trị của prop saveMode vào data addMode
    saveMode: function(value){
      this.addMode = value;
    }
  },
  methods: {
    // Hàm kiểm tra thay đổi dữ liệu hay chưa
    compareTwoObjects,

    ...mapMutations([ "setOrganizations", "setPositions"]),
    ...mapActions([ "getOrganizations", "getPositions"]),

    /**
     * Sử dụng phím tắt để thực hiện nhanh các chức năng
     * Author: VQBao - 23/10/2022
     */
    doShortcut(e) {
      // Nếu key = ESC, đóng detail
      if (e.keyCode === 27) {
        this.hideDialogDetail();
      }
      // Khi sử dụng Ctrl + S thì thực hiện cất dữ liệu
      else if(e.keyCode === 83 && e.ctrlKey && !e.shiftKey){
        this.saveOnClick();
        e.preventDefault();
      }
      // Khi sử dụng Ctrl + Shift + S thì thực hiện cất và thêm mới
      else if(e.keyCode === 83 && e.ctrlKey && e.shiftKey){
        this.refillFormOnClick();
        e.preventDefault();
      }
    },

    /**
     * Khi click tắt form thông tin chi tiết, hiện popup info
     * Author: VQBao - 12/10/2022
     */
    hideDialogDetail(){
      this.employee.DateOfBirth = this.dateOfBirth;
      this.employee.DateOfIssuanceCIC = this.dateOfICIC;
      // So sánh object nhân viên cũ và mới, nếu thay đổi thì hiện popup thay đổi dữ liệu
      if(!this.compareTwoObjects(this.oldEmployee, this.employee)){
        this.showInfoPopup = true;
      } else {
        this.$emit("hideDetail", null, true);
      }
    },

    /**
     * Khi chọn không ở popup info, tắt form thông tin chi tiết
     * Author: VQBao - 12/10/2022
     */
    cancelSaveAction(){
      this.showInfoPopup = false;
      this.$emit("hideDetail", null, true);
    },

    /**
     * Khi chọn có ở popup info, tắt popup rồi thực hiện cất
     * Author: VQBao - 12/10/2022
     */
    confirmSaveAction(){
      this.showInfoPopup = false;
      this.saveOnClick();
    },
    
    /**
     * focus vào ô input đầu tiên mỗi khi mở form chi tiết
     * Author: VQBao - 12/10/2022
     */
    onInitialized: function (e) {
      this.elementFocus = e;
      setTimeout(function () {
        e.component.focus();  
      }, 0);  
    },

    /**
     * Khi bấm cất, thêm nhân viên rồi tắt form
     * Author: VQBao - 12/10/2022
     */
    async saveOnClick(){
      await this.addEmployee();
      if(this.isSuccess){
        this.$emit("hideDetail", null, true);
      }
    },

    /**
     * Gọi api thêm, sửa nhân viên
     * Author: VQBao - 12/10/2022
     */
    async addEmployee(){
      var me = this;
      me.prepareToAddEmployee();
      me.validateEmployee();
      var modifiedDate = new Date();
      me.employee.ModifiedDate = modifiedDate;
      if(me.isValid){
        var list = [];
        list.push(me.employee);
        try {
          await axios({
            method: this.axiosMethod,
            url: ConstApi.EmployeeApi,
            data: list
          })
            .then(() => {
              // Khi thực hiện thành công, hiện toast và load lại dữ liệu
              me.isSuccess = true;
              me.employee = {};
              me.$emit("loadData", 20, 1, "");
              me.$emit("showToast", me.toastText, 'success')
              me.getNewEmployeeCode();
              me.addMode = MISAEnum.SAVE_MODE.Add;
            }).catch((error) => {
              me.$emit("hideLoader", true);
              // Nếu response trả về có lỗi trùng mã nhân viên, hiện popup cảnh báo, ko thì hiện toast thông báo lỗi
              if(error.response.data.errorMsg[0] == MISAResource.ERROR.DuplicatedCodeResponse){
                let el = document.getElementById("employeeCodeInput");
                el.classList.add("check-border");
                me.errorText[0] = MISAResource.ERROR.DuplicatedCode;
                me.showDuplicatePopup = true;
              } else {
                me.$emit("showToast", error.response.data.errorMsg, 'error');
                me.$emit("hideDetail", null, true);
              }
            })
        } catch (error) {
          me.$emit("hideLoader", true);
          console.log(error);
        }
      }
    },

    /**
     * Gán phương thức axios và toast theo addMode
     * Author: VQBao - 12/10/2022
     */
    prepareToAddEmployee(){
      var newGuid = this.$uuid.v4();
      if(this.addMode == MISAEnum.SAVE_MODE.Add){
        this.employee.EmployeeID = newGuid;
        this.axiosMethod = 'post';
        this.toastText = MISAResource.TOAST.Add;
      } else if(this.addMode == MISAEnum.SAVE_MODE.Edit){
        this.axiosMethod = 'put';
        this.toastText = MISAResource.TOAST.Edit;
      } else if(this.addMode == MISAEnum.SAVE_MODE.Duplicate){
        this.employee.EmployeeID = newGuid;
        this.axiosMethod = 'post';
        this.toastText = MISAResource.TOAST.Duplicate;
      }
    },

    /**
     * Khi bấm cất và thêm, thực hiên cất rồi reset form để tiếp tục thêm mới
     * Author: VQBao - 12/10/2022
     */
    async refillFormOnClick(){
      await this.addEmployee();
      this.isSuccess = false;
      this.onInitialized(this.elementFocus);
    },

    /**
     * Check các check box là khách hàng và là nhà cung cấp, 1-true, 0-false
     * Author: VQBao - 12/10/2022
     */
    checkCheckbox(check){
      if(check == 1){
        if(this.employee.IsCustomer == 1){
          this.employee.IsCustomer = 0;
        } else {
          this.employee.IsCustomer = 1;
        }
      } else if(check == 2) {
        if(this.employee.IsGroup == 1){
          this.employee.IsGroup = 0;
        } else {
          this.employee.IsGroup = 1;
        }
      }
    },

    /**
     * Check radio giới tính
     * 1: Nam, 2: Nữ
     * Author: VQBao - 12/10/2022
     */
    checkRadio(check){
      this.employee.Gender = check;
    },

    /**
     * Thực hiện validate nhân viên khi bấm cất
     * Author: VQBao - 12/10/2022
     */
    validateEmployee(){
      this.isValid = true;
      this.validateEmployeeCode();
      this.validateFullName();
      this.validateOrganization();
      this.validateDateOfBirth();
      this.validateDateOfIssuanceCIC();
      this.validateEmail();
      for (let i = 0; i < this.errorText.length; i++) {
        if(this.errorText[i]){
          this.isValid = false;
          break;
        }
      }
      if(!this.isValid){
        this.showErrorPopup = true;
      }
    },

    /**
     * Validate mã nhân viên
     * Author: VQBao - 12/10/2022
     */
    validateEmployeeCode(){
      this.validateEmpty("employeeCodeInput", this.employee.EmployeeCode, 0, MISAResource.ERROR.EmptyCode)
    },

    /**
     * Validate tên nhân viên
     * Author: VQBao - 12/10/2022
     */
    validateFullName(){
      this.validateEmpty("fullNameInput", this.employee.FullName, 1, MISAResource.ERROR.EmptyFullName)
    },

    /**
     * Validate tên đơn vị
     * Author: VQBao - 12/10/2022
     */
    validateOrganization(){
      this.validateEmpty("organizationInput", this.employee.OrganizationID, 2, MISAResource.ERROR.EmptyOrganization)
    },

    /**
     * Check các trường thông tin trống
     * Author: VQBao - 12/10/2022
     */
    validateEmpty(elementId, value, errorIndex, error){
      let el = document.getElementById(elementId);
      if(!value){
        el.classList.add("check-border");
        this.errorText[errorIndex] = error;
      } else {
        el.classList.remove("check-border");
        this.errorText[errorIndex] = "";
      }
    },

    /**
     * Validate email đúng định dạng
     * Author: VQBao - 12/10/2022
     */
    validateEmail(){
      let el = document.getElementById("emailInput");
      if(this.employee.Email){
        var emailFormat = /^([a-zA-Z0-9_\-.]+)@([a-zA-Z0-9_\-.]+)\.([a-zA-Z]{2,5})$/;
        if (!emailFormat.test(this.employee.Email)) {
          el.classList.add("check-border");
          this.errorText[5] = MISAResource.ERROR.ValidEmail
        } else {
          el.classList.remove("check-border");
          this.errorText[5] = "";
        }
      } else {
        el.classList.remove("check-border");
        this.errorText[5] = "";
      }
    },

    /**
     * Validate ngày sinh lớn hơn ngày hiện tại
     * Author: VQBao - 12/10/2022
     */
    validateDateOfBirth(){
      this.validateDate("dobInput", this.dateOfBirth, 3, MISAResource.ERROR.ValidDateOfBirth);
    },

    /**
     * Validate ngày cấp không lớn hơn ngày hiện tại
     * Author: VQBao - 12/10/2022
     */
    validateDateOfIssuanceCIC(){
      this.validateDate("doiInput", this.dateOfICIC, 4, MISAResource.ERROR.ValidDateOfIssuanceCIC);
    },

    /**
     * Kiểm tra ngày chọn có lớn hơn ngày hiện tại hay không
     * Author: VQBao - 12/10/2022
     */
    validateDate(elementId, value, errorIndex, error){
      let el = document.getElementById(elementId);
      if(value){
        let today = new Date();
        today = this.convertDate(today);
        if(errorIndex == 3) this.employee.DateOfBirth = this.convertDate(value);
        else if(errorIndex == 4) this.employee.DateOfIssuanceCIC = this.convertDate(value);
        if(this.convertDate(value) > today){
          el.classList.add("check-border");
          this.errorText[errorIndex] = error;
        } else {
          el.classList.remove("check-border");
          this.errorText[errorIndex] = "";
        }
      } else {
        el.classList.remove("check-border");
        this.errorText[errorIndex] = "";
      }
    },

    /**
     * Lấy mã nhân viên mới
     * Author: VQBao - 11/10/2022
     */
    getNewEmployeeCode(){
      var me = this;
      try {
        axios.get(ConstApi.EmployeeApi + ConstEndpoint.NewEmployeeCode)
        .then((response) => {
          if(!me.isHide){
            me.employee.EmployeeCode = response.data;
            if(me.addMode == MISAEnum.SAVE_MODE.Add){
              me.employee.Gender = MISAEnum.Gender.Male;
            }
          }
        })
        .catch((error) => {
          me.$emit("showToast", error.response.data.errorMsg, 'error');
        })
      } catch (error) {
        console.log(error);
      }
    },

    /**
     * Đóng các popup
     * Author: VQBao - 11/10/2022
     */
    closePopup(){
      this.showErrorPopup = false;
      this.showInfoPopup = false;
      this.showDuplicatePopup = false;
      this.onInitialized(this.elementFocus);
    },
    /**
     * Chuyển date về cùng định dạng
     * Author: VQBao - 11/10/2022
     */
    convertDate(date){
      let newDate = new Date(date);
      if (date && newDate instanceof Date && !isNaN(newDate.valueOf())) {
        let _day = newDate.getDate() + 1;
        let _month = newDate.getMonth();
        let _year = newDate.getFullYear();
        newDate = new Date(_year, _month, _day);
      }
      return newDate;
    },
  }
};
</script>
<style scoped>
.hide {
  display: none !important;
}
.header {
  width: 836px;
  height: 73px;
  box-sizing: border-box;
}
.header-title {
  font-size: 24px;
  color: #111;
  font-weight: 700;
  height: 33px;
}
.header-checkbox {
  padding: 0 20px;
}
.require-input {
  color: #ff0000;
}
.content-left {
  width: 418px;
  padding: 0 26px 0 0;
  box-sizing: border-box;
}
.item-title {
  height: 21px;
  font-size: 12px;
  font-weight: 700;
}
.content-left-row {
  width: 392px;
  height: 65px;
  box-sizing: border-box;
  padding-bottom: 10px;
}
.content-bottom {
  width: 836px;
  padding-bottom: 24px;
  box-sizing: border-box;
  margin-top: 24px;
  border-bottom: 1px solid #c9ccd2;
}
.content-control {
  padding: 20px 0;
  width: 836px;
  position: relative;
}
.control-right {
  position: absolute;
  top: 20px;
  right: -10px;
}
.header-right {
  position: absolute;
  top: 12px;
  right: -10px;
}
.header-icon {
  width: 24px;
  height: 24px;
  justify-content: center;
  margin-left: 6px;
  cursor: pointer;
}
.check-border {
  box-sizing: border-box;
  border: 1px solid red;
  border-radius: 2px;
}
.check-border .dx-texteditor.dx-editor-outlined{
  border: none !important;
}
.gender-input {
  display: flex;
  border: none;
}

.custom-item {
  display: flex;
}
.org-code{
  width: 100px;
  margin-right: 6px;
}
.validate-error {
  position: absolute;
  display: flex;
  flex-direction: column;
  justify-content: center;
  align-items: center;
  margin: auto;
  left: 0;
  right: 0;
  text-align: center;
}
.arrow {
  width: 8px;
  height: 8px;
  background-color: #f34a4a;
  transform: rotate(45deg);
}
.error-text {
  background-color: #f34a4a;
  color: #fff;
  border-radius: 4px;
  height: 24px;
  width: max-content;
  display: flex;
  align-items: center;
  padding: 0 6px;
  margin-top: -4px;
}
.item-input {
  position: relative;
}
.noti-popup {
  margin-top: 32px;
  display: flex;
  width: 380px;
  min-height: 70px;
  border-bottom: 1px solid #b8bcc3;
  box-sizing: border-box;
}
.noti-icon {
  width: 48px;
  height: 48px;
  justify-content: center;
}
.noti-text {
  padding: 4px 0 16px 16px;
  display: flex;
  flex-direction: column;
  justify-content: center;
}
.error-text-line{
  height: 24px;
  display: flex;
  align-items: center;
}
.popup-content-control {
  padding: 20px 0;
  position: relative;
  justify-content: center;
}

.noti-info-text {
  padding: 12px 0 0 16px;
}
.popup-info-control {
  padding: 20px 0;
  position: relative;
  height: 74px;
  box-sizing: border-box;
}

</style>