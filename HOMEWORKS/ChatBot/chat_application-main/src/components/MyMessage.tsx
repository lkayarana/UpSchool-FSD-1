import React from 'react';

interface MyMessageProps {
    message: {
        attachments?: {
            file: string;
        }[];
        text: string;
    };
}

const MyMessage: React.FC<MyMessageProps> = ({ message }) => {
    if (message.attachments && message.attachments.length > 0) {
        return (
            <img
                src={message.attachments[0].file}
                alt="message-attachment"
                className="message-image"
                style={{ float: 'right' }}
            />
        );
    }

    return (
        <div className="message" style={{ float: 'right', marginRight: '18px', color: 'white', backgroundColor: '#3B2A50' }}>
            {message.text}
        </div>
    );
};

export default MyMessage;