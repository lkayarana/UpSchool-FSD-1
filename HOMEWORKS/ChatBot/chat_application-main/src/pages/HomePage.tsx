import {useNavigate} from "react-router-dom";
import React, {useState} from "react";
import {Button, Header} from "semantic-ui-react";

function HomePage() {
    const navigate = useNavigate();

    const onJoinChatButtonClick = () => {
        navigate("/accounts");
    }
    const [text, setText] = useState('');

    const handleChange = (e) => {
        setText(e.target.value);
    };

    return (
        <div>
            <Header as='h1' textAlign='center' className="main-header">UpStorage Chat</Header>
            <p>Name</p>
            <input type="text" value={text} onChange={handleChange} />
            <p>Girilen metin: {text}</p>
            <Button color="blue" onClick={onJoinChatButtonClick}>Join Chat!</Button>
        </div>
    );
}