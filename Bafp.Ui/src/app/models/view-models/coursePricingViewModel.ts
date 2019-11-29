import { IEditable } from './editable'

export class CoursePricingViewModel implements IEditable {
    public categoryId: number;
    public courseId: number;
    public courseName: string;
    public price: number;
    public isEditing: boolean;
}