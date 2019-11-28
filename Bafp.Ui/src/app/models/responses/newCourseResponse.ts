import { HttpResponse } from "./httpResponse";
import { Course } from "../contracts/course";

export class NewCourseResponse extends HttpResponse {
  public course: Course;
}