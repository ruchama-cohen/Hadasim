import React from 'react';
import { BrowserRouter as Router, Routes, Route } from 'react-router-dom';
import 'bootstrap/dist/css/bootstrap.min.css';
import HomePage from './pages/HomePage';
import TeacherLogin from './pages/TeacherLogin';
import RegisterTeacher from './pages/RegisterTeacher';
import RegisterStudent from './pages/RegisterStudent';
import MonitorPage from './pages/MonitorPage';

const App: React.FC = () => {
  return (
    <Router>
      <div style={{ direction: 'rtl', backgroundColor: '#f8f9fa', minHeight: '100vh' }}>
        <Routes>
          <Route path="/" element={<HomePage />} />
          <Route path="/teacher-login" element={<TeacherLogin />} />
          <Route path="/register-teacher" element={<RegisterTeacher />} />
          <Route path="/register-student" element={<RegisterStudent />} />
          <Route path="/monitor" element={<MonitorPage />} />
        </Routes>
      </div>
    </Router>
  );
};

export default App;