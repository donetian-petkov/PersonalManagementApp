import React, { Component } from 'react';
import { Layout } from './components/main/Layout';

import './custom.css'
import { BrowserRouter as Router, Routes, Route } from 'react-router-dom';
import NavBar from "./components/main/NavBar";
import Home from "./components/main/Home";
import Calendar from "./components/calendar/Calendar";
import ToDoListComponent from "./components/todolist/ToDoListComponent";

function App() {
    return (
        <>
            <NavBar/>
            <Routes>
                <Route path='/' exact element={<Home/>} />
                <Route path='/calendar' element={<Calendar/>} />
                <Route path='/todolist' element={<ToDoListComponent/>} />
                <Route path="*" element={<Home/>} />
            </Routes>
        </>
    );
}

export default App;