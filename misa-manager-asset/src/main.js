import Vue from 'vue'
import App from './App.vue'
// import router from './router'
import { store } from './store/index'
import Notifications from 'vue-notification'
import vSelect from 'vue-select'
import VueMask from 'v-mask'

Vue.use(VueMask)
Vue.component('v-select', vSelect)
Vue.use(Notifications)
Vue.config.productionTip = false

new Vue({
    // khai báo store.
    store,
    render: h => h(App)
}).$mount('#app')