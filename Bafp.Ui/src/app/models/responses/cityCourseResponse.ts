import { HttpResponse } from "./httpResponse";
import { CityCourse } from "../contracts/cityCourse";

export class CityCoursesResponse extends HttpResponse {
  public cityCourses: CityCourse[];
}