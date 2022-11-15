<template>
  <div class="button-dropdown button-dropdown-white flex" v-if="buttonType == 'white'">
    <slot name="left"></slot>
    <span class="button-name flex">{{buttonName}}</span>
    <div class="button-icon-right flex">
        <div v-if="!enable" class="icon-dropdown"></div>
        <div v-if="enable" class="icon-dropdown-enable"></div>
    </div>
  </div>
  <div class="button-dropdown button-dropdown-green flex" v-if="buttonType == 'green'">
    <slot name="left"></slot>
    <span class="button-name flex">{{buttonName}}</span>
    <div class="button-icon-right flex">
        <div class="separate"></div>
        <div class="icon-dropdown"></div>
    </div>
  </div>
</template>
<script>
export default {
    props: ['buttonName', 'buttonType', 'enable']
}
</script>

<style scoped>
.button-dropdown {
    height: 36px;
    width: fit-content;
    border-radius: 30px;
    cursor: pointer;
    font-weight: 600;
    margin-left: 12px;
}
.button-dropdown-white {
    border: 2px solid #3b3c3f;
    color: #111;
    padding: 0 16px;
}
.button-dropdown-white:hover {
    background-color: #d2d3d6;
}
.button-name, .button-icon-right {
    height: 100%;
}
.button-dropdown-white .button-name {
    margin-right: 8px;
}
.button-dropdown-white .icon-dropdown {
    background: url(../../../assets/img/Sprites.64af8f61.svg) no-repeat -1756px -317px;
	width: 8px;
	height: 5px;
}
.button-dropdown-green {
    color: #fff;
    background-color: #2ca01c;
    padding: 0;
}
.button-dropdown-green .button-name {
    padding: 0 16px;
}
.button-dropdown-green .button-name:hover {
    background-color: #35bf22;
    border-radius: 30px 0 0 30px;
}
.separate {
    height: 20px;
    width: 1px;
    background-color: #fff;
}
.button-dropdown-green .icon-dropdown {
    background: url(../../../assets/img/Sprites.64af8f61.svg) no-repeat -852px -365px;
	width: 8px;
	height: 5px;
    margin-left: 12px;
}
.button-dropdown-green .button-icon-right{
    padding: 0 16px 0 4px;
}
.button-dropdown-green .button-icon-right:hover{
    background-color: #35bf22;
    border-radius: 0 30px 30px 0;
}

.button-dropdown-white .icon-dropdown-enable {
    background: url(../../../assets/img/Sprites.64af8f61.svg) no-repeat -564px -365px;
	width: 8px;
	height: 5px;
}
</style>