import { HttpResponse } from "./httpResponse";
import { City } from "./city";

export class CityResponse extends HttpResponse {
  public cities: City[];
}