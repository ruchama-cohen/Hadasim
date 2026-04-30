import React, { useState } from 'react';
import { Container, Card, Form, Button, Alert, Spinner } from 'react-bootstrap';
import { useNavigate } from 'react-router-dom';
import { teacherService } from '../services/api';

const TeacherLogin: React.FC = () => {
  const [tId, setTId] = useState("");
  const [error, setError] = useState("");
  const [loading, setLoading] = useState(false);
  const navigate = useNavigate();

  const handleLogin = async (e: React.FormEvent) => {
    e.preventDefault();
    
    if (tId.length !== 9) {
      setError("תעודת זהות חייבת להכיל בדיוק 9 ספרות");
      return;
    }
    
    setLoading(true);
    setError("");
    
    try {
      const res = await teacherService.getById(tId);
      
      if (res && res.data) {
        localStorage.setItem('activeTeacher', JSON.stringify(res.data));
        navigate('/monitor'); 
      } else {
        setError("מורה לא נמצאה במערכת");
      }
    } catch (err: any) {
      const message = err.response?.data || "שגיאה בחיבור לשרת. נסי שוב מאוחר יותר.";
      setError(message);
    } finally {
      setLoading(false);
    }
  };

  return (
    <Container className="d-flex align-items-center justify-content-center" style={{ minHeight: '80vh', direction: 'rtl' }}>
      <Card className="shadow-lg p-4 border-0" style={{ width: '100%', maxWidth: '420px', borderRadius: '20px' }}>
        
        <div style={{ 
          height: '6px', 
          width: '60px', 
          backgroundColor: '#28a745', 
          margin: '0 auto 20px', 
          borderRadius: '10px' 
        }}></div>

        <div className="text-center mb-4">
           <h2 className="fw-bold text-success">כניסת מורה</h2>
           <p className="text-muted small"> ! אנא הזיני פרטי זיהוי</p>
        </div>

        {error && (
          <Alert variant="danger" className="py-2 small text-center border-0 rounded-3 shadow-sm">
            {error}
          </Alert>
        )}
        
        <Form onSubmit={handleLogin}>
          <Form.Group className="mb-4">
            <Form.Label className="small fw-bold text-secondary px-1">תעודת זהות</Form.Label>
            <Form.Control 
              type="text" 
              maxLength={9} 
              className="py-2 border-0 bg-light shadow-sm custom-input"
              value={tId} 
              onChange={(e) => setTId(e.target.value.replace(/\D/g, ""))} 
              placeholder="הכניסי את התעודת זהות שלך"
              disabled={loading}
              required 
            />
          </Form.Group>

          <Button 
            variant="success" 
            type="submit" 
            className="w-100 mb-3 py-2 fw-bold shadow hover-lift border-0" 
            style={{ borderRadius: '12px', background: '#28a745' }}
            disabled={loading || tId.length < 9}
          >
            {loading ? <Spinner size="sm" animation="border" className="me-2" /> : 'התחברות למערכת'}
          </Button>

          <div className="text-center mt-3">
            <span className="text-muted small">עדיין לא רשומה? </span>
            <Button 
              variant="link" 
              onClick={() => navigate('/register-teacher')} 
              className="p-0 text-decoration-none fw-bold text-success hover-opacity small"
              disabled={loading}
            >
              צרי חשבון חדש
            </Button>
          </div>
        </Form>
      </Card>

      <style>{`
        .custom-input:focus {
          background-color: #fff !important;
          box-shadow: 0 0 0 3px rgba(40, 167, 69, 0.2) !important;
          border: 1px solid #28a745 !important;
        }
        .hover-lift {
          transition: transform 0.2s ease, box-shadow 0.2s ease;
        }
        .hover-lift:hover:not(:disabled) {
          transform: translateY(-2px);
          box-shadow: 0 8px 15px rgba(40, 167, 69, 0.3) !important;
        }
        .hover-opacity:hover {
          opacity: 0.8;
        }
      `}</style>
    </Container>
  );
};

export default TeacherLogin;