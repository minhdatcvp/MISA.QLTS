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
        <button type="button" class="btn-add"  @click="showForm">Thêm</button>
        <!-- Icon refresh  -->
        <img :src="refreshIcon" alt="refresh" @click="reRender" />
        <!-- Icon delete  -->
        <img :src="deleteIcon" alt="delete" @click="isdeleteAllItem"/>
      </div>
    </div>
    <!-- Mục danh sách item  -->
    <div class="table-data">
      <table class="table">
        <thead>
          <tr>
            <th v-if="isCheckbox" width="1%" >
              <img :src="deleteIcon" alt="delete" @click="deletesAsset" />
            </th>
            <th width="2%" class="order">STT</th>
            <th width="6%" class="posted_date">NGÀY GHI GIẢM</th>
            <th width="15%">SỐ CHỨNG TỪ</th>
            <th width="50%">LÝ DO GHI GIẢM</th>
            <th width="8%" class="cost_total">GIÁ TRỊ CÒN LẠI</th>
            <th  width="5%" class="fuctionCol">CHỨC NĂNG</th>
          </tr>
        </thead>
        <tbody v-if="decrement.length > 0">
          <!-- Duyệt mảng rồi render ra các item  -->
          <!-- khi click vào item -> gán biến isActive bằng giá trị index để hiển thị active  -->
          <tr
            v-for="(item, index) in decrement"
            :key="index"
            :class="{ highlight: isActive == index }"
            @click="activeItem(index)"
          >
            <td v-if="isCheckbox" class="check-box">
              <input
                type="checkbox"
                id="vehicle1"
                name="vehicle1"
                :value="item"
                v-model="idDeletes"
              />
            </td>
            <td class="order">{{ index + 1 }}</td>
            <td class="posted_date">
              {{ format_date(item.posted_date) }}
            </td>
            <td>{{ item.ref_no }}</td>
            <td>{{ item.journal_memo }}</td>
            <td class="cost_total">
              {{ formatPrice(item.cost_total) }}
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
            <button class="btn-add btn-del">Xóa</button>
            <!-- @click="deleteItem" -->
          </div>
        </div>
      </div>
    </transition>
    <!-- Hiển thị form khi isForm = true , ẩn khi isForm = false  -->
    <component-form
      v-if="isForm"
      :idItem="idItem"
      @resetItem="resetItem"
    />
    <!-- :dataAsset="assets"
      :dataAssetTypes="assetTypes"
      :dataDepartments="departments"
      :itemTemp="itemTemp"
      @resetItem="resetItem" -->
    <!-- Mục thống kê tổng  -->
    <footer>
      <p>Tổng số tài sản : {{ decrement.length }}</p>
      <p class="sum-asset">Tổng nguyên giá: {{ formatPrice(sumPrice) }}</p>
    </footer>
    <notifications group="foo" />
  </div>
</template>

<script>
import ComponentForm from "./ComponentForm.vue";
import moment from "moment";
export default {
  components: {
    ComponentForm
  },
  data() {
    return {
      refreshIcon: require("../../assets/icon/refresh.svg"),
      deleteIcon: require("../../assets/icon/trash.svg"),
      // Dữ liệu lấy về từ api
      decrement: [
        {
          ref_decrement_id: "1",
          organization_id: "Thuộc đơn vị",
          ref_no: "Số chứng từ",
          ref_type: 0,
          ref_date: "2021-03-06",
          ref_detail: "Chi tiết danh sách tài sản của chứng từ",
          journal_memo: "Diễn giải",
          posted_date: "2021-03-06",
          tracked_year: "2021-03-06",
          cost_total: 2000000,
          created_by: "Người tạo",
          created_date: "Ngày tạo",
          modified_by: "Người sửa",
          modified_date: "Ngày sửa"
        },
        {
          ref_decrement_id: "2",
          organization_id: "Thuộc đơn vị",
          ref_no: "Số chứng từ",
          ref_type: 0,
          ref_date: "2021-03-06",
          ref_detail: "Chi tiết danh sách tài sản của chứng từ",
          journal_memo: "Diễn giải",
          posted_date: "2021-03-06",
          tracked_year: "2021-03-06",
          cost_total: 2000000,
          created_by: "Người tạo",
          created_date: "Ngày tạo",
          modified_by: "Người sửa",
          modified_date: "Ngày sửa"
        },
        {
          ref_decrement_id: "3",
          organization_id: "Thuộc đơn vị",
          ref_no: "Số chứng từ",
          ref_type: 0,
          ref_date: "2021-03-06",
          ref_detail: "Chi tiết danh sách tài sản của chứng từ",
          journal_memo: "Diễn giải",
          posted_date: "2021-03-06",
          tracked_year: "2021-03-06",
          cost_total: 2000000,
          created_by: "Người tạo",
          created_date: "Ngày tạo",
          modified_by: "Người sửa",
          modified_date: "Ngày sửa"
        }
      ],
      idItem:"", // id truyền xuống form
      isActive: 0, // lưu item đang được trỏ tới
      //   componentKey: 0, // Biến refresh table
      isCheckbox: false, // Hiển thị checkbox
      //   itemTemp: {}, // Data 1 tài sản đẩy lên form
      textSearch: "", // lưu trữ kí tự tìm kiếm
      idDeletes: [], // lưu id delete
      //   departmentFilter: "", // id lọc phòng ban
      //  decrementTypeFilter: "", // id lọc loại
      isPopup: false, // đóng mở popup thông báo xóa
      //   decrementTypes: [], // Dữ liệu loại tài sản
      //   departments: [] // Dữ liệu phòng ban
    };
  },
  methods: {
    /**
     * Dùng bàn phim để lên xuống table
     */
    // addKeyList(e) {
    //   if (!this.isForm) {
    //     if (e.which == 38 && this.isActive > 0) {
    //       this.isActive--;
    //     }
    //     if (e.which == 40 && this.isActive < this.decrement.length - 1) {
    //       this.isActive++;
    //     }
    //   }
    // },
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
     * Bật checkbox để xáo nhiều item
     * Khi tắt checkbox bỏ tích các checkbox
     */
    isdeleteAllItem() {
      this.isCheckbox = !this.isCheckbox;
      if (this.isCheckbox == false) {
        this.idDeletes = [];
      }
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
      this.idItem = item.ref_decrement_id;
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
        let val = (value / 1).toFixed(0).replace(".", ",");
        return val.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ".");
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
    // deleteItem() {
    //   if (this.idDeletes.length == 0) {
    //     // xóa trong mảng
    //     this.assets.splice(this.assets.indexOf(this.itemDelete), 1);
    //     // call api xóa trên database
    //     let apiUrl =
    //       "http://localhost:51888/api/v1/Assets/" + this.itemDelete.assetId;
    //     const response = axios.delete(apiUrl).catch(e => console.log(e));
    //     console.log(response);
    //     this.$notify({
    //       group: "foo",
    //       title: "Thành công",
    //       text: "Xóa tài sản thành công",
    //       type: "success"
    //     });
    //   } else {
    //     var listDeletes = "";
    //     this.idDeletes.forEach(element => {
    //       listDeletes += "," + element.assetId;
    //     });
    //     this.idDeletes.forEach(item => {
    //       this.assets.splice(this.assets.indexOf(item), 1);
    //     });
    //     let apiUrl =
    //       "http://localhost:51888/api/v1/Assets?param=" +
    //       listDeletes.substring(1);
    //     // call api xóa nhiều tài sản trên database
    //     const response = axios.delete(apiUrl).catch(e => console.log(e));
    //     console.log(response);
    //     this.$notify({
    //       group: "foo",
    //       title: "Thành công",
    //       text: "Xóa tài sản thành công",
    //       type: "success"
    //     });
    //     this.idDeletes = [];
    //   }
    //   this.offPopupDelete();
    // },
    /**
     * Xóa nhiều tài sản cùng 1 lúc
     */
    deletesAsset() {
      if (this.idDeletes.length == 0) {
        // alert("Bạn chưa chọn tài sản xóa");
        this.$notify({
          group: "foo",
          title: "Cảnh báo",
          text: "Bạn chưa chọn tài sản xóa!",
          type: "error"
        });
      } else {
        this.showPopupDeletes();
      }
    }
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
        sum += this.decrement[i].cost_total;
      }
      return sum;
    }
    /**
     * Hàm lọc giá trị theo ô tìm kiếm (texsearch)
     * Lọc theo phòng ban (departmentFilter)
     * Lọc theo loại tài sản (assetTypeFilter)
     */
    // filteredAsset() {
    //   // debugger // eslint-disable-line
    //   let filterAssetType = this.assetTypeFilter,
    //     filterDepartment = this.departmentFilter,
    //     filterText = this.textSearch;

    //   return this.assets.filter(function (item) {
    //     let filtered = true;
    //     if (filterAssetType && filterAssetType.length > 0) {
    //       filtered = item.assetTypeId == filterAssetType;
    //     }
    //     if (filtered) {
    //       if (filterDepartment && filterDepartment.length > 0) {
    //         filtered = item.departmentId == filterDepartment;
    //       }
    //     }
    //     if (filtered) {
    //       if (filterText && filterText.length > 0) {
    //         filtered =
    //           item.assetCode.toLowerCase().includes(filterText.toLowerCase()) ||
    //           item.assetName.toLowerCase().includes(filterText.toLowerCase());
    //       }
    //     }
    //     return filtered;
    //   });
    // },
  }
  // call api lấy toàn bộ dữ liệu tài sản
  // async created() {
  //   /**
  //    * Gọi API lấy toàn bộ tài sản
  //    */
  //   const assets = await axios.get("http://localhost:51888/api/v1/Assets");
  //   this.assets = assets.data;
  //   /**
  //    * Gọi API lấy loại tài sản
  //    */
  //   const assetType = await axios.get(
  //     "http://localhost:51888/api/v1/AssetTypes"
  //   );
  //   this.assetTypes = assetType.data;
  //   /**
  //    * Gọi API lấy phòng ban
  //    */
  //   const department = await axios.get(
  //     "http://localhost:51888/api/v1/Departments"
  //   );
  //   this.departments = department.data;
  //   /**
  //    * Thêm sự kiện phím
  //    */
  //   window.addEventListener("keyup", this.addKeyList);
  // }
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
  margin-bottom: 30px;
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
p.sum-asset {
  margin-right: 130px;
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