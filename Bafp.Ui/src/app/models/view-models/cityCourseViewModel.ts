import { IEditable } from './editable'

export class CityCourseViewModel implements IEditable {
  public courseId: number;
  public courseName: string;
  public count: number;
  public price: number;
  public isEditing: boolean;

  constructor(public config: {courseId: number, courseName: string, count: number, price: number, isEditing: boolean}) {
    this.courseId = config.courseId;
    this.courseName = config.courseName;
    this.count = config.count;
    this.price = config.price;
    this.isEditing = config.isEditing;
  }

  get total() { return this.price * this.count; }
}
