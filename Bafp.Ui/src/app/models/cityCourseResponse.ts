import { HttpResponse } from "./httpResponse";
import { CityCourse } from "./cityCourse";

export class CityCoursesResponse extends HttpResponse {
  public cityCourses: CityCourse[];
}