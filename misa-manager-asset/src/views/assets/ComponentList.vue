<template>
  <!-- Danh sách tài sản  -->
  <div class="content-list">
    <!-- Mục tìm kiếm và thêm sửa xóa item  -->
    <div class="filter-bar">
      <div class="filter-left">
        <input
          id="txtSearch"
          class="icon-search input-search"
          type="text"
          placeholder="Tìm kiếm"
          v-model="textSearch"
        />
      </div>
      <div class="filter-right">
        <button type="button" class="btn-add" @click="showForm">Thêm</button>
        <!-- Icon refresh  -->
        <img :src="refreshIcon" alt="refresh" @click="reRender" />
        <!-- Icon delete  -->
        <img :src="deleteIcon" alt="delete"/>
      </div>
    </div>
    <!-- Mục danh sách item  -->
    <div class="table-data">
      <table class="table">
        <thead>
          <tr>
            <th v-if="isCheckbox" width="1%">
              <img :src="deleteIcon" alt="delete" @click="deletesAsset" />
            </th>
            <th width="2%" class="order">STT</th>
            <th width="6%" class="posted_date">NGÀY GHI GIẢM</th>
            <th width="15%">SỐ CHỨNG TỪ</th>
            <th width="50%" style="max-width: 60%" class="out-line">
              LÝ DO GHI GIẢM
            </th>
            <th width="8%" class="cost_total">GIÁ TRỊ CÒN LẠI</th>
            <th width="5%" class="fuctionCol">CHỨC NĂNG</th>
          </tr>
        </thead>
        <tbody v-if="decrement.length > 0">
          <!-- Duyệt mảng rồi render ra các item  -->
          <!-- khi click vào item -> gán biến isActive bằng giá trị index để hiển thị active  -->
          <tr
            v-for="(item, index) in filteredRefDecrement"
            :key="index"
            :class="{ highlight: isActive == index }"
            @click="activeItem(index)"
            @dblclick="getItem(item)"
          >
            <td class="order">{{ index + 1 }}</td>
            <td class="posted_date">
              {{ format_date(item.postedDate) }}
            </td>
            <td>{{ item.refNo }}</td>
            <td>{{ item.journalMemo }}</td>
            <td class="text-alight-right">
              {{ formatPrice(item.costTotal) }}
            </td>
            <td class="fuctionCol">
              <div class="editIcon" title="Sửa" @click="getItem(item)"></div>
              <div
                class="deleteIcon"
                @click="showPopupDelete(item)"
                title="Xóa"
              ></div>
            </td>
          </tr>
        </tbody>
        <div v-if="decrement.length == 0" class="notData">
          <h1>Không có dữ liệu</h1>
        </div>
      </table>
    </div>
    <!-- Hiển thị Popup khi isPopup = true , ẩn khi isPopup = false  -->
    <transition name="slide-fade">
      <div class="popup" v-if="isPopup">
        <div class="popupNotify">
          <h3>Xác nhận</h3>
          <p>Bạn chắc chắn muốn xóa chứng từ này ?</p>
          <div class="btn-popup">
            <button class="btn-add btn-cancel" @click="offPopupDelete">
              Hủy
            </button>
            <button class="btn-add btn-del" @click="deleteItem">Xóa</button>
          </div>
        </div>
      </div>
    </transition>
    <!-- Hiển thị form khi isForm = true , ẩn khi isForm = false  -->
    <component-form v-if="isForm" :idItem="idItem" @resetItem="resetItem" />
    <footer>
      <p>Tổng số chứng từ : {{ decrement.length }}</p>
      <p class="sum-asset">Tổng giá trị còn lại: {{ formatPrice(sumPrice) }}</p>
    </footer>
    <notifications group="foo" />
  </div>
</template>

<script>
import * as axios from "axios";
import ComponentForm from "./ComponentForm.vue";
import moment from "moment";
export default {
  components: {
    ComponentForm,
  },
  data() {
    return {
      refreshIcon: require("../../assets/icon/refresh.svg"),
      deleteIcon: require("../../assets/icon/trash.svg"),
      // Dữ liệu lấy về từ api
      decrement: [],
      idItem: null, // id truyền xuống form
      isActive: 0, // lưu item đang được trỏ tới
      isCheckbox: false, // Hiển thị checkbox
      textSearch: "", // lưu trữ kí tự tìm kiếm
      idDeletes: [], // lưu id delete
      isPopup: false, // đóng mở popup thông báo xóa
    };
  },
  methods: {
    /**
     * gán giá trị isActive bằng index để khi click vào item nào chuyển active đến item đó
     */
    activeItem(index) {
      this.isActive = index;
    },
    /**
     * Reset lại bảng dữ liệu
     */
    reRender() {
      location.reload();
    },
    /**
     * Hiển thị Form
     */
    showForm() {
      this.$store.dispatch("onForm");
    },
    /**
     * Sửa lại giá trị ngày tháng rồi đẩy dữ liệu lên form edit
     */
    getItem(item) {
      this.idItem = item.refDecrementId;
      this.showForm();
    },
    /**
     * Đưa dữ liệu đẩy lên form về rỗng
     * để khi click vào insert không còn dữ liệu
     */
    resetItem() {
      this.idItem = "";
    },
    /**
     * Format lại ngày tháng năm
     */
    format_date(value) {
      if (value) {
        return moment(String(value)).format("DD-MM-YYYY");
      }
    },
    /**
     * Format giá từ dạng 1000 -> 1.000
     */
    formatPrice(value) {
      if (value != null) {
        let val = (value / 1).toFixed(3).replace(",", ".");
        return val.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");
      }
    },
    /**
     * click vào icon xóa hiện ra popup
     */
    showPopupDelete(item) {
      this.isPopup = true;
      this.itemDelete = item;
    },
    showPopupDeletes() {
      this.isPopup = true;
    },
    /**
     * click vào icon xóa hiện ra popup
     */
    offPopupDelete() {
      this.isPopup = false;
      // this.itemDelete = {};
    },
    /**
     * Xóa 1 object trong database ảo
     * Call api xóa trên database thực
     */
    deleteItem() {
      if (this.idDeletes.length == 0) {
        // console.log(this.itemDelete.refDecrementId);
        // xóa trong mảng
        this.decrement.splice(this.decrement.indexOf(this.itemDelete), 1);
        // call api xóa trên database
        let apiUrl =
          "https://localhost:44392/api/v1/RefDecrements/" +
          this.itemDelete.refDecrementId;
        axios
          .delete(apiUrl)
          .then((response) => {
            if (!response.data.success) {
              this.$notify({
                group: "foo",
                title: "Lỗi",
                text:
                  "Đã có lỗi xảy ra, vui lòng liên hệ MISA để được trợ giúp",
                type: "error",
              });
            } else {
              this.$notify({
                group: "foo",
                title: "Thành công",
                text: "Xóa tài sản thành công",
                type: "success",
              });
            }
            console.log(response);
          })
          .catch((error) => {
            this.$notify({
              group: "foo",
              title: "Lỗi",
              text: "Đã có lỗi xảy ra, vui lòng liên hệ MISA để được trợ giúp",
              type: "error",
            });
            console.log(error);
          });
      }
      this.offPopupDelete();
    },
  },
  computed: {
    /**
     * Ânr hiện form
     */
    isForm() {
      return this.$store.state.isForm;
    },
    /**
     * Tính tổng giá tiền tài sản
     */
    sumPrice() {
      var sum = 0;
      for (let i = 0; i < this.decrement.length; i++) {
        sum += this.decrement[i].costTotal;
      }
      return sum;
    },
    /**
     * Hàm lọc giá trị theo ô tìm kiếm (texsearch)
     */
    filteredRefDecrement() {
      let filterText = this.textSearch;

      return this.decrement.filter(function (item) {
        let filtered = true;
        if (filtered) {
          if (filterText && filterText.length > 0) {
            filtered =
              item.refNo.toLowerCase().includes(filterText.toLowerCase())
          }
        }
        return filtered;
      });
    },
  },
  // call api lấy toàn bộ dữ liệu tài sản
  async created() {
    /**
     * Gọi API lấy toàn bộ tài sản
     */
    const decrement = await axios.get(
      "https://localhost:44392/api/v1/RefDecrements"
    );
    this.decrement = decrement.data.data;
  },
};
</script>

<style scoped>
/*------------------------------*/
/*-------Toàn bộ nội dung------------------*/
.content-list {
  margin: 15px 20px;
  position: relative;
  height: calc(100vh - 65px);
}
/*-------Phần tìm kiếm và thêm mới-----------*/
.filter-bar {
  display: flex;
  justify-content: space-between;
  height: 50px;
}
/*-------- ô input tìm kiếm------------------- */
#txtSearch {
  width: 299px;
  height: 31px;
  padding-top: 9px;
  padding-left: 15px;
  padding-bottom: 8px;
  font-style: italic;
}
.icon-search {
  background-image: url("../../assets/search.png");
  background-repeat: no-repeat;
  background-position: 275px center;
}
/*---------Phần thêm mới , icon xóa,icon reset ---*/
.filter-right img {
  border: 1.2px solid #e0e0e0;
  color: #7b7a8d;
  padding: 8px;
  margin-left: 11px;
  border-radius: 2.5px;
}
/*-------Phần dưới dùng----------------------*/
footer {
  display: flex;
  justify-content: space-between;
  font-size: 14px;
  font-weight: 400;
  margin-top: 20px;
  height: 40px;
  width: 100%;
  background-color: white;
}
/**
Nếu không có dữu liệu
 */
.notData {
  width: 100%;
  position: absolute;
  margin-top: 20px;
  margin-left: 20px;
}
.notData h1 {
  font-size: 25px;
}
</style>