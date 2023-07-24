import { useContext, useEffect } from "react";
import { useNavigate, useSearchParams } from "react-router-dom"
import { getClaimsFromJwt } from "../utils/jwtHelper";
import { LocalJwt } from "../types/AuthTypes";
import { AppUserContext } from "../context/StateContext";


export default function SocialLogin() {

    const [searchParams] = useSearchParams();

    const navigate = useNavigate();

    const { setAppUser } = useContext(AppUserContext);

    useEffect(() => {

        const accessToken = searchParams.get("access_token");

        const expiryDate = searchParams.get("expiry_date");

        const { uid, email, given_name, family_name } = getClaimsFromJwt(accessToken);

        const expires: string = expiryDate;

        setAppUser({ id: uid, email, firstName: given_name, lastName: family_name, expires, accessToken });

        const localJwt: LocalJwt = {
            accessToken,
            expires
        }

        localStorage.setItem("upcrawler_user", JSON.stringify(localJwt));

        navigate("/");


    }, []);

    return (
        <div></div>
    );
}