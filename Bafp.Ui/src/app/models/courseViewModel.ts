export class CourseViewModel {
  public courseId: number;
  public courseName: string;
  public count: number;
  public price: number;
  public editMode: boolean;

  get total() {
    return this.price * this.count;
  }
}