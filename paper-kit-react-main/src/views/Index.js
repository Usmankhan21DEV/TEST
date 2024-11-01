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
import React, { useEffect } from "react";
import { useNavigate } from "react-router-dom"; // Import useNavigate for navigation

// core components
import IndexNavbar from "components/Navbars/IndexNavbar.js";
import IndexHeader from "components/Headers/IndexHeader.js";
import DemoFooter from "components/Footers/DemoFooter.js";

// index sections
import SectionButtons from "views/index-sections/SectionButtons.js";
import SectionNavbars from "views/index-sections/SectionNavbars.js";
import SectionNavigation from "views/index-sections/SectionNavigation.js";
import SectionProgress from "views/index-sections/SectionProgress.js";
import SectionNotifications from "views/index-sections/SectionNotifications.js";
import SectionTypography from "views/index-sections/SectionTypography.js";
import SectionJavaScript from "views/index-sections/SectionJavaScript.js";
import SectionCarousel from "views/index-sections/SectionCarousel.js";
import SectionNucleoIcons from "views/index-sections/SectionNucleoIcons.js";
import SectionDark from "views/index-sections/SectionDark.js";
import SectionLogin from "views/index-sections/SectionLogin.js";
import SectionExamples from "views/index-sections/SectionExamples.js";
import SectionDownload from "views/index-sections/SectionDownload.js";
import HistoryTable from "./index-sections/SectionTable";

// Utility function to get a cookie value by name
export function getCookie(name) {
  const value = `; ${document.cookie}`;
  const parts = value.split(`; ${name}=`);
  if (parts.length === 2) return parts.pop().split(";").shift();
  return null;
}

function Index() {
  const navigate = useNavigate();

  useEffect(() => {
    // Add 'index' class to the body
    document.body.classList.add("index");

    // Set up the interval to check for cookies every 3 seconds
    const intervalId = setInterval(() => {
      console.log(`Interval tick at: ${new Date().toLocaleTimeString()}`); // 
      if (!getCookie("Access_Token")) {
        // Redirect to login page if the Access_Token cookie is missing
        navigate("/login-page");
      }
    }, 2000);

    // Cleanup function to remove interval and class
    return () => {
      clearInterval(intervalId); // Clear interval on component unmount
      document.body.classList.remove("index"); // Remove 'index' class on cleanup
    };
  }, [navigate]);

  return (
    <>
      <IndexNavbar />
      <IndexHeader />
      <div className="main">
        <SectionButtons />
        <SectionNavbars />
        <SectionNavigation />
        <SectionProgress />
        <SectionNotifications />
        <SectionTypography />
        <SectionJavaScript />
        <SectionCarousel />
        <SectionNucleoIcons />
        <SectionDark />
        <SectionLogin />
        <SectionExamples />
        <SectionDownload />
        {/* <HistoryTable/> */}
        <DemoFooter />
      </div>
    </>
  );
}

export default Index;
