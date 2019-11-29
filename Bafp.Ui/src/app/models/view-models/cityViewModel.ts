import { IEditable } from "./editable";

export class CityViewModel implements IEditable {
  public isEditing: boolean;
  public cityId: number;
  public cityName: string;
  public categoryId: number;
  public categoryName: string;
}