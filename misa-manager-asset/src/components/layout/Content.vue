<template>
  <!-- Phần nội dung bên phải  -->
  <!-- Gồm header dùng chung và các nội dung sử dụng router để điều hướng  -->
  <div class="right-page" :class="{ isRightPage: isMenu }">
    <Header />
    <!-- Phần sử dụng router  -->
    <!-- <loading
      :active.sync="isLoading"
      :can-cancel="false"
      :is-full-page="fullPage"
    ></loading> -->
    <component-list @reloader="reloader" :key="componentKey" />
  </div>
</template>

<script>
import Vue from "vue";
import ComponentList from "../../views/assets/ComponentList";
import Header from "./Header.vue";
import Loading from "vue-loading-overlay";
import "vue-loading-overlay/dist/vue-loading.css";
Vue.use(
  Loading,
  {
    // props
    color: "#00abfe",
    loader: "dots",
    height: 50,
    width : 50,
    backgroundColor: "rgb(204 201 201)",
  },
  {
    // slots
  }
);
export default {
  components: {
    Header,
    ComponentList
  },
  data() {
    return {
      componentKey: 0,
      isLoading: false,
      fullPage: true
    };
  },
  methods: {
    reloader() {
      let loader = this.$loading.show({
        // Optional parameters
        container: this.fullPage ? null : this.$refs.formContainer,
        canCancel: false,
      });
      // simulate AJAX
      setTimeout(() => {
        loader.hide();
        this.componentKey += 1;
      }, 2000);
    }
  },
  computed: {
    isMenu() {
      return this.$store.state.isActiveMenu;
    }
  }
};
</script>

<style>
</style>