<template>
  <div class="b-navbar">
    <div class="b-top-navbar">
      <div class="side-bar flex"><a class="b-option-navbar"></a></div>
      <a class="b-logo-navbar flex">
        <img
          src="../../assets/img/Logo_Module_TiengViet_White.66947422.svg"
          alt=""
        />
      </a>
    </div>

    <div class="b-list-navbar">
      <ul>
        <li v-for="item in navbars" :key="item">
          <router-link :to="item.routerLink" class="navbar-item" :title="item.title">
            <div class="icon-center"><div class="icon-list-navbar" :class="item.icon"></div></div>
            <span class="navbar-item-title">{{item.title}}</span>
          </router-link>
        </li>
      </ul>
    </div>
  </div>
</template>
<script>
export default {
  data() {
    return {
      navbars: [
        {
          routerLink: "/customer",
          title: "Tổng quan",
          icon: "icon-dashboard",
        },
        {
          routerLink: "/employee",
          title: "Tiền mặt",
          icon: "icon-money",
        },
        {
          routerLink: "/bank",
          title: "Tiền gửi",
          icon: "icon-bank",
        },
        {
          routerLink: "/cart",
          title: "Mua hàng",
          icon: "icon-cart",
        },
        {
          routerLink: "/sale",
          title: "Bán hàng",
          icon: "icon-sale",
        },
        {
          routerLink: "/document",
          title: "Quản lý hóa đơn",
          icon: "icon-document",
        },
        {
          routerLink: "/stock",
          title: "Kho",
          icon: "icon-stock",
        },
        {
          routerLink: "/tool",
          title: "Công cụ dụng cụ",
          icon: "icon-tool",
        },
        {
          routerLink: "/asset",
          title: "Tài sản cố định",
          icon: "icon-asset",
        },
        {
          routerLink: "/tax",
          title: "Thuế",
          icon: "icon-tax",
        },
        {
          routerLink: "/price",
          title: "Giá thành",
          icon: "icon-price",
        },
        {
          routerLink: "/general",
          title: "Tổng hợp",
          icon: "icon-general",
        },
        {
          routerLink: "/budget",
          title: "Ngân sách",
          icon: "icon-budget",
        },
        {
          routerLink: "/report",
          title: "Báo cáo",
          icon: "icon-report",
        },
        {
          routerLink: "/finace",
          title: "Phân tích tài chính",
          icon: "icon-finace",
        },
      ],
    };
  },
};
</script>
<style>
@import url(../../css/layout/navbar.css);
</style>