import React from 'react';
import './App.css';
import Form from "./components/Form";
import Post from "./components/Post";

const App = () => {

    return (
        <>
            <div>
                <h2>Articles</h2>
                <Post />
            </div>
            <hr/>
            <div>
                <Form/>
            </div>
        </>
    );
}

export default App;
