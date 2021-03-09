<template>
  <!-- Phần menu  -->
  <div class="navbar-menu" :class="{ isMenu: isActiveMenu }">
    <!-- Phần logo và biểu tưởng menu  -->
    <div class="logo" :class="{ isLogo: isActiveMenu }">
      <!-- Logo  -->
      <img :src="logoIcon" alt="logo" @click="onMenu" />
      <!-- Tên Page  -->
      <h3 :class="{ isTitle: isActiveMenu }">QLTS.VN</h3>
      <!-- Biểu tượng Menu  -->
      <img
        :src="menuIcon"
        alt="menu"
        class="icon-menu"
        :class="{ isIconMenu: isActiveMenu }"
        @click="offMenu"
      />
    </div>
    <!-- Phần menu bên dưới sử dụng router để điều hướng tới các page  -->
    <ul>
      <li :class="{ isIcon: isActiveMenu }">
        <img :src="computerIcon" alt="icon"/>
        <h4 :class="{ isTitle: isActiveMenu }">Tổng quan</h4>
      </li>
      <li class="drop-right" :class="{ isIcon: isActiveMenu }">
        <img :src="communityIcon" alt="icon"/>
        <h4 :class="{ isTitle: isActiveMenu }">Tài sản</h4>
      </li>
      <li class="drop-right" :class="{ isIcon: isActiveMenu }">
        <img :src="paintIcon" alt="icon"/>
        <h4 :class="{ isTitle: isActiveMenu }">Công cụ dụng cụ</h4>
      </li>
      <li class="drop-right activeMenu" :class="{ isIcon: isActiveMenu }">
        <img :src="commonIcon" alt="icon"/>
        <h4 :class="{ isTitle: isActiveMenu }">Danh mục</h4>
      </li>
      <li :class="{ isIcon: isActiveMenu }">
        <img :src="searchIcon" alt="icon"/>
        <h4 :class="{ isTitle: isActiveMenu }">Tra cứu</h4>
      </li>
      <li :class="{ isIcon: isActiveMenu }">
        <img :src="settingIcon" alt="icon"/>
        <h4 :class="{ isTitle: isActiveMenu }">Hệ thống</h4>
      </li>
      <li :class="{ isIcon: isActiveMenu }">
        <img :src="chartIcon" alt="icon"/>
        <h4 :class="{ isTitle: isActiveMenu }">Báo cáo</h4>
      </li>
    </ul>
  </div>
</template>

<script>
export default {
  // Dữ liệu sử dụng trong Component
  data() {
    return {
      // import icon svg 
      logoIcon: require("../../assets/icon/logo-qlts.svg"),
      menuIcon: require("../../assets/icon/hamburger-menu.svg"),
      computerIcon: require("../../assets/icon/computer-line.svg"),
      communityIcon: require("../../assets/icon/community-line.svg"),
      paintIcon: require("../../assets/icon/paint-brush-line.svg"),
      commonIcon: require("../../assets/icon/common.svg"),
      searchIcon: require("../../assets/icon/search-eye-line.svg"),
      settingIcon: require("../../assets/icon/settings.svg"),
      chartIcon: require("../../assets/icon/pie-chart-line.svg"),
      //
      
    };
  },
  // Lấy dữ liệu từ Store
  computed: {
    isActiveMenu() {
      return this.$store.state.isActiveMenu;
    },
  },
  // thực hiện các Action trong Store bằng cách viết chi tiết
  methods: {
    onMenu() {
      this.$store.dispatch("onMenu");
    },
    offMenu() {
      this.$store.dispatch("offMenu");
    },
  },
};
</script>

<style scoped>
.cls-1 {
  fill: none;
}
.cls-2 {
  fill: #f5f5f5;
}
.cls-3 {
  fill: none;
  stroke: #f5f5f5;
  stroke-linecap: round;
  stroke-linejoin: round;
  stroke-width: 2px;
}
/* class chinh lai css logo */
.isLogo {
  margin: 10px;
}


.isIconMenu {
  display: none;
}

/* class chinh lai css icon */
.isIcon {
  padding: 10px;
}
ul li:hover {
  background-color: #273557;
}
h4 {
  font-size: 13px;
  font-weight: 400;
  margin-left: 10px;
  padding-top: 5px;
}
/* class an title menu */
.isTitle {
  display: none;
}
</style>