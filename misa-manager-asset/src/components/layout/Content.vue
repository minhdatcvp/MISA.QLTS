<template>
<!-- Phần nội dung bên phải  -->
<!-- Gồm header dùng chung và các nội dung sử dụng router để điều hướng  -->
  <div class="right-page" :class="{isRightPage: isMenu}">
    <Header/>
    <!-- Phần sử dụng router  -->
    <component-list/>
  </div>
</template>

<script>
import ComponentList from '../../views/assets/ComponentList';
import Header from './Header.vue';
export default {
  components: { 
    Header,
    ComponentList
    },
  data(){
    return{
    }
  },
  computed :{
    isMenu(){
      return this.$store.state.isActiveMenu;
    }
  }
}
</script>

<style>

</style>