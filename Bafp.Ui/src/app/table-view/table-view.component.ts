import { Component, Input, OnInit } from '@angular/core';
import { FieldDescriptor } from '../models/service/FieldDescriptor';
import { ModelDescriptor } from '../models/service/modelDescriptor';

@Component({
  selector: 'app-table-view',
  templateUrl: './table-view.component.html',
  styleUrls: ['./table-view.component.css']
})
export class TableViewComponent implements OnInit {
  @Input() modelDescriptor: ModelDescriptor;
  @Input() data: Object[];
  addMode = false;
  newItem: Object = {};

  constructor() { }

  ngOnInit() {
  }

  isEditable() {
    return this.modelDescriptor && this.modelDescriptor.canEdit;
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
        const linkPart : string = routerLink[key];
        if (linkPart.startsWith("$")) {
          routerLink[key] = item[linkPart.substring(1)];
        }
      }
    }
    return routerLink;
  }

  save(item: Object) {
    this.addMode = false;
    this.modelDescriptor.addCallback && this.modelDescriptor.addCallback(item);
  }

  edit(item: Object) {
    item["isEditing"] = false;
    this.modelDescriptor.editCallback && this.modelDescriptor.editCallback(item);
  }

  remove(item: Object) {
    this.data.splice(this.data.indexOf(item), 1)
    this.modelDescriptor.removeCallback && this.modelDescriptor.removeCallback(item);
  }
}
