<template>
  <div class="container-dashboard">
    <TheNavbar />
    <div class="main-dashboard">
      <TheHeader />
      <TheContent />
    </div>
  </div>
</template>

<script>
import TheNavbar from "./components/layout/TheNavbar.vue";
import TheHeader from "./components/layout/TheHeader.vue";
import TheContent from "./components/layout/TheContent.vue";
export default {
  name: "App",
  components: {
    TheNavbar,
    TheHeader,
    TheContent,
  },
};
</script>

<style>
@import url(./css/main.css);
.container-dashboard {
  width: 100%;
  height: 100vh;
  display: flex;
  flex-direction: row;
}
.main-dashboard {
  display: flex;
  flex-direction: column;
  width: calc(100% - 178px);
}
</style>
