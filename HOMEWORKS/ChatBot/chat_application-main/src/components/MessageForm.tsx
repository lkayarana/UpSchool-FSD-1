import React, { useState } from 'react';
import { SendOutlined, PictureOutlined } from '@ant-design/icons';
import { sendMessage, isTyping } from 'react-chat-engine';

interface Props {
    chatId: number;
    creds: string;
}



const MessageForm: React.FC<Props> = ({ chatId, creds }) => {
    const [value, setValue] = useState<string>('');

    const handleChange = (event: React.ChangeEvent<HTMLInputElement>) => {
        setValue(event.target.value);

        isTyping({ props, chatId });
    };

    const handleSubmit = (event: React.FormEvent<HTMLFormElement>) => {
        event.preventDefault();

        const text = value.trim();

        if (text.length > 0) {
            sendMessage(creds, chatId, { text });
        }

        setValue('');
    };

    const handleUpload = (event: React.ChangeEvent<HTMLInputElement>) => {
        sendMessage(creds, chatId, { files: event.target.files, text: '' });
    };

    return (
        <form className="message-form" onSubmit={handleSubmit}>
            <input
                className="message-input"
                placeholder="Send a message..."
                value={value}
                onChange={handleChange}
                onSubmit={handleSubmit}
            />
            <label htmlFor="upload-button">
        <span className="image-button">
          <PictureOutlined className="picture-icon" />
        </span>
            </label>
            <input
                type="file"
                multiple={false}
                id="upload-button"
                style={{ display: 'none' }}
                onChange={handleUpload.bind(this)}
            />
            <button type="submit" className="send-button">
                <SendOutlined className="send-icon" />
            </button>
        </form>
    );
};

export default MessageForm;