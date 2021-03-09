<template>
  <!-- Form thực hiện thêm sửa  -->
  <div class="comp-form">
    
  </div>
</template>

<script>

export default {
  props: {
    dataAsset: Array, // Mảng tất cả dữ liệu truyền từ Comp-list xuống
    dataDepartments: Array, // Mảng dữ liệu phòng ban truyền từ Comp-list xuống
    dataAssetTypes: Array, // Mảng dữ liệu loại tài sản truyền từ Comp-list xuống
    itemTemp: Object // Dữ liệu 1 đối tượng để truyền vào form
  },
  data() {
    return {
      // Khai bao svg
      helpIcon: require("../../assets/icon/help.svg"),
      closeIcon: require("../../assets/icon/cancel.svg"),
      // Dữ liệu item đẩy lên form
      dataItem: {
        assetCode: "",
        assetId: "",
        assetName: "",
        assetTypeCode: "",
        assetTypeId: "",
        assetTypeName: "",
        departmentCode: "",
        departmentId: "",
        departmentName: "",
        increaseDate: "",
        isUsed: null,
        originalPrice: null,
        timeUse: null,
        wearRate: null,
        wearValue: null
      },
      maxAssetCode: 50, // giới hạn kí tự nhập vào ô mã tài sản
      maxAssetName: 255, // giới hạn kí tự nhập vào ô tên tài sản
      maxNumber: 9, // giới hạn các giá trị nhập số
      maxPrice: 10, // giới hạn nguyên giá
      isCheckCode: false, // thông báo lỗi khi không nhập mã hoặc trùng mã
      isCheckName: false, // thông báo khi không nhập tên
      msgCode: "", // tên nhắn lỗi
      msgName: "" // tin nhắn lỗi
    };
  },
  /**
   * Lifecycle gán trước khi update thực hiện khi thay đổi mã thì thay đổi tên theo database
   */
  beforeUpdate() {
    this.dataAsset.forEach(element => {
      if (element.assetTypeId == this.dataItem.assetTypeId) {
        this.dataItem.assetTypeName = element.assetTypeName;
      }
      if (element.departmentId == this.dataItem.departmentId) {
        this.dataItem.departmentName = element.departmentName;
      }
    });
    if (this.dataItem.timeUse != null) {
      setTimeout(() => {
        this.dataItem.wearRate = ((1 / this.dataItem.timeUse) * 100).toFixed(2);
      }, 1000);
    }
    if (this.dataItem.originalPrice != null) {
      setTimeout(() => {
        this.dataItem.wearValue = (
          (this.dataItem.originalPrice * this.dataItem.wearRate) /
          100
        ).toFixed(2);
      }, 1000);
    }
  },
  methods: {
    submitForm(e) {
      // To prevent the form from submitting
      e.preventDefault();
      // return false;
    },
    /**
     * Sự kiện nút tắt và lưu form
     */
    addKeyForm(e) {
      if (this.isForm) {
        if (e.which == 27) {
          this.showOffForm();
        }
        // if (e.which == 13) {
        //   this.addDataAsset();
        // }
      }
    },
    /**
     * Format lại giá trên form
     */
    formatPrice(value) {
      if (value != null) {
        // Làm tròn rồi chèn dấu .
        let val = (value / 1).toFixed(0).replace(".", ",");
        return val.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ".");
      }
      return value;
    },
    /**
     * giới hạn kí tự khi nhập vào input mã
     */
    limitAssetCode() {
      // thông báo giới hạn kí tự
      if (this.dataItem.assetCode != null) {
        if (this.dataItem.assetCode.length == this.maxAssetCode) {
          this.$notify({
            group: "foo",
            title: "Cảnh báo",
            text:
              "Mã tài sản không được nhập quá " + this.maxAssetCode + " kí tự",
            type: "error"
          });
        }
        this.isCheckCode = false;
      }
      // Thông báo không được nhập kí tự đặc việt
      // Tự động replace
      var format = "<>@!#$%^&*()_+[]{}?:;|'\"\\,./~`=";
      for (let i = 0; i < format.length; i++) {
        if (this.dataItem.assetCode.indexOf(format[i]) > -1) {
          this.isCheckCode = true;
          this.msgCode = "Không được nhập kí tự đặc biệt";
          this.dataItem.assetCode = this.dataItem.assetCode.replace(
            /[^A-Za-z0-9]/,
            ""
          );
        }
      }
      // this.isCheckCode = false;
    },
    /**
     * giới hạn kí tự khi nhập vào input tên
     */
    limitAssetName() {
      // Thông báo giới hạn kí tự
      if (this.dataItem.assetName != null) {
        if (this.dataItem.assetName.length == this.maxAssetName) {
          this.$notify({
            group: "foo",
            title: "Cảnh báo",
            text:
              "Tên tài sản không được nhập quá " + this.maxAssetName + " kí tự",
            type: "error"
          });
        }
        this.isCheckName = false;
      }
      // Thông báo không được nhập kí tự đặc biệt
      // Tự động replace
      var format = "<>@!#$%^&*()_+[]{}?:;|'\"\\,./~`=";
      for (let i = 0; i < format.length; i++) {
        if (this.dataItem.assetName.indexOf(format[i]) > -1) {
          this.isCheckName = true;
          this.msgName = "Không được nhập kí tự đặc biệt";
          this.dataItem.assetName = this.dataItem.assetName.replace(
            /[^A-Za-z0-9]/,
            ""
          );
        }
      }
    },
    /**
     * Validate trường số : không được nhập chữ và max = 9
     * Thời gian sử dụng
     */
    timeUseNumber() {
      // Thông báo giới hạn kí tự
      if (this.dataItem.timeUse != null) {
        if (this.dataItem.timeUse.length == this.maxNumber) {
          this.$notify({
            group: "foo",
            title: "Cảnh báo",
            text:
              "Thởi gian sử dụng không được nhập quá " +
              this.maxNumber +
              " kí tự",
            type: "error"
          });
        }
      }
    },
    /**
     * Tỷ lệ hao mòn
     */
    wearRateNumber() {
      // Thông báo giới hạn kí tự
      if (this.dataItem.wearRate != null) {
        if (this.dataItem.wearRate.length == this.maxNumber) {
          this.$notify({
            group: "foo",
            title: "Cảnh báo",
            text:
              "Tỷ lệ hao mòn không được nhập quá " + this.maxNumber + " kí tự",
            type: "error"
          });
        }
      }
    },
    /**
     * Nguyên giá
     * format lại giá trị trên form
     */
    originalPriceNumber(e) {
      let valueIpnut = e.target.value;
      if (e.target.value == "") {
        this.dataItem.originalPrice = null;
      }
      // Thông báo giới hạn kí tự
      if (valueIpnut.length == this.maxPrice) {
        this.$notify({
          group: "foo",
          title: "Cảnh báo",
          text: "Nguyên giá không được vượt quá tỷ",
          type: "error"
        });
      }
      // Thông báo không được nhập chũ
      // format giá và replace chữ
      var numbers = "^[0-9,]+$";
      if (!valueIpnut.match(numbers)) {
        if (valueIpnut == "") {
          this.dataItem.originalPrice = null;
        } else {
          this.$notify({
            group: "foo",
            title: "Cảnh báo",
            text: "Trường này chỉ được nhập số",
            type: "error"
          });
          this.dataItem.originalPrice = parseInt(valueIpnut.replace(/\D/g, ""));
        }
      } else {
        this.dataItem.originalPrice = parseInt(e.target.value.replace(",", ""));
      }
    },
    /**
     * Gía trị hao mòn
     */
    wearValueNumber() {
      // Thông báo giới hạn kí tự
      if (this.dataItem.wearValue != null) {
        if (this.dataItem.wearValue.length == this.maxNumber) {
          this.$notify({
            group: "foo",
            title: "Cảnh báo",
            text:
              "Gía trị hao mòn không được nhập quá " +
              this.maxNumber +
              " kí tự",
            type: "error"
          });
        }
      }
      // Thông báo không được nhập chữ
      var numbers = /^[0-9]+$/;
      if (
        !this.dataItem.wearValue.match(numbers) &&
        this.dataItem.wearValue != ""
      ) {
        this.$notify({
          group: "foo",
          title: "Cảnh báo",
          text: "Trường này chỉ được nhập số",
          type: "error"
        });
      }
      this.dataItem.wearValue = this.dataItem.wearValue.replace(/\D/g, "");
    },
    /**
     * Tắt form và xóa dữ liệu item
     */
    showOffForm() {
      this.$store.dispatch("offForm");
      this.$emit("resetItem");
    },
    /**
     * Click vào nút lưu thực hiện thêm data khi id null và sửa data khi id khác null
     */
    // addDataAsset() {
    //   // kiểm tra nghiệp vụ nếu false thì thực hiện thêm hoặc sửa
    //   if (this.validateData.error) {
    //     this.$notify({
    //       group: "foo",
    //       title: "Cảnh báo",
    //       text: this.validateData.msg,
    //       type: "error"
    //     });
    //     // focus vào ô input
    //     switch (this.validateData.typeError) {
    //       case "code":
    //         this.$refs.code.focus();
    //         this.isCheckCode = true;
    //         this.msgCode = this.validateData.msg;
    //         break;
    //       case "name":
    //         this.$refs.name.focus();
    //         this.isCheckName = true;
    //         this.msgName = this.validateData.msg;
    //         break;
    //       case "department":
    //         this.$refs.department.focus();
    //         break;
    //       case "type":
    //         this.$refs.type.focus();
    //         break;
    //       default:
    //         break;
    //     }
    //   } else {
    //     //chuyển datetime từ "" -> null
    //     if (this.dataDate == "") {
    //       this.dataItem.increaseDate = null;
    //     }

    //     //  debugger // eslint-disable-line
    //     if (this.dataItem.assetId == null) {
    //       // debugger // eslint-disable-line
    //       // Thực hiện post
    //       axios
    //         .post("http://localhost:51888/api/v1/Assets", this.dataItem)
    //         .then(response => {
    //           if (!response.data.success) {
    //             this.$notify({
    //               group: "foo",
    //               title: "Lỗi",
    //               text: response.data.data.userMsg[0],
    //               type: "error"
    //             });
    //           } else {
    //             this.$notify({
    //               group: "foo",
    //               title: "Thành công",
    //               text: "Thêm mới thành công",
    //               type: "success"
    //             });
    //             this.showOffForm();
    //             location.reload();
    //           }
    //           console.log(response);
    //         })
    //         .catch(error => {
    //           this.$notify({
    //             group: "foo",
    //             title: "Lỗi",
    //             text:
    //               "Đã có lỗi xảy ra, vui lòng liên hệ MISA để được trợ giúp",
    //             type: "error"
    //           });
    //           console.log(error);
    //         });
    //     } else {
    //       // Thực hiện put
    //       let apiUrl =
    //         "http://localhost:51888/api/v1/Assets/" + this.dataItem.assetId;
    //       axios
    //         .put(apiUrl, this.dataItem)
    //         .then(response => {
    //           if (!response.data.success) {
    //             this.$notify({
    //               group: "foo",
    //               title: "Lỗi",
    //               text: response.data.data.userMsg[0],
    //               type: "error"
    //             });
    //           } else {
    //             this.$notify({
    //               group: "foo",
    //               title: "Thành công",
    //               text: "Cập nhật thành công",
    //               type: "success"
    //             });
    //             this.showOffForm();
    //             location.reload();
    //           }
    //           console.log(response);
    //         })
    //         .catch(error => {
    //           this.$notify({
    //             group: "foo",
    //             title: "Lỗi",
    //             text:
    //               "Đã có lỗi xảy ra, vui lòng liên hệ MISA để được trợ giúp",
    //             type: "error"
    //           });
    //           console.log(error);
    //         });
    //     }
    //   }
    // }
  },
  computed: {
    originalPrice() {
      let price = null;
      if (this.dataItem.originalPrice != null) {
        price = this.dataItem.originalPrice.toString()
        // // .toString()
        // .replace(/\B(?=(\d{3})+(?!\d))/g, ",");
      }
      return price;
    },
    /**
     * Lấy biến isForm từ store
     */
    isForm() {
      return this.$store.state.isForm;
    },
    /**
     * Validate dữ liệu trên form
     */
    validateData() {
      // debugger // eslint-disable-line
      let returnData = {
        error: false,
        msg: "",
        typeError: ""
      };
      //1. validate để trống
      //Để trống loại tài sản
      if (this.dataItem.assetTypeId == "") {
        returnData = {
          error: true,
          msg: "Vui lòng chọn loại tài sản",
          typeError: "type"
        };
      }
      // Để trống tên phòng ban
      if (this.dataItem.departmentId == "") {
        returnData = {
          error: true,
          msg: "Vui lòng chọn phòng ban tài sản",
          typeError: "department"
        };
      }
      // Để trống tên tài sản
      if (this.dataItem.assetName == null || this.dataItem.assetName == "") {
        returnData = {
          error: true,
          msg: "Vui lòng nhập tên tài sản",
          typeError: "name"
        };
      }
      // Để trống mã tài sản
      if (this.dataItem.assetCode == null || this.dataItem.assetCode == "") {
        returnData = {
          error: true,
          msg: "Vui lòng nhập mã tài sản",
          typeError: "code"
        };
      }
      return returnData;
    }
  },
  // async created() {
  //   /**
  //    * Gọi API lấy 1 tài sản theo id
  //    */
  //   if (this.itemTemp.assetId != null) {
  //     let urlApi =
  //       "http://localhost:51888/api/v1/Assets/" + this.itemTemp.assetId;
  //     const item = await axios.get(urlApi);
  //     this.dataItem = item.data;
  //   }
  //   window.addEventListener("keyup", this.addKeyForm);
  // }
};
</script>

<style scoped>
label {
  font-family: "GoogleSans-Thin";
  font-size: 13px;
  margin-bottom: 10px;
  font-weight: 100;
  margin-top: 10px;
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
  font-family: "GoogleSans-Thin";
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
</style>