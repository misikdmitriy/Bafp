import { HttpResponse } from "./httpResponse";
import { City } from "../contracts/city";

export class CitiesResponse extends HttpResponse {
  public cities: City[];
}