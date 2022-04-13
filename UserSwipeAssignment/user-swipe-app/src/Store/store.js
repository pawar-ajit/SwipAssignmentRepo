import thunk from 'redux-thunk';
import { createStore, applyMiddleware, compose } from 'redux';
import rootReducer from './reducers/index';
import { composeWithDevTools } from 'redux-devtools-extension';

console.log("Store");

export default function configureStore() {
    const initialState = {};
    const middlewares = [thunk];
    const middlewareEnhancer = applyMiddleware(...middlewares);
    const enhancers = [middlewareEnhancer];
    const composedEnhancer = composeWithDevTools(...enhancers);
    const store = createStore(
        rootReducer,
        initialState,
        composedEnhancer
    );
    console.log(store);
    return store;
}
