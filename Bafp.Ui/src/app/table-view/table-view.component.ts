import { Component, Input, OnInit } from '@angular/core';
import { FieldDescriptor, EditMode } from '../models/service/FieldDescriptor';
import { ModelDescriptor } from '../models/service/modelDescriptor';
import { debounce } from '../methods';

@Component({
  selector: 'app-table-view',
  templateUrl: './table-view.component.html',
  styleUrls: ['./table-view.component.css']
})
export class TableViewComponent implements OnInit {
  @Input() modelDescriptor: ModelDescriptor;
  @Input() data: Object[];
  @Input() header: string;
  addMode = false;
  newItem: Object = {};
  debouncedEdit = [];

  constructor() { }

  ngOnInit() {
  }

  isEditable() {
    return this.modelDescriptor && this.modelDescriptor.canEdit &&
      this.modelDescriptor.fieldsDescriptor
        .some((fd: FieldDescriptor) => fd.editMode !== EditMode.None && fd.editMode !== EditMode.PlusMinus);
  }

  canRemove() {
    return this.modelDescriptor && this.modelDescriptor.canRemove;
  }

  canAdd() {
    return this.modelDescriptor && this.modelDescriptor.canAdd;
  }

  buildLink(descriptor: FieldDescriptor, item: Object) {
    var routerLink = descriptor.args["routerLink"].slice();
    for (const key in routerLink) {
      if (routerLink.hasOwnProperty(key)) {
        const linkPart: string = routerLink[key];
        if (linkPart.startsWith("$")) {
          routerLink[key] = item[linkPart.substring(1)];
        }
      }
    }
    return routerLink;
  }

  save(item: Object) {
    this.addMode = false;
    this.newItem = {};
    this.modelDescriptor.addCallback && this.modelDescriptor.addCallback(item);
  }

  edit(item: Object) {
    item["isEditing"] = false;
    this.modelDescriptor.editCallback && this.modelDescriptor.editCallback(item);
  }

  remove(item: Object) {
    if (confirm("Are you sure?") == true) {
      this.data.splice(this.data.indexOf(item), 1)
      this.modelDescriptor.removeCallback && this.modelDescriptor.removeCallback(item);
    }
  }

  plus(item: Object, keyName: string, debounce: boolean) {
    ++item[keyName];
    debounce && this.debounceEdit(item);
  }

  minus(item: Object, keyName: string, debounce: boolean) {
    if (item[keyName] > 0) {
      --item[keyName];
      debounce && this.debounceEdit(item);
    }
  }

  debounceEdit(item: Object) {
    let debouncer = this.debouncedEdit.filter((d: Object) => d["item"] === item)[0];
    if (!debouncer) {
      debouncer = { item: item, debounce: debounce(this.edit.bind(this), 500) };
      this.debouncedEdit.push(debouncer);
    }
    debouncer.debounce(item);
  }
}
