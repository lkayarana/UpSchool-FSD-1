interface Props {
    chats: any;
    activeChat: any;
    userName: any;
    messages: any;
}

const ChatFeed: React.FC<Props> = (props) => {
    const { chats, activeChat, userName, messages } = props;

    const chat = chats && chats[activeChat];

    const renderReadReceipts = (message: any, isMyMessage: any) => {
        return chat?.people?.map((person: any, index: number) => {
            return (
                person?.lastread === message.id && (
                    <div
                        key={`read${index}`}
                        className="read-receipt"
                        style={{
                            float: isMyMessage ? 'right' : 'left',
                            backgroundImage: person.person.avatar && `url(${person.person.avatar})`,
                        }}
                    />
                )
            );
        });
    };

    const renderMessages = () => {
        const keys = Object.keys(messages);

        return keys.map((key, index) => {
            const message = messages[key];
            const lastMessageKey = index === 0 ? null : keys[index - 1];
            const isMyMessage = userName === message.sender.username;

            return (
                <div key={`msg_${index}`} style={{ width: '100%' }}>
                    <div className="message-block">
                        {isMyMessage ? (
                            <MyMessage message={message} />
                        ) : (
                            <TheirMessage message={message} lastMessage={messages[lastMessageKey]} />
                        )}
                    </div>
                    <div
                        className="read-receipts"
                        style={{
                            marginRight: isMyMessage ? '18px' : '0px',
                            marginLeft: isMyMessage ? '0px' : '68px',
                        }}
                    >
                        {renderReadReceipts(message, isMyMessage)}
                    </div>
                </div>
            );
        });
    };

    if (!chat) return <div />;

    return (
        <div className="chat-feed">
            <div className="chat-title-container">
                <div className="chat-title">{chat?.title}</div>
                <div className="chat-subtitle">
                    {chat?.people?.map((person: any) => {
                        return <span key={person.person.username}>{person.person.username}</span>;
                    })}
                </div>
            </div>
            {renderMessages()}
            <div style={{ height: '100px' }} />
            <div className="message-form-container">
                <MessageForm {...props} chatId={activeChat} />
            </div>
        </div>
    );
};

export default ChatFeed;
