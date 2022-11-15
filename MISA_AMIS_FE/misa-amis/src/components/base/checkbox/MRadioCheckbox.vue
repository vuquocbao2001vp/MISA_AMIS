<template>
    <label class="container">{{title}}
        <input type="radio" v-bind:checked="isCheck" name="radio">
        <span class="checkmark"></span>
    </label>
</template>

<script>
export default {
    props: ['title', 'isCheck']
};
</script>

<style scoped>
.container {
  display: flex;
  align-items: center;
  position: relative;
  cursor: pointer;
  font-size: 13px;
  font-weight: 400;
  padding-left: 32px;
  padding-top: 2px;
  -webkit-user-select: none;
  -moz-user-select: none;
  -ms-user-select: none;
  user-select: none;
  margin-right: 20px;
}

/* Hide the browser's default radio button */
.container input {
  position: absolute;
  opacity: 0;
  cursor: pointer;
}

/* Create a custom radio button */
.checkmark {
  position: absolute;
  top: 0;
  left: 0;
  height: 18px;
  width: 18px;
  background-color: #fff;
  border-radius: 50%;
  border: 1px solid #ccc
}


/* When the radio button is checked, add a blue background */
.container input:checked ~ .checkmark {
  border-color: #2ca01c;
}

/* Create the indicator (the dot/circle - hidden when not checked) */
.checkmark:after {
  content: "";
  position: absolute;
  display: none;
}

/* Show the indicator (dot/circle) when checked */
.container input:checked ~ .checkmark:after {
  display: block;
}

/* Style the indicator (dot/circle) */
.container .checkmark:after {
    top: 4px;
	left: 4px;
	width: 10px;
	height: 10px;
	border-radius: 50%;
	background: #2ca01c;
}
</style>