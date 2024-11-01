import React, { useEffect, useState } from "react";
import { useNavigate } from "react-router-dom";
import { Button, Card, Form, Input, Container, Row, Col } from "reactstrap";
import ExamplesNavbar from "components/Navbars/ExamplesNavbar.js";
import Swal from 'sweetalert2'; // Make sure to install sweetalert if you haven't already


function LoginPage() {
  const navigate = useNavigate();
  const [ip, setIp] = useState("");
  const [username, setUsername] = useState("");
  const [password, setPassword] = useState("");
  const [isButtonDisabled, setIsButtonDisabled] = useState(true);
  const [errorMessage, setErrorMessage] = useState("");
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
    
      console.log(`Interval tick at: ${new Date().toLocaleTimeString()}`); // 
      if (getCookie("Access_Token")) {
        // Redirect to login page if the Access_Token cookie is missing
        navigate("/index");
      }
    
    document.documentElement.classList.remove("nav-open");
    document.body.classList.add("login-page");
    
    
    
    // Fetch user public IP
    const fetchIp = async () => {
      try {
        const response = await fetch("https://api.ipify.org?format=json");
        const data = await response.json();
        setIp(data.ip);
        if (data.ip) setIsButtonDisabled(false);
      } catch (error) {
        console.error("Error fetching IP:", error);
      }
    };

    fetchIp();

    return () => {
      document.body.classList.remove("login-page");
    };
  }, [navigate]);

  const handleLogin = async (event) => {
    event.preventDefault();

    const loginData = {
      seQ_NUM: 0,
      Email: username,        // Assuming `username` corresponds to `email`
      password: password,      // Pass the `password` directly
      isVerified: true,        // Assuming user is verified by default
      isLogin: true,           // Assuming this is for login, so setting to true
      firstName: "string",           // Placeholder, adjust as necessary
      lastName: "string",            // Placeholder, adjust as necessary
      userIp: ip,              // Maps to `ip` field
      rights: "1"           // Assuming a default rights level; adjust if needed
    };
    
    const validationError = validateForm(loginData);
    console.log(loginData);
    if (validationError) {
      setErrorMessage(validationError);
      setTimeout(() => setErrorMessage(""), 3000);
      return;
    }

    // Call the login API
    try {
      const response = await fetch("https://localhost:7162/api/Auth/login1", {
        method: "POST",
        credentials: 'include',
        headers: {
          "Content-Type": "application/json",
        },
        
        body:  JSON.stringify({seQ_NUM: 0,
          Email: username,        // Assuming `username` corresponds to `email`
          password: password,      // Pass the `password` directly
          isVerified: true,        // Assuming user is verified by default
          isLogin: true,           // Assuming this is for login, so setting to true
          firstName: "string",           // Placeholder, adjust as necessary
          lastName: "string",            // Placeholder, adjust as necessary
          userIp: ip,              // Maps to `ip` field
          rights: "1"}) ,
      });
      const result = await response.json();
      handleHttpResponse(result);
      const accessToken = getCookie("Access_Token");
      console.log("Access Token:", accessToken);
    } catch (error) {
      console.error("Error during login:", error);      
      Swal.fire({
        title: 'Error!',
        text: "An error occurred during login.",
        icon: 'error'       
      })
    }
  };

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
      console.error("Error during login:", error);      
      Swal.fire({
        title: 'Error!',
        text: "An error occurred during login.",
        icon: 'error'       
      })
    }
  };
  
  const handleHttpResponse = (data) => {
    if (data.status) {
      console.log(data);     
      
      window.location.href = "/index"; // Adjust this based on your response structure
      
    } else {
      Swal("Error!", data.info, "error");
    }
  };

  const validateForm = (formData) => {
    const errorMessage = ["Enter Your Email", "Enter Your Password"];
    for (let i = 0; i < formData.length; i++) {
      if (!formData[i]) {
        return errorMessage[i];
      }
    }
    return null; // No error
  };

  return (
    <>
      <ExamplesNavbar />
      <div
        className="page-header"
        style={{
          backgroundImage: "url(" + require("assets/img/login-image.jpg") + ")",
        }}
      >
        <div className="filter" />
        <Container>
          <Row>
            <Col className="ml-auto mr-auto" lg="4">
              <Card className="card-register ml-auto mr-auto">
                <h3 className="title mx-auto">Welcome</h3>
                <div className="social-line text-center">
                  <Button
                    className="btn-neutral btn-just-icon mr-1"
                    color="facebook"
                    href="#pablo"
                    onClick={(e) => e.preventDefault()}
                  >
                    <i className="fa fa-facebook-square" />
                  </Button>
                  <Button
                    className="btn-neutral btn-just-icon mr-1"
                    color="google"
                    href="#pablo"
                    onClick={(e) => e.preventDefault()}
                  >
                    <i className="fa fa-google-plus" />
                  </Button>
                  <Button
                    className="btn-neutral btn-just-icon"
                    color="twitter"
                    href="#pablo"
                    onClick={(e) => e.preventDefault()}
                  >
                    <i className="fa fa-twitter" />
                  </Button>
                </div>
                <Form className="register-form" onSubmit={handleLogin}>
                  <label>Email</label>
                  <Input
                    placeholder="Email"
                    type="text"
                    value={username}
                    onChange={(e) => setUsername(e.target.value)}
                  />
                  <label>Password</label>
                  <Input
                    placeholder="Password"
                    type="password"
                    value={password}
                    onChange={(e) => setPassword(e.target.value)}
                  />
                  <Button block className="btn-round" color="danger" onClick={handleLogin} disabled={isButtonDisabled}>
                    Login
                  </Button>
                  <Button block className="btn-round" color="danger" onClick={handleLogout} disabled={isButtonDisabled}>
                    Logout
                  </Button>
                  {errorMessage && <div className="text-danger">{errorMessage}</div>}
                </Form>
                <div className="forgot">
                  <Button
                    className="btn-link"
                    color="danger"
                    href="#pablo"
                    onClick={(e) => e.preventDefault()}
                  >
                    Forgot password?
                  </Button>
                </div>
              </Card>
            </Col>
          </Row>
        </Container>
        <div className="footer register-footer text-center">
          <h6>
            © {new Date().getFullYear()}, made with{" "}
            <i className="fa fa-heart heart" /> by ABS
          </h6>
        </div>
      </div>
    </>
  );
}

export default LoginPage;
