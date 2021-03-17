<template>
  <!-- Form thực hiện thêm sửa  -->
  <div class="comp-form">
    <form class="form">
      <!-- phần header có title và 2 icon help và close  -->
      <div class="header-form">
        <p>Thêm chứng từ ghi giảm</p>
        <div class="icon-header">
          <img :src="helpIcon" alt="icon" />
          <img :src="closeIcon" alt="icon" @click="showOffForm" />
        </div>
      </div>
      <div class="input_form">
        <div class="date_input">
          <label>Ngày ghi giảm</label>
          <!-- <input class="input-search" type="date" /> -->
          <date-picker
            v-model="dataItem.refDate"
            value-type="YYYY-MM-DD"
            format="DD-MM-YYYY"
            class="datepicker"
            placeholder="dd-mm-yyyy"
            ref="postDate"
          ></date-picker>
        </div>
        <div class="decrement_number">
          <label>Số chứng từ</label>
          <input
            class="input-search"
            type="text"
            v-model="dataItem.refNo"
            ref="refNo"
            :maxlength="maxCode"
            @input="checkMaxLegthCode"
            @keypress="onlyCode"
            :class="{ warnning: isCheckCode }"
          />
          <span v-if="isCheckCode">Không được nhập kí tự đặc biệt</span>
        </div>
      </div>
      <div class="journal_memo">
        <label>Lý do ghi giảm</label>
        <div class="memo">
          <textarea
            v-model="dataItem.journalMemo"
            name="journal_memo"
            id="journal_memo"
            class="memo"
            rows="3"
          ></textarea>
        </div>
      </div>
      <div class="table-form">
        <p>Danh sách tài sản</p>
        <div class="table-asset">
          <table
            fieldName="RefDetail"
            id="decrement-sub-grid"
            class="decrement-sub-grid"
          >
            <thead>
              <tr>
                <th
                  style="width: 40px; max-width: 40px; min-width: 40px"
                  class="text-alight-center"
                  title="Số thứ tự"
                >
                  #
                </th>
                <th style="width: 89px; max-width: 89px; min-width: 89px">
                  Mã tài sản
                </th>
                <th style="width: 221px; max-width: 221px; min-width: 221px">
                  Tên tài sản
                </th>
                <th
                  style="width: 121px; max-width: 121px; min-width: 121px"
                  class="text-alight-right"
                >
                  Nguyên giá
                </th>
                <th
                  style="width: 121px; max-width: 121px; min-width: 121px"
                  class="text-alight-right"
                  title="Hao mòn lũy kế"
                >
                  Tỉ lệ HM năm (%)
                </th>
                <th style="width: 131px" class="text-alight-right">
                  Gía trị còn lại
                </th>
                <th style="width: 36px; max-width: 36px; min-width: 36px"></th>
              </tr>
            </thead>
            <tbody>
              <tr v-for="(item, index) in dataAssetForm" :key="index">
                <td scope="row" class="text-alight-center">{{ index + 1 }}</td>
                <td id="code">
                  <v-select
                    v-model="item.fixed_asset_code"
                    :options="options"
                    append-to-body
                    :calculate-position="withPopper"
                    @input="changeDataAsset(index, item.fixed_asset_code)"
                    label="assetCode"
                  />
                </td>
                <td class="out-line" :title="item.fixed_asset_name">{{ item.fixed_asset_name }}</td>
                <td>
                  <input
                    type="text"
                    class="text-alight-right"
                    v-model="item.cost"
                    @keypress="onlyNumber"
                  />
                </td>
                <td>
                  <input
                    type="text"
                    class="text-alight-right"
                    v-model="item.wearValue"
                    @keypress="onlyNumber"
                  />
                </td>
                <td>
                  <input
                    type="text"
                    class="text-alight-right"
                    v-model="item.resValue"
                    @keypress="onlyNumber"
                  />
                </td>
                <td @click="showPopupDelete(item)" class="text-alight-center">
                  <img :src="deleteIcon" alt="delete" />
                </td>
              </tr>
            </tbody>
          </table>
          <!-- ----------------------------- -->
        </div>
      </div>
      <div class="event-line">
        <div class="add-line">
          <button type="button" class="btn-add" @click.prevent="addLine">
            <i class="fas fa-plus"></i>
            <p>Thêm dòng</p>
          </button>
        </div>

        <div class="sub-line">
          <button type="button" class="btn-add" @click.prevent="removeAllLine">
            <i class="far fa-trash-alt"></i>
            <p>Xóa hết dòng</p>
          </button>
        </div>
      </div>
      <footer>
        <!-- Khi click vào nút hủy tắt form và reset dữ liệu  -->
        <button class="btn-add btn-cancel" @click.prevent="showOffForm">
          Hủy
        </button>
        <!-- Validate dữ liệu trên form rồi kiểm tra xem là thêm hay sửa  -->
        <button class="btn-add btn-submit" @click.prevent="addDataAsset">
          Đồng ý
        </button>
      </footer>
    </form>
    <!-- Hiển thị Popup khi isPopup = true , ẩn khi isPopup = false  -->
    <transition name="slide-fade">
      <div class="popup" v-if="isPopup">
        <div class="popupNotify">
          <h3>Xác nhận</h3>
          <p>Bạn chắc chắn muốn xóa tài sản này ?</p>
          <div class="btn-popup">
            <button class="btn-add btn-cancel" @click="offPopupDelete">
              Hủy
            </button>
            <button class="btn-add btn-del" @click="removeItem">Xóa</button>
          </div>
        </div>
      </div>
    </transition>
  </div>
</template>

<script>
import * as axios from "axios";
import DatePicker from "vue2-datepicker";
import "vue2-datepicker/index.css";
import "vue-select/dist/vue-select.css";
import { createPopper } from "@popperjs/core";
export default {
  components: { DatePicker },
  props: {
    idItem: String,
  },
  data() {
    return {
      // Khai bao svg
      helpIcon: require("../../assets/icon/help.svg"),
      closeIcon: require("../../assets/icon/cancel.svg"),
      deleteIcon: require("../../assets/icon/trash.svg"),
      // Dữ liệu item đẩy lên form
      date: "",
      /**
       * Dữ liệu hiển thị trên form
       * Nếu là thêm mới chứng từ khi render sẽ rỗng
       * Nếu là edit khi render gọi api chứng từ gán bằng các tài sản trong chứng từ rồi bind lên form
       */
      dataAssetForm: [
        {
          fixed_asset_id: null,
          fixed_asset_code: null,
          fixed_asset_name: null,
          cost: null,
          depreciation_rate: null,
          depreciation_year_price: null,
          tracked_year: null,
          life_time: null,
          production_year: null,
          wearValue: null,
          resValue: null,
        },
        {
          fixed_asset_id: null,
          fixed_asset_code: null,
          fixed_asset_name: null,
          cost: null,
          depreciation_rate: null,
          depreciation_year_price: null,
          tracked_year: null,
          life_time: null,
          production_year: null,
          wearValue: null,
          resValue: null,
        },
        {
          fixed_asset_id: null,
          fixed_asset_code: null,
          fixed_asset_name: null,
          cost: null,
          depreciation_rate: null,
          depreciation_year_price: null,
          tracked_year: null,
          life_time: null,
          production_year: null,
          wearValue: null,
          resValue: null,
        },
        {
          fixed_asset_id: null,
          fixed_asset_code: null,
          fixed_asset_name: null,
          cost: null,
          depreciation_rate: null,
          depreciation_year_price: null,
          tracked_year: null,
          life_time: null,
          production_year: null,
          wearValue: null,
          resValue: null,
        },
        {
          fixed_asset_id: null,
          fixed_asset_code: null,
          fixed_asset_name: null,
          cost: null,
          depreciation_rate: null,
          depreciation_year_price: null,
          tracked_year: null,
          life_time: null,
          production_year: null,
          wearValue: null,
          resValue: null,
        },
      ],
      /**
       * Dữ liệu tài sản gọi lên từ api để bind dữ liệu vào bảng trong form
       */
      dataAsset: [
        {
          fixed_asset_id: "11eea441-2a19-1735-6b78-553c0310877a",
          fixed_asset_code: "TS0022",
          fixed_asset_name: "Máy móc, thiết bị dùng trong ngành dệt",
          cost: 719325549.207,
          depreciation_rate: 52,
          depreciation_year_price: 627237005.6214,
          tracked_year: 2019,
          life_time: -1095589246,
          production_year: 2006,
        },
      ],
      textCode: "", // mã tài sản khi select
      iData: 0,
      isPopup: false, // trạng thái đóng mở popup
      itemDelete: {}, // item tài sản cần xóa
      dataItem: {}, // Data truyền vào api để post và put
      maxCode: 50, // giới hạn mã chứng từ
      isCheckCode: false, // kiểm tra cảnh báo
    };
  },
  methods: {
    /**
     * Hiển thị drop-menu sang bên phải
     */
    withPopper(dropdownList, component, { width }) {
      dropdownList.style.width = width;
      const popper = createPopper(component.$refs.toggle, dropdownList, {});
      return () => popper.destroy();
    },
     /**
     * Gán giá trị vừa chọn cho textCode để tìm trong mảng tài sản dữ liệu mã đó rồi bind vào dữ liệu render
     */
    changeDataAsset(index, dl) {
      event.preventDefault();
      this.textCode = dl;
      this.iData = index;
    },
    /**
     * Thêm 1 dòng trong bảng
     */
    addLine() {
      this.dataAssetForm.push({});
    },
    /**
     * Xóa hết các dòng trong bảng
     */
    removeAllLine() {
      this.textCode = "";
      this.iData = 0;
      this.dataAssetForm = [];
    },
    /**
     * Xóa tài sản khi bấn xóa ở popup
     */
    removeItem() {
      this.iData = 0;
      this.textCode = "";
      this.dataAssetForm.splice(this.itemDelete, 1);
      this.offPopupDelete();
    },
    /**
     * Mở popup xóa tài sản
     */
    showPopupDelete(item) {
      this.isPopup = true;
      this.itemDelete = item;
    },
    /**
     * Đóng popup xóa
     */
    offPopupDelete() {
      this.isPopup = false;
      // this.itemDelete = {};
    },
    /**
     * Format giá trên form
     */
    formatPrice(value) {
      if (value != null) {
        let val = (value / 1).toFixed(0).replace(".", ",");
        return val.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ".");
      }
    },
    /**
     * Format giá trị hiển thị giá trị còn lại
     */
    wearValueInput(index) {
      event.preventDefault();
      this.dataAssetForm[index].wearValue = event.target.value;
    },
    /**
     * Format giá khi click ra ngoài
     */
    handleBlurWearValue(index) {
      // console.log(event.target.value)
      this.dataAssetForm[
        index
      ].wearValue = event.target.value
        .toString()
        .replace(/\B(?=(\d{3})+(?!\d))/g, ".");
    },
    /**
     * Input chỉ đc nhập số
     */
    onlyNumber($event) {
      let keyCode = $event.keyCode ? $event.keyCode : $event.which;
      if ((keyCode < 48 || keyCode > 57) && keyCode !== 46) {
        // 46 is dot
        $event.preventDefault();
      }
    },
    /**
     * Nhập số chứng từ không được nhập các kí tự đặc biệt
     */
    onlyCode($event) {
      let keyCode = $event.keyCode ? $event.keyCode : $event.which;
      if (
        (keyCode < 48 || keyCode > 57) &&
        (keyCode < 65 || keyCode > 90) &&
        (keyCode < 97 || keyCode > 122)
      ) {
        $event.preventDefault();
        this.isCheckCode = true;
      } else {
        this.isCheckCode = false;
      }
    },
    // /**
    //  * Format lại giá trên form
    //  */
    // formatPrice(value) {
    //   if (value != null) {
    //     // Làm tròn rồi chèn dấu .
    //     let val = (value / 1).toFixed(0).replace(".", ",");
    //     return val.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ".");
    //   }
    //   return value;
    // },
    // /**
    //  * giới hạn kí tự khi nhập vào input mã
    //  */
    checkMaxLegthCode() {
      // thông báo giới hạn kí tự
      if (this.dataItem.refNo != null) {
        if (this.dataItem.refNo.length == this.maxCode) {
          this.$notify({
            group: "foo",
            title: "Cảnh báo",
            text: "Mã tài sản không được nhập quá " + this.maxCode + " kí tự",
            type: "error",
          });
        }
      }
    },
    /**
     * Tắt form và xóa dữ liệu item
     */
    showOffForm() {
      this.$store.dispatch("offForm");
      this.$emit("resetItem");
    },
    sumPrice() {
      var sum = 0;
      for (let i = 0; i < this.dataAssetForm.length; i++) {
        sum += parseInt(this.dataAssetForm[i].resValue);
      }
      return sum;
    },
    /**
     * Click vào nút lưu thực hiện thêm data khi id null và sửa data khi id khác null
     */
    addDataAsset() {
      // kiểm tra nghiệp vụ nếu false thì thực hiện thêm hoặc sửa
      if (this.validateData.error) {
        this.$notify({
          group: "foo",
          title: "Cảnh báo",
          text: this.validateData.msg,
          type: "error",
        });
        // focus vào ô input
        switch (this.validateData.typeError) {
          case "postDate":
            this.$refs.postDate.focus();
            // this.isCheckCode = true;
            // this.msgCode = this.validateData.msg;
            break;
          case "refNo":
            this.$refs.refNo.focus();
            // this.isCheckName = true;
            // this.msgName = this.validateData.msg;
            break;
          default:
            break;
        }
      } else {
        //chuyển datetime từ "" -> null
        // this.dataAssetForm.forEach((element) => {
        //   if (element.fixed_asset_code == null) {
        //     this.dataAssetForm.splice(this.dataAssetForm.indexOf(element), 1);
        //   }
        // });
        var filtered = this.dataAssetForm.filter(function (el) {
          return el.fixed_asset_code != null;
        });
        this.dataItem.refDetail = JSON.stringify(filtered);
        this.dataItem.costTotal = this.sumPrice();
        //  debugger // eslint-disable-line
        if (this.dataItem.refDecrementId == null) {
          // debugger // eslint-disable-line
          // Thực hiện post
          console.log("post");
          axios
            .post("https://localhost:44392/api/v1/RefDecrements", this.dataItem)
            .then((response) => {
              if (!response.data.success) {
                this.$notify({
                  group: "foo",
                  title: "Lỗi",
                  text: response.data.data.userMsg[0],
                  type: "error",
                });
              } else {
                this.$notify({
                  group: "foo",
                  title: "Thành công",
                  text: "Thêm mới thành công",
                  type: "success",
                });
                this.showOffForm();
                location.reload();
              }
              console.log(response);
            })
            .catch((error) => {
              this.$notify({
                group: "foo",
                title: "Lỗi",
                text:
                  "Đã có lỗi xảy ra, vui lòng liên hệ MISA để được trợ giúp",
                type: "error",
              });
              console.log(error);
            });
        } else {
          // Thực hiện put
          axios
            .put("https://localhost:44392/api/v1/RefDecrements", this.dataItem)
            .then((response) => {
              if (!response.data.success) {
                this.$notify({
                  group: "foo",
                  title: "Lỗi",
                  text: response.data.data.userMsg[0],
                  type: "error",
                });
              } else {
                this.$notify({
                  group: "foo",
                  title: "Thành công",
                  text: "Cập nhật thành công",
                  type: "success",
                });
                this.showOffForm();
                location.reload();
              }
              console.log(response);
            })
            .catch((error) => {
              this.$notify({
                group: "foo",
                title: "Lỗi",
                text:
                  "Đã có lỗi xảy ra, vui lòng liên hệ MISA để được trợ giúp",
                type: "error",
              });
              console.log(error);
            });
        }
      }
    },
  },
  computed: {
    // wearValue() {
    //   // this.dataAssetForm[this.iData].wearValue = this.dataAssetForm[
    //   //   this.iData
    //   // ].wearValue =
    //   //   this.dataAssetForm[this.iData].life_time *
    //   //   (this.dataAssetForm[this.iData].depreciation_rate / 100) *
    //   //   this.dataAssetForm[this.iData].cost;
    //   return (
    //     this.dataAssetForm[this.iData].life_time *
    //     (this.dataAssetForm[this.iData].depreciation_rate / 100) *
    //     this.dataAssetForm[this.iData].cost
    //   );
    // },
    /**
     * lấy giá trị từ api tài sản đẩy vào mảng để hiển thị select
     */
    options() {
      let assetC = [];
      this.dataAsset.forEach((element) => {
        assetC.push(element.fixed_asset_code);
        // let optionCode = {};
        // optionCode.assetCode = element.assetCode;
        // optionCode.disabled = true;
        // assetC.push(optionCode);
      });
      return assetC;
    },
    //   originalPrice() {
    //     let price = null;
    //     if (this.dataItem.originalPrice != null) {
    //       price = this.dataItem.originalPrice.toString();
    //       // // .toString()
    //       // .replace(/\B(?=(\d{3})+(?!\d))/g, ",");
    //     }
    //     return price;
    //   },
    //   /**
    //    * Validate dữ liệu trên form
    //    */
    validateData() {
      // debugger // eslint-disable-line
      let returnData = {
        error: false,
        msg: "",
        typeError: "",
      };
      //1. validate để trống

      if (this.dataItem.refNo == "" || this.dataItem.refNo == null) {
        returnData = {
          error: true,
          msg: "Vui lòng điền mã chứng từ",
          typeError: "refNo",
        };
      }
      if (this.dataItem.refDate == "" || this.dataItem.refDate == null) {
        returnData = {
          error: true,
          msg: "vui lòng chọn ngày ghi giảm",
          typeError: "postDate",
        };
      }
      return returnData;
    },
  },
  watch: {
    textCode: function () {
      if (this.textCode != "") {
        this.dataAsset.forEach((element) => {
          if (element.fixed_asset_code == this.textCode) {
            this.dataAssetForm[this.iData].fixed_asset_id =
              element.fixed_asset_id;
            this.dataAssetForm[this.iData].fixed_asset_code =
              element.fixed_asset_code;
            this.dataAssetForm[this.iData].fixed_asset_name =
              element.fixed_asset_name;
            this.dataAssetForm[this.iData].cost = element.cost;
            this.dataAssetForm[this.iData].depreciation_rate =
              element.depreciation_rate;
            this.dataAssetForm[this.iData].depreciation_year_price =
              element.depreciation_year_price;
            this.dataAssetForm[this.iData].tracked_year = element.tracked_year;
            this.dataAssetForm[this.iData].life_time = element.life_time;
            this.dataAssetForm[this.iData].production_year =
              element.production_year;
              this.dataAssetForm[this.iData].wearValue =this.dataAssetForm[this.iData].cost * this.dataAssetForm[this.iData].life_time * (this.dataAssetForm[this.iData].depreciation_rate / 100);
            this.dataAssetForm[this.iData].resValue = this.dataAssetForm[this.iData].cost - this.dataAssetForm[this.iData].wearValue;
          }
        });
      }
      if (this.textCode == null) {
        this.dataAssetForm[this.iData].fixed_asset_id = null;
        this.dataAssetForm[this.iData].fixed_asset_code = null;
        this.dataAssetForm[this.iData].fixed_asset_name = null;
        this.dataAssetForm[this.iData].cost = null;
        this.dataAssetForm[this.iData].depreciation_rate = null;
        this.dataAssetForm[this.iData].depreciation_year_price = null;
        this.dataAssetForm[this.iData].tracked_year = null;
        this.dataAssetForm[this.iData].life_time = null;
        this.dataAssetForm[this.iData].production_year = null;
        this.dataAssetForm[this.iData].wearValue = null;
        this.dataAssetForm[this.iData].resValue = null;
      }
    },
  },
  /*--------------LIFE CYCLE-----------------------------------------*/
  async created() {
    /**
     * Gọi API lấy 1 tài sản theo id
     */
    if (this.idItem != null && this.idItem != "") {
      let urlApi =
        "https://localhost:44392/api/v1/RefDecrements/" + this.idItem;
      axios.get(urlApi).then((reponsive) => {
        this.dataItem = reponsive.data.data;
        this.dataAssetForm = JSON.parse(this.dataItem.refDetail);
      });
    }
    const assets = await axios.get("https://localhost:44392/api/v1/Assets");
    this.dataAsset = assets.data.data;
    // window.addEventListener("keyup", this.addKeyForm);
  },
  // beforeUpdate() {
  //   this.dataAssetForm.forEach((element) => {
  //     if (element.fixed_asset_code != null) {
  //       element.wearValue =
  //         element.cost * element.life_time * (element.depreciation_rate / 100);
  //       element.resValue = element.cost - element.wearValue;
  //     }
  //   });
  // },
};
</script>

<style scoped>
label {
  /* font-family: "GoogleSans-Thin"; */
  font-size: 13px;
  color: #373737;
}
.header-form {
  padding: 20px 20px 0 20px;
  display: flex;
  justify-content: space-between;
}
.icon-header img {
  margin-left: 10px;
}
.input-search {
  height: 35px;
  width: 100%;
}
.decrement_number span {
  font-size: 12px;
  color: red;
  position: absolute;
}
.form-row {
  margin-left: 20px;
  margin-right: 20px;
}
.warnning {
  border: 1px solid red;
}
p.textWarning {
  font-size: 11px;
  margin-bottom: -10px;
  color: #e24949;
  margin-top: 5px;
  font-weight: 100;
}
.input-disabled {
  background-color: #f5f5f5;
  border: 1px solid #e8e8e8;
}
/**
  Phần nhập ngày và số chứng từ
*/
.input_form {
  display: flex;
  margin-left: 20px;
}

.input_form input.input-search {
  width: 149px;
  height: 31px;
  display: block;
  margin-right: 31px;
}
.date_input input.input-search {
  padding-top: 6px;
  padding-bottom: 9px;
  padding-left: 10px;
}

.decrement_number input.input-search {
  padding-top: 8px;
  padding-bottom: 7px;
  padding-left: 14px;
  font-size: 13px;
  color: #373737;
}
/**
Phần textarea
*/
.journal_memo {
  margin-top: 18px;
  margin-left: 20px;
}
textarea#journal_memo {
  width: 100%;
  padding: 0;
  border: none;
}
.memo {
  display: block;
  width: 750px;
  resize: none;
  border: 1px solid #e2e2e2;
  outline: none;
  padding-top: 9px;
  padding-left: 15px;
  padding-bottom: 8px;
  font-size: 13px;
}
/**.date_input
Bảng danh sách tài sản
 */
.table-form {
  margin-left: 20px;
  margin-top: 18px;
  margin-right: 20px;
}
.table-form tbody td {
  padding-top: 7px;
  padding-bottom: 8px;
  height: 35px;
}
.table-asset {
  height: 212px;
  overflow: auto;
}
.table-asset input {
  border: none;
  border: none;
  padding: 0;
  font-size: 13px;
  color: #373737;
  width: 100%;
}
.table-asset input:focus {
  outline: none;
}
/* .table-asset .iconDelete {
  padding-left: 12px;
} */
.decrement-sub-grid thead {
  width: 100px !important;
}
.decrement-sub-grid th {
  padding: 0 13px 0px 13px;
  box-sizing: border-box;
  height: 35px;
  background-color: #e8e8e8;
  white-space: nowrap;
  font-size: 13px;
  font-weight: 500;
  color: #212121;
  position: sticky;
  top: 0px;
  z-index: 10;
}
.decrement-sub-grid tbody tr td {
  height: 34px;
  padding: 0 0 0 0;
  border: #e0e0e0 solid 1px;
  font-size: 13px;
  color: #373737;
  border-right: none;
}
table .out-line {
  white-space: nowrap;
  text-overflow: ellipsis;
  max-width: 221px;
  overflow: hidden;
  padding: 0 13px !important;
}
/*--------------2 nút thêm và xóa--------*/
.event-line button.btn-add {
  width: 120px;
  height: 32px;
  background-color: white;
  border: 1px solid black;
}

.event-line {
  margin-left: 20px;
  margin-top: 17px;
  display: flex;
}

i.fas.fa-plus {
  color: #00abfe;
  font-size: 14px;
  padding-top: 9px;
  padding-left: 12px;
}

.add-line button.btn-add {
  border: 1px solid #00abfe;
  display: flex;
}

.add-line p {
  font-size: 13px;
  color: #00abfe;
  padding-top: 6px;
  padding-left: 11px;
  font-weight: 100;
}

i.far.fa-trash-alt {
  color: #7b7a8d;
  font-size: 14px;
  padding-top: 9px;
  padding-left: 12px;
}

.sub-line button.btn-add {
  border: 1px solid #e2e2e2;
  display: flex;
  margin-left: 21px;
}

.sub-line p {
  font-size: 13px;
  color: #212121;
  padding-top: 6px;
  padding-left: 11px;
  font-weight: 100;
}
</style>