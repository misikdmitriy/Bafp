import { HttpResponse } from "./httpResponse"
import { PricingCategory } from "../contracts/pricingCategory"

export class PricingCategoriesResponse extends HttpResponse {
  public categories: PricingCategory[];
}