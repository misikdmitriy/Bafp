import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { HttpService } from '../http.service';
import { CoursePricingResponse } from '../models/coursePricingResponse';
import { CoursesResponse } from '../models/coursesResponse';
import { CoursePricing } from '../models/coursePricing';
import { Course } from '../models/course';
import { CoursePricingViewModel } from '../models/coursePricingViewModel';

@Component({
  selector: 'app-category-pricings',
  templateUrl: './category-pricings.component.html',
  styleUrls: ['./category-pricings.component.css']
})
export class CategoryPricingsComponent implements OnInit {
  coursePrices: CoursePricingViewModel[];

  constructor(private httpService: HttpService, private route: ActivatedRoute) {
    this.route.params.subscribe((params) => {
      let categoryId: number = params.categoryId;

      Promise.all([this.httpService.getCoursePricing(categoryId), this.httpService.getCourses()])
        .then((response: [CoursePricingResponse, CoursesResponse]) => {
          let coursesPricing: CoursePricing[] = response[0].coursePriceList;
          let courses: Course[] = response[1].courses;

          this.coursePrices = coursesPricing.map((pricing: CoursePricing) => {
            let course: Course = courses.find((course: Course) => course.id === pricing.courseId);

            let viewModel: CoursePricingViewModel = new CoursePricingViewModel();
            viewModel.courseId = pricing.courseId;
            viewModel.courseName = course.name;
            viewModel.price = pricing.price;
            viewModel.categoryId = pricing.categoryId;
            viewModel.editMode = false;

            return viewModel;
          });
        });
    });
  }

  ngOnInit() {
  }

  upsertView(viewModel: CoursePricingViewModel): Promise<Object> {
    var model: CoursePricing = new CoursePricing();
    model.categoryId = viewModel.categoryId;
    model.courseId = viewModel.courseId;
    model.price = viewModel.price;

    return this.httpService.addCoursePricing(model)
      .then(() => viewModel.editMode = false);
  }
}
