import { createContext } from "react";
import { AuthRegisterCommand, LocalUser } from "../types/AuthTypes";

export type AppUserContextType = {
    appUser:LocalUser | undefined,
    setAppUser:React.Dispatch<React.SetStateAction<LocalUser | undefined>>,
}

export const AppUserContext = createContext<AppUserContextType>({

    appUser:undefined,

    setAppUser: () => {},

});


export type RegisterUserContextType = {
    registerUser:AuthRegisterCommand | undefined,
    setRegisterUser:React.Dispatch<React.SetStateAction<AuthRegisterCommand | undefined>>,
}

export const RegisterUserContext = createContext<{
    registerUser: LocalUser | undefined;
    setRegisterUser: React.Dispatch<React.SetStateAction<LocalUser | undefined>>;
}>({
    registerUser: undefined,
    setRegisterUser: () => undefined,
});