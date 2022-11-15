<template>
  <div class="popup flex">
    <div class="popup-drag flex">
        <div class="popup-header">
            <slot name="header"></slot>
        </div>
        <div class="popup-content">
            <slot name="content"></slot>
        </div>
    </div>
  </div>
</template>

<script>
export default {
};
</script>

<style scoped>
.popup {
  position: fixed;
  justify-content: center;
  top: 0;
  left: 0;
  right: 0;
  bottom: 0;
  background-color: rgba(0, 0, 0, 0.4);
  z-index: 100;
}
.popup-drag {
    position: relative;
    flex-direction: column;
    box-sizing: border-box;
    padding: 0 32px;
    background-color: #fff;
    /* cursor: move; */
    align-items:flex-start;
}
.popup-header {
  position: relative;
}
</style>