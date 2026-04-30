import React, { useEffect, useState, useCallback } from "react";
import { Container, Button, Spinner } from "react-bootstrap";
import { useNavigate } from "react-router-dom";
import Map from "../components/Map/Map";
import { Student, Teacher } from "../types";
import { studentService } from "../services/api";

const MonitorPage: React.FC = () => {
  const [students, setStudents] = useState<Student[]>([]);
  const [activeTeacher, setActiveTeacher] = useState<Teacher | null>(null);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState<string | null>(null);

  const navigate = useNavigate();

  const fetchData = useCallback(async (teacher: Teacher | null) => {
    try {
      const res = teacher?.tId 
        ? await studentService.monitorByTeacher(teacher.tId) 
        : await studentService.getStudents();
      
      setStudents(res.data);
    } catch (err) {
      setError("קיימת בעיה בסנכרון הנתונים מול השרת");
      console.error("Fetch error:", err);
    } finally {
      setLoading(false);
    }
  }, []);

  useEffect(() => {
    const stored = localStorage.getItem("activeTeacher");
    let currentTeacher: Teacher | null = null;
    
    if (stored) {
      try {
        currentTeacher = JSON.parse(stored);
        setActiveTeacher(currentTeacher);
      } catch (e) {
        console.error("Failed to parse teacher data");
      }
    }

    fetchData(currentTeacher);
    const intervalId = setInterval(() => {
      fetchData(currentTeacher);
    }, 10000);
    return () => clearInterval(intervalId);
  }, [fetchData]);

  const handleLogout = () => {
    localStorage.removeItem("activeTeacher");
    navigate("/");
  };

  return (
    <Container fluid style={{ height: "100vh", position: "relative", padding: 0 }}>
      
      <div style={{ position: "absolute", top: 20, right: 20, zIndex: 1000, display: 'flex', gap: '10px' }}>
<Button 
  variant="light" 
  className="shadow-sm" 
  onClick={() => {
    localStorage.removeItem("activeTeacher"); 
    navigate("/"); 
  }}
>
</Button>

        {activeTeacher && (
          <Button variant="danger" className="shadow-sm" onClick={handleLogout}>
            התנתקות
          </Button>
        )}
      </div>

      <div
        style={{
          position: "absolute",
          bottom: 30,
          left: 20,
          zIndex: 1000,
          background: "rgba(255, 255, 255, 0.9)",
          padding: "15px",
          borderRadius: "12px",
          boxShadow: "0 4px 12px rgba(0,0,0,0.15)",
          minWidth: "200px"
        }}
      >
        <h6 style={{ marginBottom: '5px' }}>
          {activeTeacher ? `ניטור: כיתה ${activeTeacher.className}` : "מבט מערכת כללי"}
        </h6>
        
        <div style={{ fontSize: '12px', color: '#666' }}>
          {loading ? (
            <span><Spinner animation="border" size="sm" /> מעדכן נתונים...</span>
          ) : (
            <span>עדכון אחרון: לפני רגע</span>
          )}
        </div>

        {error && <div style={{ fontSize: '12px', color: 'red', marginTop: '5px' }}>{error}</div>}
      </div>
      <Map
        students={students}
        teacher={activeTeacher || ({} as Teacher)} 
      />
    </Container>
  );
};

export default MonitorPage;