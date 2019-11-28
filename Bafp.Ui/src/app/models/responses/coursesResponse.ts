import { HttpResponse } from './httpResponse'
import { Course } from '../contracts/course'

export class CoursesResponse extends HttpResponse {
  public courses: Course[];
}