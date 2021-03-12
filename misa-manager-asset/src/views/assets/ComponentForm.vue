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
            v-model="date"
            value-type="YYYY-MM-DD"
            format="DD-MM-YYYY"
            class="datepicker"
            placeholder="dd-mm-yyyy"
          ></date-picker>
        </div>
        <div class="decrement_number">
          <label>Số chứng từ</label>
          <input class="input-search" type="text" />
        </div>
      </div>
      <div class="journal_memo">
        <label>Lý do ghi giảm</label>
        <div class="memo">
          <textarea
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
          <table class="table table-bordered">
            <thead>
              <tr>
                <th scope="col" style="width:40px;" class="order">#</th>
                <th scope="col" style="width:89px;">Mã tài sản</th>
                <th scope="col" style="width:221px;">Tên tài sản</th>
                <th scope="col" style="width:121px;" class="cost_total">
                  Nguyên giá
                </th>
                <th scope="col" style="width:121px;" class="cost_total">
                  HM lũy kế
                </th>
                <th scope="col" style="width:132px;" class="cost_total">
                  Giá trị còn lại
                </th>
                <th scope="col" style="width:35px;" class="iconDelete"></th>
              </tr>
            </thead>
            <tbody>
              <tr v-for="(item, index) in dataAssetForm" :key="index">
                <td scope="row" class="order">{{ index + 1 }}</td>
                <td id="code">
                  <v-select
                    v-model="item.assetCode"
                    :options="options"
                    append-to-body
                    :calculate-position="withPopper"
                    @input="changeDataAsset(index, item.assetCode)"
                    label="assetCode"
                  />
                </td>
                <td>{{ item.assetName }}</td>
                <td class="cost_total">
                  {{ formatPrice(item.originalPrice) }}
                </td>
                <td class="cost_total">
                  {{ formatPrice(item.wearAccumulated) }}
                </td>
                <td>
                  <input
                    type="text"
                    class="cost_total"
                    :value="item.wearValue"
                    @input="wearValueInput(index)"
                    @blur="handleBlurWearValue(index)"
                    @keypress="onlyNumber"
                  />
                </td>
                <td @click="showPopupDelete(item)" class="iconDelete">
                  <img :src="deleteIcon" alt="delete" />
                </td>
              </tr>
            </tbody>
          </table>
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
        <button class="btn-add btn-submit">
          <!-- @click.prevent="addDataAsset" -->
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
import DatePicker from "vue2-datepicker";
import "vue2-datepicker/index.css";
import "vue-select/dist/vue-select.css";
import { createPopper } from "@popperjs/core";
export default {
  components: { DatePicker },
  props: {
    // dataAsset: Array, // Mảng tất cả dữ liệu truyền từ Comp-list xuống
    // dataDepartments: Array, // Mảng dữ liệu phòng ban truyền từ Comp-list xuống
    // dataAssetTypes: Array, // Mảng dữ liệu loại tài sản truyền từ Comp-list xuống
    // itemTemp: Object // Dữ liệu 1 đối tượng để truyền vào form
    idItem: String
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
          assetId: null,
          assetCode: null,
          assetName: null,
          originalPrice: null,
          wearAccumulated: null,
          wearValue: null
        },
        {
          assetId: null,
          assetCode: null,
          assetName: null,
          originalPrice: null,
          wearAccumulated: null,
          wearValue: null
        },
        {
          assetId: null,
          assetCode: null,
          assetName: null,
          originalPrice: null,
          wearAccumulated: null,
          wearValue: null
        },
        {
          assetId: null,
          assetCode: null,
          assetName: null,
          originalPrice: null,
          wearAccumulated: null,
          wearValue: null
        },
        {
          assetId: null,
          assetCode: null,
          assetName: null,
          originalPrice: null,
          wearAccumulated: null,
          wearValue: null
        }
      ],
      /**
       * Dữ liệu tài sản gọi lên từ api để bind dữ liệu vào bảng trong form
       */
      dataAsset: [
        {
          assetId: "1",
          assetCode: "LS0001",
          assetName: "laptop1",
          originalPrice: 10000000,
          wearAccumulated: 1000000,
          wearValue: 9000000
        },
        {
          assetId: "2",
          assetCode: "LS0002",
          assetName: "laptop 2",
          originalPrice: 10000000,
          wearAccumulated: 1000000,
          wearValue: 9000000
        },
        {
          assetId: "3",
          assetCode: "LS0003",
          assetName: "laptop 3",
          originalPrice: 10000000,
          wearAccumulated: 1000000,
          wearValue: 9000000
        },
        {
          assetId: "4",
          assetCode: "LS0004",
          assetName: "laptop 4",
          originalPrice: 10000000,
          wearAccumulated: 1000000,
          wearValue: 9000000
        }
      ],
      textCode: "",
      iData: 0,
      isPopup: false,
      itemDelete: {}
    };
  },
  /**
   * Lifecycle gán trước khi update thực hiện khi thay đổi mã thì thay đổi tên theo database
   */
  beforeUpdate() {},
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
     * Gán giá trị vừa chọn cho textCode để tìm trong mảng tài sản dữ liệu mã đó rồi bind vào dữ liệu render
     */
    changeDataAsset(index, dl) {
      event.preventDefault();
      this.textCode = dl;
      this.iData = index;
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
    // submitForm(e) {
    //   // To prevent the form from submitting
    //   e.preventDefault();
    //   // return false;
    // },
    // /**
    //  * Sự kiện nút tắt và lưu form
    //  */
    // addKeyForm(e) {
    //   if (this.isForm) {
    //     if (e.which == 27) {
    //       this.showOffForm();
    //     }
    //     // if (e.which == 13) {
    //     //   this.addDataAsset();
    //     // }
    //   }
    // },
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
    // limitAssetCode() {
    //   // thông báo giới hạn kí tự
    //   if (this.dataItem.assetCode != null) {
    //     if (this.dataItem.assetCode.length == this.maxAssetCode) {
    //       this.$notify({
    //         group: "foo",
    //         title: "Cảnh báo",
    //         text:
    //           "Mã tài sản không được nhập quá " + this.maxAssetCode + " kí tự",
    //         type: "error"
    //       });
    //     }
    //     this.isCheckCode = false;
    //   }
    //   // Thông báo không được nhập kí tự đặc việt
    //   // Tự động replace
    //   var format = "<>@!#$%^&*()_+[]{}?:;|'\"\\,./~`=";
    //   for (let i = 0; i < format.length; i++) {
    //     if (this.dataItem.assetCode.indexOf(format[i]) > -1) {
    //       this.isCheckCode = true;
    //       this.msgCode = "Không được nhập kí tự đặc biệt";
    //       this.dataItem.assetCode = this.dataItem.assetCode.replace(
    //         /[^A-Za-z0-9]/,
    //         ""
    //       );
    //     }
    //   }
    //   // this.isCheckCode = false;
    // },
    // /**
    //  * giới hạn kí tự khi nhập vào input tên
    //  */
    // limitAssetName() {
    //   // Thông báo giới hạn kí tự
    //   if (this.dataItem.assetName != null) {
    //     if (this.dataItem.assetName.length == this.maxAssetName) {
    //       this.$notify({
    //         group: "foo",
    //         title: "Cảnh báo",
    //         text:
    //           "Tên tài sản không được nhập quá " + this.maxAssetName + " kí tự",
    //         type: "error"
    //       });
    //     }
    //     this.isCheckName = false;
    //   }
    //   // Thông báo không được nhập kí tự đặc biệt
    //   // Tự động replace
    //   var format = "<>@!#$%^&*()_+[]{}?:;|'\"\\,./~`=";
    //   for (let i = 0; i < format.length; i++) {
    //     if (this.dataItem.assetName.indexOf(format[i]) > -1) {
    //       this.isCheckName = true;
    //       this.msgName = "Không được nhập kí tự đặc biệt";
    //       this.dataItem.assetName = this.dataItem.assetName.replace(
    //         /[^A-Za-z0-9]/,
    //         ""
    //       );
    //     }
    //   }
    // },
    // /**
    //  * Validate trường số : không được nhập chữ và max = 9
    //  * Thời gian sử dụng
    //  */
    // timeUseNumber() {
    //   // Thông báo giới hạn kí tự
    //   if (this.dataItem.timeUse != null) {
    //     if (this.dataItem.timeUse.length == this.maxNumber) {
    //       this.$notify({
    //         group: "foo",
    //         title: "Cảnh báo",
    //         text:
    //           "Thởi gian sử dụng không được nhập quá " +
    //           this.maxNumber +
    //           " kí tự",
    //         type: "error"
    //       });
    //     }
    //   }
    // },
    // /**
    //  * Tỷ lệ hao mòn
    //  */
    // wearRateNumber() {
    //   // Thông báo giới hạn kí tự
    //   if (this.dataItem.wearRate != null) {
    //     if (this.dataItem.wearRate.length == this.maxNumber) {
    //       this.$notify({
    //         group: "foo",
    //         title: "Cảnh báo",
    //         text:
    //           "Tỷ lệ hao mòn không được nhập quá " + this.maxNumber + " kí tự",
    //         type: "error"
    //       });
    //     }
    //   }
    // },
    // /**
    //  * Nguyên giá
    //  * format lại giá trị trên form
    //  */
    // originalPriceNumber(e) {
    //   let valueIpnut = e.target.value;
    //   if (e.target.value == "") {
    //     this.dataItem.originalPrice = null;
    //   }
    //   // Thông báo giới hạn kí tự
    //   if (valueIpnut.length == this.maxPrice) {
    //     this.$notify({
    //       group: "foo",
    //       title: "Cảnh báo",
    //       text: "Nguyên giá không được vượt quá tỷ",
    //       type: "error"
    //     });
    //   }
    //   // Thông báo không được nhập chũ
    //   // format giá và replace chữ
    //   var numbers = "^[0-9,]+$";
    //   if (!valueIpnut.match(numbers)) {
    //     if (valueIpnut == "") {
    //       this.dataItem.originalPrice = null;
    //     } else {
    //       this.$notify({
    //         group: "foo",
    //         title: "Cảnh báo",
    //         text: "Trường này chỉ được nhập số",
    //         type: "error"
    //       });
    //       this.dataItem.originalPrice = parseInt(valueIpnut.replace(/\D/g, ""));
    //     }
    //   } else {
    //     this.dataItem.originalPrice = parseInt(e.target.value.replace(",", ""));
    //   }
    // },
    // /**
    //  * Gía trị hao mòn
    //  */
    // wearValueNumber() {
    //   // Thông báo giới hạn kí tự
    //   if (this.dataItem.wearValue != null) {
    //     if (this.dataItem.wearValue.length == this.maxNumber) {
    //       this.$notify({
    //         group: "foo",
    //         title: "Cảnh báo",
    //         text:
    //           "Gía trị hao mòn không được nhập quá " +
    //           this.maxNumber +
    //           " kí tự",
    //         type: "error"
    //       });
    //     }
    //   }
    //   // Thông báo không được nhập chữ
    //   var numbers = /^[0-9]+$/;
    //   if (
    //     !this.dataItem.wearValue.match(numbers) &&
    //     this.dataItem.wearValue != ""
    //   ) {
    //     this.$notify({
    //       group: "foo",
    //       title: "Cảnh báo",
    //       text: "Trường này chỉ được nhập số",
    //       type: "error"
    //     });
    //   }
    //   this.dataItem.wearValue = this.dataItem.wearValue.replace(/\D/g, "");
    // },
    // /**
    //  * Tắt form và xóa dữ liệu item
    //  */
    showOffForm() {
      this.$store.dispatch("offForm");
      this.$emit("resetItem");
    }
    // /**
    //  * Click vào nút lưu thực hiện thêm data khi id null và sửa data khi id khác null
    //  */
    // // addDataAsset() {
    // //   // kiểm tra nghiệp vụ nếu false thì thực hiện thêm hoặc sửa
    // //   if (this.validateData.error) {
    // //     this.$notify({
    // //       group: "foo",
    // //       title: "Cảnh báo",
    // //       text: this.validateData.msg,
    // //       type: "error"
    // //     });
    // //     // focus vào ô input
    // //     switch (this.validateData.typeError) {
    // //       case "code":
    // //         this.$refs.code.focus();
    // //         this.isCheckCode = true;
    // //         this.msgCode = this.validateData.msg;
    // //         break;
    // //       case "name":
    // //         this.$refs.name.focus();
    // //         this.isCheckName = true;
    // //         this.msgName = this.validateData.msg;
    // //         break;
    // //       case "department":
    // //         this.$refs.department.focus();
    // //         break;
    // //       case "type":
    // //         this.$refs.type.focus();
    // //         break;
    // //       default:
    // //         break;
    // //     }
    // //   } else {
    // //     //chuyển datetime từ "" -> null
    // //     if (this.dataDate == "") {
    // //       this.dataItem.increaseDate = null;
    // //     }
    // //     //  debugger // eslint-disable-line
    // //     if (this.dataItem.assetId == null) {
    // //       // debugger // eslint-disable-line
    // //       // Thực hiện post
    // //       axios
    // //         .post("http://localhost:51888/api/v1/Assets", this.dataItem)
    // //         .then(response => {
    // //           if (!response.data.success) {
    // //             this.$notify({
    // //               group: "foo",
    // //               title: "Lỗi",
    // //               text: response.data.data.userMsg[0],
    // //               type: "error"
    // //             });
    // //           } else {
    // //             this.$notify({
    // //               group: "foo",
    // //               title: "Thành công",
    // //               text: "Thêm mới thành công",
    // //               type: "success"
    // //             });
    // //             this.showOffForm();
    // //             location.reload();
    // //           }
    // //           console.log(response);
    // //         })
    // //         .catch(error => {
    // //           this.$notify({
    // //             group: "foo",
    // //             title: "Lỗi",
    // //             text:
    // //               "Đã có lỗi xảy ra, vui lòng liên hệ MISA để được trợ giúp",
    // //             type: "error"
    // //           });
    // //           console.log(error);
    // //         });
    // //     } else {
    // //       // Thực hiện put
    // //       let apiUrl =
    // //         "http://localhost:51888/api/v1/Assets/" + this.dataItem.assetId;
    // //       axios
    // //         .put(apiUrl, this.dataItem)
    // //         .then(response => {
    // //           if (!response.data.success) {
    // //             this.$notify({
    // //               group: "foo",
    // //               title: "Lỗi",
    // //               text: response.data.data.userMsg[0],
    // //               type: "error"
    // //             });
    // //           } else {
    // //             this.$notify({
    // //               group: "foo",
    // //               title: "Thành công",
    // //               text: "Cập nhật thành công",
    // //               type: "success"
    // //             });
    // //             this.showOffForm();
    // //             location.reload();
    // //           }
    // //           console.log(response);
    // //         })
    // //         .catch(error => {
    // //           this.$notify({
    // //             group: "foo",
    // //             title: "Lỗi",
    // //             text:
    // //               "Đã có lỗi xảy ra, vui lòng liên hệ MISA để được trợ giúp",
    // //             type: "error"
    // //           });
    // //           console.log(error);
    // //         });
    // //     }
    // //   }
    // // }
  },
  computed: {
    /**
     * lấy giá trị từ api tài sản đẩy vào mảng để hiển thị select
     */
    options() {
      let assetC = [];
      this.dataAsset.forEach(element => {
        // assetC.push(element.assetCode);
        let optionCode = {};
        optionCode.assetCode = element.assetCode;
        optionCode.disabled = true;
        assetC.push(optionCode);
      });
      return assetC;
    }
    // bindDataForm() {
    //   if (this.textCode != "") {
    //     this.dataAsset.forEach(element => {
    //       if (element.assetCode == this.textCode) {
    //         this.dataAssetForm[this.iData].assetId = element.assetId;
    //         this.dataAssetForm[this.iData].assetCode = element.assetCode;
    //         this.dataAssetForm[this.iData].assetName = element.assetName;
    //         this.dataAssetForm[this.iData].originalPrice =
    //           element.originalPrice;
    //         this.dataAssetForm[this.iData].wearAccumulated =
    //           element.wearAccumulated;
    //         this.dataAssetForm[this.iData].wearValue = element.wearValue;
    //       }
    //     });
    //   }
    //   return this.dataAssetForm;
    // }
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
    //    * Lấy biến isForm từ store
    //    */
    //   isForm() {
    //     return this.$store.state.isForm;
    //   },
    //   /**
    //    * Validate dữ liệu trên form
    //    */
    //   validateData() {
    //     // debugger // eslint-disable-line
    //     let returnData = {
    //       error: false,
    //       msg: "",
    //       typeError: ""
    //     };
    //     //1. validate để trống
    //     //Để trống loại tài sản
    //     if (this.dataItem.assetTypeId == "") {
    //       returnData = {
    //         error: true,
    //         msg: "Vui lòng chọn loại tài sản",
    //         typeError: "type"
    //       };
    //     }
    //     // Để trống tên phòng ban
    //     if (this.dataItem.departmentId == "") {
    //       returnData = {
    //         error: true,
    //         msg: "Vui lòng chọn phòng ban tài sản",
    //         typeError: "department"
    //       };
    //     }
    //     // Để trống tên tài sản
    //     if (this.dataItem.assetName == null || this.dataItem.assetName == "") {
    //       returnData = {
    //         error: true,
    //         msg: "Vui lòng nhập tên tài sản",
    //         typeError: "name"
    //       };
    //     }
    //     // Để trống mã tài sản
    //     if (this.dataItem.assetCode == null || this.dataItem.assetCode == "") {
    //       returnData = {
    //         error: true,
    //         msg: "Vui lòng nhập mã tài sản",
    //         typeError: "code"
    //       };
    //     }
    //     return returnData;
    //   }
  },
  watch: {
    textCode: function() {
      if (this.textCode != null) {
        this.dataAsset.forEach(element => {
          if (element.assetCode == this.textCode.assetCode) {
            this.dataAssetForm[this.iData].assetId = element.assetId;
            this.dataAssetForm[this.iData].assetCode = element.assetCode;
            this.dataAssetForm[this.iData].assetName = element.assetName;
            this.dataAssetForm[this.iData].originalPrice =
              element.originalPrice;
            this.dataAssetForm[this.iData].wearAccumulated =
              element.wearAccumulated;
            this.dataAssetForm[this.iData].wearValue = element.wearValue;
          }
        });
        // this.options.forEach(element => {
        //   if(this.textCode == element.assetCode)
        // });
      } else {
        this.dataAssetForm[this.iData].assetId = null;
        this.dataAssetForm[this.iData].assetCode = null;
        this.dataAssetForm[this.iData].assetName = null;
        this.dataAssetForm[this.iData].originalPrice = null;
        this.dataAssetForm[this.iData].wearAccumulated = null;
        this.dataAssetForm[this.iData].wearValue = null;
      }
      this.textCode = "";
    }
  }
  // // async created() {
  // //   /**
  // //    * Gọi API lấy 1 tài sản theo id
  // //    */
  // //   if (this.itemTemp.assetId != null) {
  // //     let urlApi =
  // //       "http://localhost:51888/api/v1/Assets/" + this.itemTemp.assetId;
  // //     const item = await axios.get(urlApi);
  // //     this.dataItem = item.data;
  // //   }
  // //   window.addEventListener("keyup", this.addKeyForm);
  // // }
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
  /* font-family: "GoogleSans-Thin"; */
  height: 35px;
  width: 100%;
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
.table-form thead th {
  font-size: 13px;
  font-weight: 500;
  color: #212121;
  background-color: #e8e8e8;
  border: 1px solid #e0e0e0;
  padding-top: 7px;
  padding-bottom: 7px;
  white-space: nowrap;
  padding-left: 13px;
  position: sticky;
  top: -1px;
}
.table-form tbody td {
  padding-top: 7px;
  padding-bottom: 8px;
  height: 35px;
}
.table-asset {
  height: 210px;
  overflow: auto;
}
.table-asset .table {
  margin-bottom: 0;
}
.table-asset input {
  border: none;
  padding: 0;
  border: none;
  padding: 0;
  font-size: 13px;
  color: #373737;
}
.table-asset input:focus {
  outline: none;
}
td#code {
  padding: 0;
  position: relative;
}
.table-asset .iconDelete {
  padding-left: 12px;
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