import { IEditable } from "./editable";
import { Constants } from "src/app/constants";

export class CityViewModel implements IEditable {
  public isEditing: boolean;
  public cityId: number;
  public cityName: string;
  public categoryId: number;
  public categoryName: string;
  public total: number;
  public courses: string[]

  constructor(public config: { isEditing: boolean, cityId: number, cityName: string, categoryId: number, 
    categoryName: string, total: number, courses: string[] }) {
    this.total = config.total;
    this.isEditing = config.isEditing;
    this.cityId = config.cityId;
    this.cityName = config.cityName;
    this.categoryId = config.categoryId;
    this.categoryName = config.categoryName;
    this.courses = config.courses;
  }

  public get totalStudents() { return Constants.averageStudents * this.total; }
}