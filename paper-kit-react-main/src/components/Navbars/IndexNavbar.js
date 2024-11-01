/*!

=========================================================
* Paper Kit React - v1.3.2
=========================================================

* Product Page: https://www.creative-tim.com/product/paper-kit-react

* Copyright 2023 Creative Tim (https://www.creative-tim.com)
* Licensed under MIT (https://github.com/creativetimofficial/paper-kit-react/blob/main/LICENSE.md)

* Coded by Creative Tim

=========================================================

* The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.

*/
import React, { useEffect, useState } from 'react';
import { jwtDecode } from 'jwt-decode';
import Swal from 'sweetalert2';
// nodejs library that concatenates strings
import classnames from "classnames";
// reactstrap components
import {
  Button,
  Collapse,
  NavbarBrand,
  Navbar,
  NavItem,
  NavLink,
  Nav,
  Container,
} from "reactstrap";
import { Link } from "react-router-dom";

function IndexNavbar() {
  const [navbarColor, setNavbarColor] = React.useState("navbar-transparent");
  const [navbarCollapse, setNavbarCollapse] = React.useState(false);
  const [decodedToken, setDecodedToken] = useState(null);

  const toggleNavbarCollapse = () => {
    setNavbarCollapse(!navbarCollapse);
    document.documentElement.classList.toggle("nav-open");
  };
  const getCookie = (cookieName) => {
    const cookies = document.cookie.split(';');
    for (let i = 0; i < cookies.length; i++) {
      const cookie = cookies[i].trim();
      if (cookie.startsWith(`${cookieName}=`)) {
        return cookie.substring(cookieName.length + 1);
      }
    }
    return null; // Return null if the cookie is not found
  };
  useEffect(() => {
    const updateNavbarColor = () => {
      if (
        document.documentElement.scrollTop > 299 ||
        document.body.scrollTop > 299
      ) {
        setNavbarColor("");
      } else if (
        document.documentElement.scrollTop < 300 ||
        document.body.scrollTop < 300
      ) {
        setNavbarColor("navbar-transparent");
      }
    };

    window.addEventListener("scroll", updateNavbarColor);
    const token = getCookie("Access_Token");
    console.log(token);
    if (token) {
      try {
        const decoded = jwtDecode(token); // Decode the token
        setDecodedToken(decoded); // Set the decoded token to state
      } catch (error) {
        console.error("Invalid token", error);
      }
    }    
    return function cleanup() {
      window.removeEventListener("scroll", updateNavbarColor);
    };
  }, []); 
  const handleLogout = async (event) => {
    event.preventDefault();

    // Call the login API
    try {
      const accessToken = getCookie("Access_Token");
      const response = await fetch("https://localhost:7162/api/Auth/signoutJWT", {
        method: "POST",
        credentials: 'include',
        headers: {
          'Authorization': `Bearer ${accessToken}`,
          "Content-Type": "application/json",
        },   
      });
      const result = await response.json();
      handleHttpResponse(result);    
       Swal.fire({
        toast: true,
        position: "top-end",
        title: "Login",
        text: "",
        icon: "success",
        showConfirmButton: false,
        timer: 3000,
        timerProgressBar: true,        
    });
      
      
    } catch (error) {    
        const cookies = document.cookie.split(";");      
        for (let i = 0; i < cookies.length; i++) {
          const cookie = cookies[i];
          const eqPos = cookie.indexOf("=");
          const name = eqPos > -1 ? cookie.slice(0, eqPos) : cookie;
          document.cookie = name + "=;expires=Thu, 01 Jan 1970 00:00:00 GMT;path=/";
        }     
      console.error("Error during login:", error);      
      // Swal.fire({
      //   title: 'Error!',
      //   text: "An error occurred during login.",
      //   icon: 'error'       
      // })
    }
  };
  const handleHttpResponse = (data) => {
    if (data.status) {
      console.log(data);   
    window.location.replace(`${window.location.protocol}//${window.location.host}/login-page`);  // Adjust this based on your response structure
      
    } else {
      Swal("Error!", data.info, "error");
    }
  };

  return (
    <Navbar className={classnames("fixed-top", navbarColor)} expand="lg">
      <Container>
        <div className="navbar-translate">
          <Link
                to="/index"                
              ><NavbarBrand>
                IMEI SOL
                </NavbarBrand>
              </Link>
          <button
            aria-expanded={navbarCollapse}
            className={classnames("navbar-toggler navbar-toggler", {
              toggled: navbarCollapse,
            })}
            onClick={toggleNavbarCollapse}
          >
            <span className="navbar-toggler-bar bar1" />
            <span className="navbar-toggler-bar bar2" />
            <span className="navbar-toggler-bar bar3" />
          </button>
        </div>
        <Collapse
          className="justify-content-end"
          navbar
          isOpen={navbarCollapse}
        >
          {decodedToken ? (
          <Nav navbar>
            <NavItem>
              <NavLink
                data-placement="bottom"
                href="https://twitter.com/CreativeTim?ref=creativetim"
                target="_blank"
                title="Follow us on Twitter"
              >
              SignUp
              </NavLink>
            </NavItem>
            <NavItem>
              <NavLink
                data-placement="bottom"
                href="https://www.facebook.com/CreativeTim?ref=creativetim"
                target="_blank"
                title="Like us on Facebook"
              >
              Packages
              </NavLink>
            </NavItem>
            <NavItem>
              <NavLink
                data-placement="bottom"
                href="https://www.instagram.com/CreativeTimOfficial?ref=creativetim"
                target="_blank"
                title="Follow us on Instagram"
              >Service
              </NavLink>
            </NavItem>            
            <NavItem>              
                <Link
              to="/profile-page"              
            ><NavLink>
              Dashboard
              </NavLink>
            </Link>             
            </NavItem>
            <NavItem>
              <Button
                className="btn-round"
                color="danger"
                to=""
                target="_blank"
              >
                <i className="nc-icon nc-spaceship"></i> {decodedToken.unique_name}
              </Button>
            </NavItem>
            <NavItem>
              <Button
                className="btn-round"
                color="danger"
                to="#"
                target="_blank"
                onClick={handleLogout}
              >
                <i className="nc-icon nc-button-power"></i>
              </Button>
            </NavItem>
          </Nav>
           ) : (
            <Nav navbar>
            <NavItem>
              <NavLink
                data-placement="bottom"
                href="https://twitter.com/CreativeTim?ref=creativetim"
                target="_blank"
                title="Follow us on Twitter"
              >
              SignUp
              </NavLink>
            </NavItem>
            <NavItem>
              <NavLink
                data-placement="bottom"
                href="https://www.facebook.com/CreativeTim?ref=creativetim"
                target="_blank"
                title="Like us on Facebook"
              >
              Packages
              </NavLink>
            </NavItem>
            <NavItem>
              <NavLink
                data-placement="bottom"
                href="https://www.instagram.com/CreativeTimOfficial?ref=creativetim"
                target="_blank"
                title="Follow us on Instagram"
              >Service
              </NavLink>
            </NavItem>
            <NavItem>
            <NavLink>
              <Link
                to="/login-page"
                style={{
                  color: "#FFFFFF",
                  borderColor: "#FFFFFF",
                  fontWeight: 600
                }}
              >
                Login
              </Link>
            </NavLink>
          </NavItem>
            <NavItem>
              <NavLink
                href="https://demos.creative-tim.com/paper-kit-react/#/documentation?ref=pkr-index-navbar"
                target="_blank"
              >Documentation
              </NavLink>
            </NavItem>
            <NavItem>
              <Button
                className="btn-round"
                color="danger"
                href="https://www.creative-tim.com/product/paper-kit-pro-react?ref=pkr-index-navbar"
                target="_blank"
              >
                <i className="nc-icon nc-spaceship"></i> Upgrade to Pro
              </Button>
            </NavItem>
          </Nav>
          )}
        </Collapse>
      </Container>
    </Navbar>
  );
}

export default IndexNavbar;
