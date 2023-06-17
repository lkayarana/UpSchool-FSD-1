import React, {useContext, useEffect, useState} from "react";
import {Button, Divider, Grid, Header, Icon, Input, Segment, Select} from "semantic-ui-react";
import "./AccountsPage.css";
import AccountCard from "../components/AccountCard.tsx";
import {AccountsContext} from "../context/StateContext.tsx";
import api from "../utils/axiosInstance.ts";
import { PaginatedList} from "../types/GenericTypes.ts";
import {AccountGetAllDto} from "../types/AccountTypes.ts";
import {useNavigate} from "react-router-dom";
import {PictureOutlined, SendOutlined} from "@ant-design/icons";


const options = [
    { key: '1', text: 'Ascending', value: 'true' },
    { key: '2', text: 'Descending', value: 'false' }
];

/*export type AccountsPageProps = {

}*/

function AccountsPage( ) {

    const navigate = useNavigate();

    const [value, setValue] = useState<string>('');

    const handleChange = (event: React.ChangeEvent<HTMLInputElement>) => {
        setValue(event.target.value);

        isTyping({ props, chatId });
    };

    const onLeaveButtonClick = () => {
        navigate("/");
    }

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

    const { accounts, setAccounts } = useContext(AccountsContext);

    useEffect(() => {
        const fetchAccounts = async () => {

            const response = await api.get<PaginatedList<AccountGetAllDto>>("/Accounts");

            setAccounts(response.data.items);
        }

        fetchAccounts();

        return;

    },[]);



    const onPasswordVisibilityToggle = (id:string) => {
        // Create a new array with the same accounts, but with the showPassword property of the account with the given id toggled
        const updatedAccounts = accounts.map(account => {
            if (account.id === id) {
                // Toggle the showPassword property

                return {...account, ShowPassword: !account.showPassword};
            } else {
                // Leave the account unchanged
                return account;
            }
        });

        // Update the state with the new array
        setAccounts(updatedAccounts);
    }


    return (
        <header>UpStorage Chat</header>
        <button color="red" onClick={onLeaveButtonClick}>Leave</button>
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
}

export default AccountsPage;