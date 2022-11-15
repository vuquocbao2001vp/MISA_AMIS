<template>
  <div class="button button-white flex" v-if="buttonType == 'white'" :title="title">
    <slot name="left"></slot>
    <span>{{buttonName}}</span>
    <slot name="right"></slot>
  </div>
  <div class="button button-green flex" v-if="buttonType == 'green'" :title="title">
    <slot name="left"></slot>
    <span>{{buttonName}}</span>
    <slot name="right"></slot>
  </div>
</template>
<script>
export default {
    props: ['buttonName', 'buttonType', 'title']
}
</script>

<style scoped>
.button {
    justify-content: center;
    height: 34px;
    padding: 6px 16px;
    border-radius: 3px;
    width: fit-content;
    cursor: pointer;
    margin-right: 10px;
    font-weight: 600;
}
.button-white {
    border: 1px solid #8d9096;
    background-color: #fff;
    color: #111;
}
.button-white:hover {
    background-color: #d2d3d6;
}
.button-green {
    border: 1px solid transparent;
    background-color: #2ca01c;
    color: #FFFFFF;
}
.button-green:hover {
    background-color: #35bf22;
}
</style>