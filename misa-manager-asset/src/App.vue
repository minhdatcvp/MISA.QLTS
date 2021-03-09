<template>
  <div id="app">
    <!-- giao diện gồm 2 phần menu ở bên trái và content bên phải -->
    <navbar/>
    <Content/>
  </div>
</template>

<script>
import Content from './components/layout/Content.vue'
import Navbar from './components/layout/Navbar.vue'

export default {
  name: 'App',
  // Khai báo component 
  components: {
    Navbar,
    Content
  }
}
</script>

<style>
/* import file index.css  */
@import "./styles/index.css";

</style>
