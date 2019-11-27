import { HttpResponse } from "./httpResponse";
import { City } from "./city";

export class CitiesResponse extends HttpResponse {
  public cities: City[];
}