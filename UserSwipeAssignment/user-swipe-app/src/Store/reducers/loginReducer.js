import {LOGIN_START,LOGIN_SUCCESS,LOGIN_FAILED} from '../actions/types'

const initialState = {
    token :'',
    InProgress: true
}
console.log('in login reducer')
export default function (state = initialState, action) {

    switch (action.type) {
                     
        default:
            return state
    }

}

