import React, { useEffect } from 'react';
import { Container, Row, Col, Button, Stack, Card } from 'react-bootstrap';
import { useNavigate } from 'react-router-dom';
import kidsImage from '../Photos/ChildrenTraveling.jpg'; 

const HomePage: React.FC = () => {
  const navigate = useNavigate();

  useEffect(() => {
    localStorage.removeItem("activeTeacher");
  }, []);

  return (
    <div style={{ 
      backgroundColor: '#f8f9fa', 
      color: '#212529', 
      minHeight: '100vh',
      display: 'flex',
      alignItems: 'center',
      fontFamily: 'system-ui, -apple-system, "Segoe UI", Roboto, sans-serif',
      direction: 'rtl',
      overflowX: 'hidden'
    }}>
      
      <Container>
        <Row className="align-items-center justify-content-center gy-5">
          
          <Col lg={6} xl={5} className="text-right order-2 order-lg-1">
            <div className="pe-lg-4">
              <h1 className="display-3 fw-black mb-4" style={{ 
                lineHeight: 1.1,
                color: '#1a1a1a',
                letterSpacing: '-1px'
              }}>
                מערכת הטיולים <br /> 
                של <span className="text-success">בנות משה</span>
              </h1>
              
              <p className="lead mb-5 text-muted" style={{ fontSize: '1.25rem' }}>
              מעקב אחרי הבנות בטיול 
              </p>

              <Stack gap={3} className="col-md-10">
                <Button 
                  variant="success" 
                  className="py-3 rounded-pill shadow-lg border-0 hover-lift fs-5 fw-bold"
                  onClick={() => navigate('/teacher-login')}
                >
                  כניסת מורה
                </Button>
                
                <Stack direction="horizontal" gap={3}>
                  <Button 
                    variant="outline-primary" 
                    className="py-3 rounded-pill border-2 flex-grow-1 fw-bold hover-lift"
                    onClick={() => navigate('/register-student')}
                  >
                    רישום תלמידה
                  </Button>
                  <Button 
                    variant="outline-dark" 
                    className="py-3 rounded-pill border-2 flex-grow-1 fw-bold hover-lift"
                    onClick={() => navigate('/monitor')}
                  >
                    צפייה במפה
                  </Button>
                </Stack>
              </Stack>
            </div>
          </Col>

          <Col lg={6} className="d-flex justify-content-center order-1 order-lg-2">
            <div className="position-relative">
              <div style={{
                position: 'absolute',
                top: '-10%',
                right: '-10%',
                width: '120%',
                height: '120%',
                background: 'radial-gradient(circle, rgba(40,167,69,0.1) 0%, rgba(255,255,255,0) 70%)',
                zIndex: 0
              }} />
              
              <Card className="border-0 shadow-2xl rounded-circle overflow-hidden p-3 bg-white hover-rotate" style={{ zIndex: 1 }}>
                <div style={{
                  width: 'min(450px, 80vw)',
                  height: 'min(450px, 80vw)', 
                  borderRadius: '50%', 
                  overflow: 'hidden', 
                  position: 'relative',
                }}>
                  <img 
                    src={kidsImage} 
                    alt="ילדים מטיילים" 
                    style={{ 
                      width: '100%', 
                      height: '100%', 
                      objectFit: 'cover'
                    }} 
                  />
                  <div style={{
                    position: 'absolute',
                    top: 0,
                    left: 0,
                    width: '100%',
                    height: '100%',
                    background: 'linear-gradient(to bottom, transparent, rgba(0,0,0,0.05))'
                  }} />
                </div>
              </Card>
            </div>
          </Col>
          
        </Row>
      </Container>

      <style>{`
        .fw-black { font-weight: 900; }
        
        .hover-lift {
          transition: all 0.3s cubic-bezier(0.175, 0.885, 0.32, 1.275);
        }
        
        .hover-lift:hover {
          transform: translateY(-5px);
          box-shadow: 0 12px 25px rgba(0,0,0,0.1) !important;
        }

        .hover-rotate {
            transition: transform 0.6s ease;
        }

        .hover-rotate:hover {
            transform: scale(1.02) rotate(2deg);
        }

        .shadow-2xl {
            box-shadow: 0 25px 50px -12px rgba(0, 0, 0, 0.15);
        }

        @media (max-width: 991px) {
            .display-3 { font-size: 2.5rem; }
            .text-right { text-align: center !important; }
            .pe-lg-4 { padding-right: 0 !important; }
        }
      `}</style>
    </div>
  );
};

export default HomePage;