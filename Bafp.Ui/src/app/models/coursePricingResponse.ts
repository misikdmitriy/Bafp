import { HttpResponse } from "./httpResponse";
import { CoursePricing } from "./coursePricing";

export class CoursePricingResponse extends HttpResponse {
  public coursePriceList: CoursePricing[];
}