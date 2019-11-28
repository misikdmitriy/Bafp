import { HttpResponse } from "./httpResponse";
import { CoursePricing } from "../contracts/coursePricing";

export class CoursePricingResponse extends HttpResponse {
  public coursePriceList: CoursePricing[];
}