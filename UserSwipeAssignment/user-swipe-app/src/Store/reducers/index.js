import { combineReducers } from 'redux';
import loginReducer from './loginReducer';

console.log('in reducer')
export default combineReducers({
    loginReducer:loginReducer
})