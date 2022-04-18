import React from 'react'
import {connect} from 'react-redux'
import {loginAPI} from '../Store/actions/login'
import {Button}  from 'reactstrap'
import { faSpinner } from '@fortawesome/free-solid-svg-icons'
import {FontAwesomeIcon} from '@fortawesome/react-fontawesome'

// interface LoginTypes{
//     token: string,
//     InProgress: boolean,
//     loginAPI:(postData:any)=>any
// }

// class LoginComponent extends React.Component<LoginTypes> {
export class LoginComponent extends React.Component {
    // dispatch = useDispatch();
    
    constructor(props) {
        super(props);
        this.state = {
            message: ''
        }
    }

    doLogin=()=>{
        this.props.loginAPI({
            "UserName" : "ajit",
            "Password" : "cybage@123"
        });
    }

    componentDidUpdate(prevProps){

        if(this.props.token && prevProps.token != this.props.token){
            debugger
            this.setState({message:'Success'})
        }else if(this.props.InProgress == false && (!this.props.token || this.props.token == '')){
            debugger
            this.setState({message:'Failure'})
        }
    }

    render(){
        console.log(this.props)
    return(
        <div>
             <div>
                 Progress: {this.props.InProgress ? 'true' : 'false'}
             </div>
             {this.props.InProgress &&
             <div>
                <FontAwesomeIcon icon={faSpinner} spin className='fa-spinner'></FontAwesomeIcon>
             </div>
             } 
             <div>
                 Message: {this.state.message}
             </div>
             <Button onClick={() => this.doLogin()}>LOGIN</Button>
             <div>
                 token: {this.props.token}
             </div>
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

