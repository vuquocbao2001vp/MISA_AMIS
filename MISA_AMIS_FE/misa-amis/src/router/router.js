import { createRouter, createWebHistory } from "vue-router";
import Employee from "../pages/Employee/PageEmployee.vue";
import PageCustomer from "../pages/Customer/PageCustomer.vue";

const misaroutes = [
  { path: "/employee", component: Employee },
  { path: "/customer", component: PageCustomer },
  { path: "/bank", component: PageCustomer },
  { path: "/cart", component: PageCustomer },
  { path: "/sale", component: PageCustomer },
  { path: "/document", component: PageCustomer },
  { path: "/stock", component: PageCustomer },
  { path: "/tool", component: PageCustomer },
  { path: "/asset", component: PageCustomer },
  { path: "/tax", component: PageCustomer },
  { path: "/price", component: PageCustomer },
  { path: "/general", component: PageCustomer },
  { path: "/budget", component: PageCustomer },
  { path: "/report", component: PageCustomer },
  { path: "/finace", component: PageCustomer },
];
const router = createRouter({
  history: createWebHistory(),
  routes: misaroutes,
});

export default router;