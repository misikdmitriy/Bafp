import { HttpResponse } from "./httpResponse";
import { CityTotal } from "../contracts/cityTotal";

export class CitiesTotalResponse extends HttpResponse {
    public total: CityTotal[];
}