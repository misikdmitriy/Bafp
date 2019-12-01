import { ChooseOption } from '../contracts/option';

export enum EditMode {
  None,
  Text,
  Number,
  Dropdown,
  PlusMinus
}

export enum FieldType {
  Text,
  Link
}

export class FieldDescriptor {
  public idName: string;
  public keyName: string;
  public name: string;
  public type: FieldType;
  public editMode: EditMode;
  public addMode: EditMode;
  public possibleValues: ChooseOption[];
  public args: Object;
}