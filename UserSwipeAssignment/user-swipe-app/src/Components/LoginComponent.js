import React from 'react'
import {connect} from 'react-redux'
import {loginAPI} from '../Store/actions/login'
import {Button}  from 'reactstrap'

// interface LoginTypes{
//     token: string,
//     InProgress: boolean,
//     loginAPI:(postData:any)=>any
// }

// class LoginComponent extends React.Component<LoginTypes> {
export class LoginComponent extends React.Component {

    render(){
    return(
        <div>
             login page!!!
        </div>
        )
    }

}

function mapStateToProps(state){
    return{
        token: state.loginReducer.token,
        InProgress: state.loginReducer.InProgress
    }
}

function mapDispatchToProps(dispatch) {
    return{
        loginAPI: (postData) => dispatch(loginAPI(postData))   
    }   
}

export default connect(mapStateToProps, mapDispatchToProps)(LoginComponent);

