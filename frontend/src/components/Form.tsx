import React from "react";
import {Article} from "../reducers";
import {connect} from "react-redux";
import {addArticle} from "../actions";

type ActionProps = {
    addArticle: (_: Article) => void;
}

const Form: React.FunctionComponent<ActionProps> = ({addArticle}) => {
    return (
        <button onClick={() => addArticle({title: "lel", id: 5})}>Add Article</button>
    );
}

const mapDispatchToProps = (dispatch): ActionProps => {
    return {
      addArticle: (a: Article) => dispatch(addArticle(a))
    };
}

export default connect(null, mapDispatchToProps)(Form);
