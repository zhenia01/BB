import {Article, State} from "../reducers";
import React from "react";
import {connect} from "react-redux";

export type ArticleProps = {
    articles: Article[]
}

const ConnectedList: React.FunctionComponent<ArticleProps> = ({articles}) => {
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

const mapStateToProps = (state: State): ArticleProps  => {
    return {articles: state.articles};
}

const List = connect(mapStateToProps)(ConnectedList);


export default List;
