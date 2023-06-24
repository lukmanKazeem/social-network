import React, { Fragment, useEffect, useState } from 'react';
import { useHistory, Link } from "react-router-dom";

export default function UserHeader() {
    const history = useHistory();

    const [username, setUserName] = useState("");

    useEffect(() => {
        setUserName(localStorage.getItem("username"));
    }, []);
}