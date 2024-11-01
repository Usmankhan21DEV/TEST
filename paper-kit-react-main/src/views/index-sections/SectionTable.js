// HistoryTable.js
import React, { useState, useEffect } from 'react';
import { useTable, usePagination, useSortBy } from 'react-table';
import axios from 'axios';
//import Modal from 'react-modal';
import * as XLSX from 'xlsx';
import ReactDatetime from "react-datetime";
import {
    Button,
    Label,
    FormGroup,
    Input,
    ModalHeader,Modal,
    ModalBody,
    ModalFooter,
    InputGroupAddon,
    InputGroupText,
    InputGroup,
    Container,
    Row,
    Col,
    CustomInput,
  } from "reactstrap";
import { getCookie } from 'views/Index';

const HistoryTable = () => {
    const [data, setData] = useState([]);
    const [serviceData, setserviceData] = useState([]);
    const [userData, setUserData] = useState([]);
    const [pageCount, setPageCount] = useState(0);
    const [modalData, setModalData] = useState(null);
    const [isModalOpen, setIsModalOpen] = useState(false);
    
    // State for filter inputs
    const [imei, setImei] = useState('');
    const [from, setFrom] = useState('');
    const [to, setTo] = useState('');
    const [service, setService] = useState('');
    const [userSeqNum, setUserSeqNum] = useState('');


    const fetchServices = async () => {
        const accessToken = getCookie("Access_Token");
        const response = await axios.get('https://localhost:7162/api/Frm/fetch-services', {
            withCredentials: true,            
            headers: {
                'Authorization': `Bearer ${accessToken}`,
                'Content-Type': 'application/json' // Specify the content type
            }
        });
       // console.log(response.data.info);
        setserviceData(response.data.info);
    }

    const fetchusers = async () => {
        const accessToken = getCookie("Access_Token");
        const response = await axios.get('https://localhost:7162/api/Frm/fetch-users', {
            withCredentials: true,            
            headers: {
                'Authorization': `Bearer ${accessToken}`,
                'Content-Type': 'application/json' // Specify the content type
            }
        });
        //console.log(response.data.info,response.data.status);
        if(response.data.status){
        setUserData(response.data.info);
    }
   // console.log(userData);
    }
    const fetchData = async ({ pageIndex, pageSize, sortBy }) => {
     //   console.log(pageIndex, pageSize, sortBy);
        
        const response = await axios.post('https://localhost:7162/api/Frm/history', {
            Imei: imei,
            From: from,
            To: to,
            Start: pageIndex * pageSize, // Calculate start index
            Length: pageSize, // Number of records to fetch
            Service: service,
            UserSeqNum: userSeqNum,
        }, {
            withCredentials: true, // Include credentials (cookies) in the request
            headers: {
                'Content-Type': 'application/json' // Specify the content type
            }
        });

        // Handle the response data
       // console.log('History Data:', response.data.info);
        setData(response.data.info.map(item => {
            // Conditional mapping for DESCRIPTION
            let description = '';
            var serviceData = item[10];
            if (serviceData === 'IPhone Carrier') {
                description = item[2];               
            } else if (serviceData === 'iPHONE CARRIER - FMI & BLACKLIST') {
                description = item[3];               
            } else if (serviceData === 'iCLOUD ON/OFF') {
                description = item[4];               
            } else if (serviceData === 'APPLE MDM STATUS') {
                description = item[5];               
            } else if (serviceData === 'iPHONE SIM-LOCK') {
                description = item[6];               
            } else if (serviceData === 'APPLE BASIC INFO') {
                description = item[7];               
            } else if (serviceData === 'APPLE ACTIVATION STATUS - IMEI/SN') {
                description = item[14];               
            } else if (serviceData === 'MDM LOCK BYPASS - iPHONE/iPAD') {
                description = item[15];               
            } else if (serviceData === 'WW BLACKLIST STATUS') {
                description = item[16];               
            } else if (serviceData === 'USA T-MOBILE - CLEAN') {
                description = item[17];               
            } else if (serviceData === 'USA AT&T - CLEAN/ACTIVE LINE') {
                description = item[18];               
            } else if (serviceData === 'USA CRICKET - CLEAN & 6 MONTHS OLD') {
                description = item[19];               
            } else if (serviceData === 'CLARO ALL COUNTRIES - PREMIUM (iPHONE 14)') {
                description = item[20];               
            } else if (serviceData === 'CLARO ALL COUNTRIES - PREMIUM (iPHONE 15)') {
                description = item[21];               
            } else if (serviceData === 'CLARO ALL COUNTRIES - PREMIUM (UP TO iPHONE 13)') {
                description = item[22];               
            } else if (serviceData === 'SAMSUNG AT&T/CRICKET... ALL MODELS') {
                description = item[23];               
            } else if (serviceData === 'ONEPLUS INFO') {
                description = item[24];               
            } else if (serviceData === 'MOTOROLA INFO') {
                description = item[25];               
            } else if (serviceData === 'GOOGLE PIXEL INFO') {
                description = item[26];               
            } else if (serviceData === 'SAMSUNG INFO') {
                description = item[27];               
            } else if (serviceData === 'SAMSUNG INFO - PRO') {
                description = item[28];
               
            } else if (serviceData === 'BRAND & MODEL INFO') {
                description= item[29];
               
            }else {
                description = ''; // Default value or any other fallback
            }
            //console.log(description);
            return {
                FULL_NAME: item[0],
                IMEI: item[1],
                DESCRIPTION: description, // Set DESCRIPTION based on condition
                STATUS: "OK", // This should be updated based on actual data
                COST: item[8], // Map cost
                PACKAGE_NAME: item[10],
                ENTRY_DATE: item[11],
                SEARCH_DATE: item[12],
            };
        }));
       
        setPageCount(Math.ceil(response.data.totalRecords / pageSize));
    };

    const columns = React.useMemo(
        () => [
            { Header: 'Full Name', accessor: 'FULL_NAME' },
            { Header: 'IMEI', accessor: 'IMEI' },
            { Header: 'Description', accessor: 'DESCRIPTION' },
            { Header: 'Status', accessor: 'STATUS' },
            { Header: 'Cost', accessor: 'COST' },
            { Header: 'Package', accessor: 'PACKAGE_NAME' },
            { Header: 'Created Date', accessor: 'ENTRY_DATE' },
            { Header: 'Searched Date', accessor: 'SEARCH_DATE' },
        ],
        []
    );

    const {
        getTableProps,
        getTableBodyProps,
        headerGroups,
        page,
        prepareRow,
        canPreviousPage,
        canNextPage,
        pageOptions,
        pageCount: controlledPageCount,
        gotoPage,
        nextPage,
        previousPage,
        setPageSize,
        state: { pageIndex, pageSize },
    } = useTable(
        {
            columns,
            data,
            initialState: { pageIndex: 0 },
            manualPagination: true,
            pageCount,
            manualSortBy: true,
            autoResetPage: false,
        },
        useSortBy,
        usePagination
    );

    useEffect(() => {
        fetchServices();
        fetchusers();
        fetchData({ pageIndex, pageSize });     
    }, [pageIndex, pageSize, imei, from, to, service, userSeqNum]); // Update dependencies

    const handleRowClick = (row) => {
        setModalData(row.original);
        setIsModalOpen(true);
    };

    const closeModal = () => setIsModalOpen(false);

    const exportToExcel = () => {
        // Create a modified data array for export
        const modifiedData = data.map(item => ({
            FULL_NAME: item.FULL_NAME,
            IMEI: item.IMEI,
            DESCRIPTION: item.DESCRIPTION.replace(/<[^>]+>/g, ' '), // Remove HTML tags
            STATUS: "OK",
            COST: item.COST,
            PACKAGE_NAME: item.PACKAGE_NAME,
            ENTRY_DATE: item.ENTRY_DATE,
            SEARCH_DATE: item.SEARCH_DATE,
        }));

        const ws = XLSX.utils.json_to_sheet(modifiedData);
        const wb = XLSX.utils.book_new();
        XLSX.utils.book_append_sheet(wb, ws, 'History Data');
        XLSX.writeFile(wb, 'HistoryData.xlsx');
    };
   // console.log(serviceData.data);
    return (
        <Container> 
        <div>
            {/* Filter Inputs */}
            <div className="filter-section">
            <Row>
            <FormGroup className="has-success">
               
              </FormGroup>
              <Col sm="3"> <Input 
                    className="form-control-success"
                    type="search" 
                    value={imei} 
                    onChange={e => setImei(e.target.value)} 
                    placeholder="IMEI" 
                /></Col>
               
                 {/* <Col sm="3"> <Input 
                className="form-control-success"
                    type="search" 
                    value={from} 
                    placeholder="IMEI" 
                    onChange={e => setFrom(e.target.value)} 
                /> </Col> */}
               
               <Col sm="3">
                <FormGroup>
                            <InputGroup className="date" id="datetimepicker">
                <ReactDatetime
                    inputProps={{
                        placeholder: "Select To Date",
                    }}
                    value={to || ""}  // Set to an empty string when `to` is null
                    onChange={date => setTo(date)}
                    dateFormat="YYYY-MM-DD" // or any preferred format
                />
                <InputGroupAddon addonType="append">
                    <InputGroupText onClick={() => setTo(null)} style={{ cursor: "pointer" }}>
                        <span className="glyphicon glyphicon-calendar">
                            <i aria-hidden={true} className="fa fa-calendar" />
                        </span>
                        
                    </InputGroupText>
                </InputGroupAddon>
            </InputGroup>
                </FormGroup>
                </Col>

                <Col sm="3">
                <FormGroup>
                    <InputGroup className="date" id="datetimepicker">
                    <ReactDatetime
                        inputProps={{
                        placeholder: "Select From Date",
                        }}
                        value={from}
                        onChange={date => setFrom(date)}
                        dateFormat="YYYY-MM-DD" // or any preferred format
                    />
                    <InputGroupAddon addonType="append">
                        <InputGroupText>
                        <span className="glyphicon glyphicon-calendar">
                            <i aria-hidden={true} className="fa fa-calendar" />
                        </span>
                        </InputGroupText>
                    </InputGroupAddon>
                    </InputGroup>
                </FormGroup>
                </Col>
                <Col sm="3">
                 <FormGroup>            
            <Input
                id="serviceSelect"
                name="select"
                type="select"
                placeholder="Service" 
                value={service}
                onChange={e => setService(e.target.value)} // Handle selection change
                        >
                            <option value="">Select a service</option>
                            {serviceData.map((item) => (
                                <option key={item.serviceSeqNum} value={item.serviceSeqNum}>
                                    {item.completeServiceName}
                                </option>
                            ))}
                        </Input>
                    </FormGroup>                     
                 </Col>
                {userData&&userData.length>0?( <Col sm="3">
                <InputGroup>

                        <Input
                            id="userSelect"
                            name="select"
                            type="select"
                            placeholder="Username"
                            value={userSeqNum}
                            onChange={e => setUserSeqNum(e.target.value)} // Handle selection change
                        >
                            <option value="">Select a user</option>
                            {userData.map((item) => (
                                <option key={item.seqNum} value={item.seqNum}>
                                    {item.userName}
                                </option>
                            ))}
                        </Input>
       
                    <InputGroupAddon addonType="append">
                    <InputGroupText>
                        <i aria-hidden={true} className="fa fa-group" />
                    </InputGroupText>
                    </InputGroupAddon>
                </InputGroup>
                </Col> ):(null)}
                
                </Row>
                
                <Button className= "btn btn-warning mt-2" onClick={() => fetchData({ pageIndex, pageSize })}>Search</Button>
                <Button  onClick={exportToExcel} className="btn btn-success ml-2 mt-2">Export to Excel</Button>
               
            </div>
            <hr></hr>
           
            <Container>
            <Row>
                <Col>
                    <table {...getTableProps()} className="table">
                        <thead>
                            {headerGroups.map(headerGroup => (
                                <tr {...headerGroup.getHeaderGroupProps()}>
                                    {headerGroup.headers.map(column => (
                                        <th {...column.getHeaderProps(column.getSortByToggleProps())}>
                                            {column.render('Header')}
                                            <span>
                                                {column.isSorted ? (column.isSortedDesc ? ' 🔽' : ' 🔼') : ''}
                                            </span>
                                        </th>
                                    ))}
                                </tr>
                            ))}
                        </thead>
                        <tbody {...getTableBodyProps()}>
                            {page.map(row => {
                                prepareRow(row);
                                return (
                                    <tr {...row.getRowProps()} onClick={() => handleRowClick(row)}>
                                        {row.cells.map(cell => {
                                            // Use different rendering for DESCRIPTION
                                            if (cell.column.id === 'DESCRIPTION') {
                                                return (
                                                    <td {...cell.getCellProps()} dangerouslySetInnerHTML={{ __html: cell.value }} />
                                                );
                                            }
                                            return (
                                                <td {...cell.getCellProps()}>{cell.render('Cell')}</td>
                                            );
                                        })}
                                    </tr>
                                );
                            })}
                        </tbody>
                    </table>
                    {/* Add Pagination Controls */}
                    <div>
                        <Button onClick={() => gotoPage(0)} disabled={!canPreviousPage}>
                            {'<<'}
                        </Button>{' '}
                        <Button onClick={() => previousPage()} disabled={!canPreviousPage}>
                            {'<'}
                        </Button>{' '}
                        <Button onClick={() => nextPage()} disabled={!canNextPage}>
                            {'>'}
                        </Button>{' '}
                        <Button onClick={() => gotoPage(controlledPageCount - 1)} disabled={!canNextPage}>
                            {'>>'}
                        </Button>{' '}
                        <span>
                            Page{' '}
                            <strong>
                                {pageIndex + 1} of {pageOptions.length}
                            </strong>{' '}
                        </span>
                        <select className='btn-round btn btn-outline-default'
                            value={pageSize}
                            onChange={e => {
                                setPageSize(Number(e.target.value));
                            }}
                        >
                            {[10, 20, 30, 40, 50].map(pageSize => (
                                <option key={pageSize} value={pageSize}>
                                    Show {pageSize}
                                </option>
                            ))}
                        </select>
                    </div>
                </Col>
            </Row>
            {/* Modal for Details */}
            <Modal
            isOpen={isModalOpen}
            toggle={() => setIsModalOpen(false)}
            centered // Centers the modal vertically in the viewport
            className="modal-dialog modal-lg" // Set modal size
        >
            <ModalHeader toggle={() => setIsModalOpen(false)}>
                <strong>Details for {modalData?.FULL_NAME}</strong>
            </ModalHeader>
            <ModalBody>
                {modalData && (
                    <div>
                        <p><strong>IMEI:</strong> <strong>{modalData.IMEI}</strong></p>
                        <p><strong>Description:</strong> 
                            <span dangerouslySetInnerHTML={{ __html: modalData.DESCRIPTION }} />
                        </p>
                        <p><strong>Status:</strong> OK</p>
                        <p><strong>Cost:</strong> {modalData.COST}</p>
                        <p><strong>Package Name:</strong> {modalData.PACKAGE_NAME}</p>
                        <p><strong>Entry Date:</strong> {modalData.ENTRY_DATE}</p>
                        <p><strong>Search Date:</strong> {modalData.SEARCH_DATE}</p>
                    </div>
                )}
            </ModalBody>
            <ModalFooter>
                <Button color="danger" onClick={() => setIsModalOpen(false)}>
                    Close
                </Button>
            </ModalFooter>
        </Modal>
        </Container>
        </div>
        </Container> 
    );
};

export default HistoryTable;
