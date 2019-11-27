export class CourseViewModel {
  public courseName: string;
  public count: number;
  public price: number;

  get total() {
    return this.price * this.count;
  }
}