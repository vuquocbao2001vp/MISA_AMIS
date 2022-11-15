import axios from 'axios'
import ConstApi from '../../constants/api/constApi'
import ConstEndpoint from '../../constants/api/constEndpoint'

const state = {
    employees: [],
    employeePaging: {
        totalRecords: null,
        totalPages: null,
        pageSize: null,
        pageNumber: null
    },
    employeeSelected: {},
    organizations: [],
    positions: [],
}

const getters = {
    employees(state){
        return state.employees;
    },
    employeePaging(state){
        return state.employeePaging;
    },
    employeeSelected(state){
        return state.employeeSelected;
    },
    organizations(state){
        return state.organizations;
    },
    positions(state){
        return state.positions;
    }
}

const mutations = {
    /**
     * Gán dữ liệu phân trang nhân viên vào state
     * Author: VQBao - 11/10/2022
     */
    setEmployeePaging(state, data){
        state.employees = data.Data;
        state.employeePaging.totalRecords = data.TotalRecords;
        state.employeePaging.totalPages = data.TotalPages;
        state.employeePaging.pageSize = data.PageSize;
        state.employeePaging.pageNumber = data.PageNumber;
    },

    /**
     * Gán dữ liệu nhân viên theo id vào state
     * Author: VQBao - 11/10/2022
     */
    setEmployeeById(state, data){
        state.employeeSelected = data;
    },

    /**
     * Gán tất cả đơn vị vào state
     * Author: VQBao - 11/10/2022
     */
    setOrganizations(state, data){
        state.organizations = data;
    },

    /**
     * Gán tất cả phòng ban vào state
     * Author: VQBao - 11/10/2022
     */
    setPositions(state, data){
        state.positions = data;
    }
}

const actions = {
    /**
     * Lấy dữ liệu phân trang nhân viên
     * Author: VQBao - 11/10/2022
     */
    async getEmployeePaging({commit}, {pageSize, pageNumber, searchKey}){
        try {
            await axios.get(ConstApi.EmployeeApi + ConstEndpoint.Paging, {
                params: {
                    pageSize: pageSize,
                    pageNumber: pageNumber,
                    searchKey: searchKey
                }
            }).then((response) => {
                commit("setEmployeePaging", response.data)
            })
            .catch((error) => {
                console.log(error.response.data.errorMsg);
            })
        } catch (error) {
            console.log(error);
        }
    },

    /**
     * Lấy dữ liệu nhân viên theo id
     * Author: VQBao - 11/10/2022
     */
    async getEmployeeById({commit}, id){
        try {
            await axios.get(ConstApi.EmployeeApi + '/' + id)
            .then((response) => {
                commit("setEmployeeById", response.data);
            })
            .catch((error) => {
                console.log(error.response.data.errorMsg);
            })
        } catch (error) {
            console.log(error);
        }
    },


    /**
     * Lấy tất cả đơn vị
     * Author: VQBao - 11/10/2022
     */
     async getOrganizations({commit}){
        try {
            await axios.get(ConstApi.OrganizationApi)
            .then((response) => {
                commit("setOrganizations", response.data);
            })
            .catch((error) => {
                console.log(error.response.data.errorMsg);
            })
        } catch (error) {
            console.log(error);
        }
    },

    /**
     * Lấy tất cả phòng ban
     * Author: VQBao - 11/10/2022
     */
    async getPositions({commit}){
        try {
            await axios.get(ConstApi.PositionApi)
            .then((response) => {
                commit("setPositions", response.data);
            })
            .catch((error) => {
                console.log(error.response.data.errorMsg);
            })
        } catch (error) {
            console.log(error);
        }
    },
}

export default {
    state,
    getters,
    mutations,
    actions
}