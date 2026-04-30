import React, { useState } from 'react';
import { Container, Card, Form, Button, Alert, Spinner, Row, Col } from 'react-bootstrap';
import { useNavigate } from 'react-router-dom';
import { Teacher } from '../types';
import { teacherService } from '../services/api';

const RegisterTeacher: React.FC = () => {
  const [teacher, setTeacher] = useState<Partial<Teacher>>({
    tId: '',
    firstName: '',
    lastName: '',
    className: '',
  });

  const [status, setStatus] = useState<{ msg: string; type: string } | null>(null);
  const [loading, setLoading] = useState(false);
  const navigate = useNavigate();

  const handleChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    const { name, value } = e.target;
    setTeacher((prev) => ({
      ...prev,
      [name]: name === 'tId' ? value.replace(/\D/g, '').slice(0, 9) : value
    }));
  };

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    setLoading(true);
    setStatus(null);

    try {
      await teacherService.register(teacher as Teacher);
      setStatus({ msg: 'נרשמת בהצלחה! מעביר אותך להתחברות...', type: 'success' });
      setTimeout(() => navigate('/teacher-login'), 2000);
    } catch (err: any) {
      let serverError = 'שגיאה בתהליך הרישום';
      if (err.response && err.response.data) {
        if (typeof err.response.data === 'string') {
          serverError = err.response.data;
        } else if (err.response.data.message) {
          serverError = err.response.data.message;
        } else if (err.response.data.errors) {
          serverError = Object.values(err.response.data.errors).flat().join(', ');
        }
      }
      setStatus({ msg: serverError, type: 'danger' });
    } finally {
      setLoading(false);
    }
  };

  return (
    <div style={{ backgroundColor: '#fdfcfb', minHeight: '100vh', display: 'flex', alignItems: 'center', direction: 'rtl' }}>
      <Container className="py-5">
        <Row className="justify-content-center">
          <Col md={8} lg={6}>
            <Card className="p-4 shadow-sm border-0" style={{ borderRadius: '20px' }}>
              <h3 className="text-center mb-4 fw-bold" style={{ color: '#28a745' }}>רישום מורה חדשה</h3>
              
              {status && (
                <Alert variant={status.type} className="text-center fw-bold">
                  {status.msg}
                </Alert>
              )}

              <Form onSubmit={handleSubmit}>
                <Form.Group className="mb-3">
                  <Form.Label className="fw-bold small">תעודת זהות</Form.Label>
                  <Form.Control 
                    name="tId" 
                    value={teacher.tId} 
                    onChange={handleChange} 
                    placeholder="הזיני 9 ספרות"
                    required 
                  />
                </Form.Group>

                <Row>
                  <Col md={6}>
                    <Form.Group className="mb-3">
                      <Form.Label className="fw-bold small">שם פרטי</Form.Label>
                      <Form.Control 
                        name="firstName" 
                        value={teacher.firstName} 
                        onChange={handleChange} 
                        required 
                      />
                    </Form.Group>
                  </Col>
                  <Col md={6}>
                    <Form.Group className="mb-3">
                      <Form.Label className="fw-bold small">שם משפחה</Form.Label>
                      <Form.Control 
                        name="lastName" 
                        value={teacher.lastName} 
                        onChange={handleChange} 
                        required 
                      />
                    </Form.Group>
                  </Col>
                </Row>

                <Form.Group className="mb-4">
                  <Form.Label className="fw-bold small">כיתה</Form.Label>
                  <Form.Control 
                    name="className" 
                    value={teacher.className} 
                    onChange={handleChange} 
                    placeholder="למשל: א' 1"
                    required 
                  />
                </Form.Group>

                <Button 
                  variant="success" 
                  size="lg"
                  className="w-100 py-3 fw-bold shadow-sm" 
                  style={{ borderRadius: '50px' }}
                  type="submit" 
                  disabled={loading}
                >
                  {loading ? <Spinner size="sm" animation="border" /> : 'סיום רישום'}
                </Button>

                <Button 
                  variant="link" 
                  className="w-100 mt-2 text-decoration-none text-muted small" 
                  onClick={() => navigate('/')}
                >
                  ביטול וחזרה
                </Button>
              </Form>
            </Card>
          </Col>
        </Row>
      </Container>
    </div>
  );
};

export default RegisterTeacher;