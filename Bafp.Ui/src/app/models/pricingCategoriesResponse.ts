import { HttpResponse } from "./httpResponse"
import { PricingCategory } from "./pricingCategory"

export class PricingCategoriesResponse extends HttpResponse {
  public categories: PricingCategory[];
}