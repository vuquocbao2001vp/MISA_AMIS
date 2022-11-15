import { createApp } from 'vue'
import vueClickOutsideElement from 'vue-click-outside-element'
import App from './App.vue'
import router from '../src/router/router'
import 'devextreme/dist/css/dx.light.css';
import MCheckbox from './components/base/checkbox/MCheckbox.vue'
import MToast from './components/base/toast/MToast.vue'
import MLoader from './components/base/loader/MLoader.vue'
import MRadioCheckbox from './components/base/checkbox/MRadioCheckbox.vue'
import MPopup from './components/base/popup/MPopup.vue'
import MButton from './components/base/button/MButton.vue'
import MButtonDropdown from './components/base/button/MButtonDropdown.vue'
import MTable from './components/base/table/MTable.vue'
import DxSelectBox from "devextreme-vue/select-box"
import DxTextBox from 'devextreme-vue/text-box'
import DxDateBox from 'devextreme-vue/date-box'
import store from '../src/store/store.js'
import UUID from 'vue-uuid'

const app = createApp(App);

app.component("MCheckbox", MCheckbox)
app.component("MToast", MToast)
app.component("MLoader", MLoader)
app.component("MRadioCheckbox", MRadioCheckbox)
app.component("MPopup", MPopup)
app.component("MButton", MButton)
app.component("MTable", MTable)
app.component("MButtonDropdown", MButtonDropdown)
app.component("DxSelectBox", DxSelectBox);
app.component("DxTextBox", DxTextBox);
app.component("DxDateBox", DxDateBox);

app.use(store)
app.use(UUID)
app.use(vueClickOutsideElement)
app.use(router).mount("#app");
