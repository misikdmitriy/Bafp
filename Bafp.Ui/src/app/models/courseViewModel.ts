export class CourseViewModel {
  public courseName: string;
  public count: number;
  public price: number;
  public editMode: boolean;

  get total() {
    return this.price * this.count;
  }
}