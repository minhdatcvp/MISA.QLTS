import Vue from 'vue'
import Vuex from 'vuex'

Vue.use(Vuex)

export const store = new Vuex.Store({
    state : {
        isActiveMenu : true, // Biến thực hiện ẩn hiện MenuBar - true : ẩn, false: hiện
        isForm : false, // Bến thực hiện ẩn hiện Form - true : hiện , false : ẩn
    },

    mutations:{
        //hien thi ra menu
        ON_MENU(state){
            state.isActiveMenu = false;
        },
        //an menu ra menu
        OFF_MENU(state){
            state.isActiveMenu = true;
        },
        //hien thi ra form
        ON_FORM(state){
            state.isForm = true;
        },
        //an FORM
        OFF_FORM(state){
            state.isForm = false;
        },

    },
    
    actions : {
        // hàm action thực hiện ON_MENU 
        onMenu : ({commit}) => {
            commit('ON_MENU');
        },
        // hàm action thực hiện OFF_MENU 
        offMenu : ({commit}) => {
            commit('OFF_MENU');
        },
        // hàm action thực hiện ON_FORM
        onForm : ({commit}) => {
            commit('ON_FORM');
        },
        // hàm action thực hiện OFF_FORM
        offForm : ({commit}) => {
            commit('OFF_FORM');
        }
    }
})