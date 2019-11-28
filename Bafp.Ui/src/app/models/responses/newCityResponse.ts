import { HttpResponse } from "./httpResponse";
import { City } from "../contracts/city";

export class NewCityResponse extends HttpResponse {
  public city: City;
}