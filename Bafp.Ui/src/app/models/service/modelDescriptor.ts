import { FieldDescriptor } from "./FieldDescriptor";

export class ModelDescriptor {
  public fieldsDescriptor: FieldDescriptor[]
  public canRemove: boolean;
  public canEdit: boolean;
  public canAdd: boolean;
  public editCallback: (obj: Object) => void;
  public removeCallback: (obj: Object) => void;
  public addCallback: (obj: Object) => void;

  constructor() {
    this.fieldsDescriptor = [];
  }
}