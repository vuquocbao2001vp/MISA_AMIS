import { createStore } from 'vuex'
import employee from './modules/employee.js'

const store = createStore({
  modules: {
    employee,
  },
})

export default store