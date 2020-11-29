import React, {useEffect} from "react";
import {Article, State} from "../reducers";
import {loadData} from "../actions";
import {connect} from "react-redux";

type LoadedProps = {
    loadData: () => void;
    articles: Article[]
}

const Post: React.FunctionComponent<LoadedProps> = ({loadData, articles}) => {

    useEffect(() => {
        loadData();
    })

    return (
        <ul>
            {
                articles.map(a =>
                    <li key={a.id}>{a.title}</li>
                )
            }
        </ul>
    );
}

const mapStateToProps = (state: State) => {
    return {
        articles: state.remoteArticles
    };
}

const mapDispatchToProps = (dispatch) => {
    return {
        loadData: () => dispatch(loadData())
    }
}

export default connect(mapStateToProps, mapDispatchToProps)(Post);
