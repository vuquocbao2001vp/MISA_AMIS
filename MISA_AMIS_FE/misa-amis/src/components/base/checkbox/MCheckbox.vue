<template>
<div class="flex">
    <div class="checkbox-container">
        <input type="checkbox" v-bind:checked="isCheck">
        <span class="checkmark"><span class="icon-checkbox"></span></span>
    </div>
    <div v-show="title != null && title != ''" class="checkbox-title flex">{{title}}</div>
</div>
    
</template>

<script>
export default {
    props: ['isCheck', 'title'],
}
</script>

<style scoped>
.checkbox-container {
    display: block;
    position: relative;
    cursor: pointer;
    height: 18px;
    width: 18px;
    box-sizing: border-box;
}
.checkbox-container input {
    top: 0;
    left: 0;
    position: absolute;
    opacity: 0;
    cursor: pointer;
    z-index: 20;
}
.checkmark {
    position: absolute;
    top: 0;
    left: 0;
    width: 18px;
    height: 18px;
    border-radius: 2px;
    border: 1px solid #afafaf;
    transition: all .2s ease;
    transform: rotate(-90deg);
    background-color: #fff;
    z-index: 10;
    display: flex;
    align-items: center;
    justify-content: center;
}
.checkbox-container input:checked ~ .checkmark {
    border-color: #2ca01c;
    transform: rotate(0deg);
    background-color: #fff;
}
.checkbox-container input:checked ~ .checkmark .icon-checkbox {
    background: url(../../../assets/img/Sprites.64af8f61.svg) no-repeat -1225px -363px;
    width: 14px;
    height: 11px;
}
.checkmark:after {
  content: "";
  position: absolute;
  display: none;
}
.checkbox-container input:checked ~ .checkmark:after {
  display: block;
}
.checkbox-title{
    margin-left: 10px;
    height: 20px;
    padding-top: 4px;
}
</style>