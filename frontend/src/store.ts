import {createStore, compose, applyMiddleware} from "redux";
import rootReducer from "./reducers/index"
import thunk from "redux-thunk";
import {forbiddenWordsMiddleware} from "./middlewares";

declare global {
    interface Window { // eslint-disable-line
        // eslint-disable-next-line no-undef
        __REDUX_DEVTOOLS_EXTENSION_COMPOSE__?: typeof compose;
    }
}

const storeEnhancers = window.__REDUX_DEVTOOLS_EXTENSION_COMPOSE__ || compose;

const store = createStore(
    rootReducer,
    storeEnhancers(applyMiddleware(forbiddenWordsMiddleware, thunk)));

export default store;
