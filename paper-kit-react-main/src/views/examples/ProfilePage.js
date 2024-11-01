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
import { getCookie } from "views/Index";
import { useNavigate } from "react-router-dom"; 
import Swal from 'sweetalert2';
// reactstrap components
import {
  Button,
  Label,
  FormGroup,
  Input,
  NavItem,
  NavLink,
  Nav,
  TabContent,
  TabPane,
  Container,
  Row,
  Col,
} from "reactstrap";


// core components
import ExamplesNavbar from "components/Navbars/ExamplesNavbar.js";
import ProfilePageHeader from "components/Headers/ProfilePageHeader.js";
import DemoFooter from "components/Footers/DemoFooter.js";
import HistoryTable from 'views/index-sections/SectionTable';

function ProfilePage() {
  const [activeTab, setActiveTab] = React.useState("1");
  const [userFetchedData, setuserFetchedData] = useState(null);
  const navigate = useNavigate();
  const toggle = (tab) => {
    if (activeTab !== tab) {
      setActiveTab(tab);
    }
  };
  const fetchUserData = async () => {
    
    try {
      const accessToken = getCookie("Access_Token");
     // console.log(accessToken);
      const response = await fetch("https://localhost:7162/api/Auth/fetchuserbyid", {
        method: "GET",
        credentials: 'include',
        headers: {
          'Authorization': `Bearer ${accessToken}`,
          "Content-Type": "application/json",
        },   
      });
      const result = await response.json();
      //console.log(result);
      setuserFetchedData(result);
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
      console.error("Error during Data reterival:", error);   
      navigate("/login-page");
      Swal.fire({
        title: 'Error!',
        text: "An error occurred during login.",
        icon: 'error'       
      })
    }
  };
  const handleHttpResponse = (data) => {
    if (data.status) {
      //console.log(data);            
     
    } else {
      Swal("Error!", data.info, "error");
    }
  };
  document.documentElement.classList.remove("nav-open");
  useEffect(() => {
    fetchUserData();
    document.body.classList.add("landing-page");   
    const token = getCookie("Access_Token");
    //console.log(token);
    if (token) {
      try {
        const decoded = jwtDecode(token); // Decode the token
       // console.log(decoded.nameid) // Set the decoded token to state
      } catch (error) {
        console.error("Invalid token", error);
      }
    }  
    return function cleanup() {
      document.body.classList.remove("landing-page");
    }; 
    
  }, []); 
 // console.log(userFetchedData);  
  return (
    <>
      <ExamplesNavbar />
      <ProfilePageHeader />
      <div className="section profile-content">
      {userFetchedData ? ( 
        <Container>        
          <div className="owner">
            <div className="avatar">
              <img
                alt="..."
                className="img-circle img-no-padding img-responsive"
                src={require("assets/img/ryan.jpg")}
              />
            </div>
            {/* const userNameParts = item.userR_NAME.split(" / ");
        const capitalizedFirstName = userNameParts[0].charAt(0).toUpperCase() + userNameParts[0].slice(1); */}

            <div className="name">
              <h4 className="title">
                {userFetchedData.info.useR_NAME.split(" / ")[0].charAt(0).toUpperCase()+userFetchedData.info.useR_NAME.split(" / ")[0].slice(1)} <br />
              </h4>
              <h6 className="description">{userFetchedData.info.useR_NAME.split(" / ")[1]}</h6>
            </div>
          </div>
          <Row>
            <Col className="ml-auto mr-auto text-center" md="6">
              <p> 
                <strong>     {userFetchedData.info.rights}</strong>
           
              </p>
              <br />
              <Button className="btn-round" color="default" outline>
                <i className="fa fa-cog" /> Settings
              </Button>
            </Col>
          </Row>
          <br />
          <div className="nav-tabs-navigation">
            <div className="nav-tabs-wrapper">
              <Nav role="tablist" tabs>
                <NavItem>
                  <NavLink
                    className={activeTab === "1" ? "active" : ""}
                    onClick={() => {
                      toggle("1");
                    }}
                  >
                    Balance
                  </NavLink>
                </NavItem>
                <NavItem>
                  <NavLink
                    className={activeTab === "2" ? "active" : ""}
                    onClick={() => {
                      toggle("2");
                    }}
                  >
                    Summary
                  </NavLink>
                  
                </NavItem>
                <NavItem>
                  <NavLink
                    className={activeTab === "3" ? "active" : ""}
                    onClick={() => {
                      toggle("3");
                    }}
                  >
                    IMEI History
                  </NavLink>
                  
                </NavItem>
              </Nav>
            </div>
          </div>
          {/* Tab panes */}
          <TabContent className="following" activeTab={activeTab}>
            <TabPane tabId="1" id="follows">
              <Row>
                <Col className="ml-auto mr-auto" md="6">
                  <ul className="list-unstyled follows">
                  {userFetchedData.balance.map((item, index) => (
                    <li>                    
                        <Row key={index}>
                          <Col className="ml-auto mr-auto" lg="2" md="4" xs="4">
                            <img
                              alt="..."
                              className="img-circle img-no-padding img-responsive"
                              src={require("assets/img/package.png")}
                            />
                          </Col>
                          <Col className="ml-auto mr-auto" lg="7" md="4" xs="4">
                            <h6>
                              {item.packagE_NAME} <br />
                              <large>Balance: ${item.balance}</large>
                            </h6>
                          </Col>
                          <Col className="ml-auto mr-auto" lg="3" md="4" xs="4">
                            <FormGroup check>
                              <Label check>
                                <Input
                                  defaultChecked
                                  defaultValue=""
                                  type="checkbox"
                                />
                                <span className="form-check-sign" />
                              </Label>
                            </FormGroup>
                          </Col>
                        </Row>                     
                    </li>
                     ))} 
                    <hr />                                    
                  </ul>

                </Col>
              </Row>
            </TabPane>
            <TabPane className="text-center" tabId="2" id="following">
              <h2 className="text-bold">Total IMEI Searched: $ {userFetchedData.info.useR_TOTAL_IMEI}</h2>
                <h2 className="text-muted">Total Balance: $ {userFetchedData.info.totaL_BALANCE} </h2>
                <h2 className="text-muted">Total Spendings: $ {userFetchedData.info.userS_EXPENDITURE} </h2>
              <Button className="btn-round" color="warning">
                Find artists
              </Button>
            </TabPane>
            <TabPane className="text-center" tabId="3" id="following">
            <HistoryTable/>
            </TabPane>
          </TabContent>
        
        </Container>
        ):(null)}
      </div>
      
     
      <DemoFooter />
    </>
  );
}

export default ProfilePage;
