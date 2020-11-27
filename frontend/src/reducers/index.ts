import {ADD_ARTICLE, DATA_LOADED} from "../constants/action-types";
import {Action} from "../actions";

export type Article = {
    title: string,
    id: number
}

export type State = {
    articles: Article[],
    remoteArticles: Article[]
}

const initialState: State = {
    articles: [],
    remoteArticles: []
}

function rootReducer(state: State = initialState, action: Action): State {
    if (action.type === ADD_ARTICLE) {
        return Object.assign({}, state, {
            articles: state.articles.concat(action.payload)
        })
    }
    if (action.type === DATA_LOADED) {
        return Object.assign({}, state, {
            remoteArticles: state.remoteArticles.concat(action.payload)
        });
    }
    return state;
}

export default rootReducer;
