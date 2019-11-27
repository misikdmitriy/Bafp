import { HttpResponse } from './httpResponse'
import { Course } from './course'

export class CoursesResponse extends HttpResponse {
  public courses: Course[];
}