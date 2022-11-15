<template>
  <div class="toast-message" :class="{'hide-toast': isHideToast}">
    <div v-if="type == 'success'" class="toast-icon"><div class="icon-toast-success"></div></div>
    <div v-if="type == 'error'" class="toast-icon"><div class="icon-toast-error"></div></div>
    <div class="toast-text">
      <div class="toast-text-content">{{text}}</div>
    </div>
  </div>
</template>
<script>
export default {
  props: ['isHideToast', 'text', 'type']
};
</script>
<style scoped>
.hide-toast {
  display: none !important;
}

.toast-message {
  position: fixed;
  top: 66px;
  right: 22px;
  height: 48px;
  display: flex;
  align-items: center;
  border-radius: 4px;
  border-left: 4px solid;
  border-left-color: #2ca01c;
  box-shadow: 1px 0.5px 1px 1px rgb(200, 200, 200);
  box-sizing: border-box;
  background-color: #fff;
  transition: 0.4s;
}

.toast-icon {
  width: 40px;
  height: 40px;
  margin: 0 12px;
  display: flex;
  align-items: center;
  justify-content: center;
}

.toast-text {
  display: flex;
  height: 40px;
  width: max-content;
  align-items: center;
  padding-right: 16px;
}

.toast-text-content {
  font-size: 14px;
}

</style>