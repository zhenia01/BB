import {ADD_ARTICLE, DATA_LOADED} from "../constants/action-types";
import {Article} from "../reducers";

export type Action = {
    type: string,
    payload: Article
}

export function addArticle(payload: Article): Action {
    return {
        type: ADD_ARTICLE,
        payload
    };
}

export function loadData(): (dispatch) => Promise<void> {
    return function (dispatch) {
        return fetch("https://jsonplaceholder.typicode.com/posts")
            .then(response => response.json())
            .then(json => {
                dispatch({type: DATA_LOADED, payload: json});
            });
    }
}
