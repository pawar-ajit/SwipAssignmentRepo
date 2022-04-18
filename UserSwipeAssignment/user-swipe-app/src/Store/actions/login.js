import {LOGIN_START,LOGIN_SUCCESS,LOGIN_FAILED} from './types'
import axios from 'axios'

export const loginAPI = (postData) => (dispatch) =>{
    console.log("login api called");
    dispatch({type:LOGIN_START});

    axios.post('http://localhost:59513/api/user/login',postData)
    .then((res)=>{
        console.log("result: ",res.data)
        dispatch({type:LOGIN_SUCCESS, payload:res.data});
    })
}