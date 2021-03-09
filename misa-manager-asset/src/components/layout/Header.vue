<template>
  <!-- Phần header dùng chung  -->
  <header>
    <div class="title">
      <p><span>Tài sản</span> / Ghi giảm</p>
    </div>
    <div class="infomation">
      <p>Sở tài chính</p>
      <div class="date-time">
        <input
          type="month"
          id="start"
          name="start"
          min="2018-03"
          value="2021-02"
        />
      </div>
      <div class="help">
        <svg
          xmlns="http://www.w3.org/2000/svg"
          width="16"
          height="16"
          viewBox="0 0 18 18"
        >
          <g
            id="Group_11747"
            data-name="Group 11747"
            transform="translate(1 1)"
          >
            <g id="Group_5955" data-name="Group 5955">
              <circle
                id="Ellipse_372"
                data-name="Ellipse 372"
                class="cls-1"
                cx="8"
                cy="8"
                r="8"
              />
              <path
                id="Path_7308"
                data-name="Path 7308"
                class="cls-2"
                d="M1.9-2.426a2.374,2.374,0,0,1,.243-1.2,4.03,4.03,0,0,1,.946-.926,3.092,3.092,0,0,0,.665-.732A1.607,1.607,0,0,0,4-6.164a1.189,1.189,0,0,0-.284-.844,1.055,1.055,0,0,0-.812-.3,1.093,1.093,0,0,0-.735.258.955.955,0,0,0-.308.756H.2L.188-6.334A2.02,2.02,0,0,1,.929-8.057a3.1,3.1,0,0,1,1.978-.6,2.988,2.988,0,0,1,2.06.665,2.3,2.3,0,0,1,.747,1.808A2.376,2.376,0,0,1,5.276-4.8,3.9,3.9,0,0,1,4.184-3.744a1.4,1.4,0,0,0-.469.554,2.116,2.116,0,0,0-.111.765ZM3.615,0H1.893V-1.465H3.615Z"
                transform="translate(5.312 12.154)"
              />
            </g>
          </g>
        </svg>
      </div>
      <div class="bell">
        <svg
          xmlns="http://www.w3.org/2000/svg"
          width="15"
          height="15"
          viewBox="0 0 17.005 16.5"
        >
          <g
            id="Group_5690"
            data-name="Group 5690"
            transform="translate(1.009 1)"
          >
            <path
              id="Path_7211"
              data-name="Path 7211"
              class="cls-1"
              d="M6.5,16.5V12A5.5,5.5,0,0,1,12,6.5h0A5.5,5.5,0,0,1,17.5,12v4.5"
              transform="translate(-4.507 -6.5)"
            />
            <path
              id="Path_7212"
              data-name="Path 7212"
              class="cls-1"
              d="M13.5,19.5a1.5,1.5,0,0,1-3,0"
              transform="translate(-4.507 -6.5)"
            />
            <g
              id="Group_5689"
              data-name="Group 5689"
              transform="translate(0 10)"
            >
              <path
                id="Path_7213"
                data-name="Path 7213"
                class="cls-1"
                d="M17.5,16.5a3,3,0,0,0,1.947,2.81.071.071,0,0,1-.026.137H4.579a.071.071,0,0,1-.026-.137A3,3,0,0,0,6.5,16.5"
                transform="translate(-4.507 -16.5)"
              />
            </g>
          </g>
        </svg>
      </div>
      <div class="divider">
        <svg
          xmlns="http://www.w3.org/2000/svg"
          width="1"
          height="22"
          viewBox="0 0 1 22"
        >
          <rect id="Divider" class="cls-1" width="1" height="22" />
        </svg>
      </div>
      <img src="../../assets/avatar.png" alt="" />
      <div class="arrow-up">
        <svg
          id="i_arrow_up_small"
          xmlns="http://www.w3.org/2000/svg"
          width="8"
          height="4"
          viewBox="0 0 8 4"
        >
          <path id="Rectangle_54_copy_4" class="cls-3" d="M8,0H0L4,4Z" />
        </svg>
      </div>
    </div>
  </header>
</template>

<script>
export default {
  data() {
    return {};
  },
};
</script>

<style scoped>
header {
  display: flex;
  justify-content: space-between;
  border-bottom: 1px solid #e2e2e2;
}
.cls-1 {
  fill: none;
  stroke: #7b7a8d;
  stroke-linecap: round;
  stroke-miterlimit: 10;
  stroke-width: 2px;
}
.cls-2 {
  fill: #7b7a8d;
}
.cls-3 {
  fill: #011239;
}
.infomation img {
  margin-left: 20px;
  border-radius: 50%;
  border: 1px solid #ffffff;
  background-color: #000000;
  width: 31px;
  height: 31px;
  overflow: hidden;
}
.help,
.bell,
.divider {
  margin-top: 4px;
  margin-left: 25px;
  cursor: pointer;
}
</style>