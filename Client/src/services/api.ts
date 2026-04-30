import axios from "axios";
import { Student, Teacher } from "../types";

const API_BASE_URL = "https://localhost:7081/api";

const api = axios.create({
  baseURL: API_BASE_URL,
});

export const teacherService = {
  register: (teacher: Teacher) =>
    api.post("/Teachers/register", teacher),

  getById: (id: string) =>
    api.get<Teacher>(`/Teachers/id/${id}`),

  getStudentsByTeacher: (teacherId: string) =>
    api.get<Student[]>(`/Teachers/${teacherId}/students`),
};

export const studentService = {

  register: (student: Student) =>
    api.post("/Students/register", student),

  getStudents: () =>
    api.get<Student[]>("/Students"),

  monitorByTeacher: (teacherId: string) =>
    api.get<Student[]>(`/Students/monitor/${teacherId}`),
};

export default api;