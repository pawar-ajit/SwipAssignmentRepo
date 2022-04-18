import {LOGIN_START,LOGIN_SUCCESS,LOGIN_FAILED} from '../actions/types'

const initialState = {
    token :'',
    InProgress: false
}
console.log('in login reducer')
export default function (state = initialState, action) {

    switch (action.type) {
        case LOGIN_START:
            console.log("reducer", action.type);
            return {
                ...state,
                token: '',
                InProgress: true
            }
        case LOGIN_SUCCESS:
            console.log("reducer", action.type);
            console.log("reducer", action.payload);
            return {
                ...state,
                token: action.payload,
                InProgress: false
            }
        case LOGIN_FAILED:
            console.log("reducer", action.type);
            return {
                ...state,
                token: '',
                InProgress: false
            }                
        default:
            return state
    }

}

