export class CourseViewModel {
  public cityId: number;
  public cityName: string;
  public categoryId: number;
  public categoryName: string;
  public price: number;
  public count: number;

  public get total(): number { return this.price * this.count; }
}