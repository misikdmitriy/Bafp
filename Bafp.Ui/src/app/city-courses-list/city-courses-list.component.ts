import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { CityCoursesResponse } from '../models/cityCourseResponse';
import { ActivatedRoute } from "@angular/router";
import { environment } from '../../environments/environment';
import { CityCourse } from '../models/cityCourse';

@Component({
  selector: 'app-city-courses-list',
  templateUrl: './city-courses-list.component.html',
  styleUrls: ['./city-courses-list.component.css']
})
export class CityCoursesListComponent implements OnInit {
  cityCoursesUrl = "api/cities/{cityName}/courses";
  addCityCoursesUrl = "api/cities/courses";
  cityCourse: CityCourse[];
  cityName: string;
  addMode = false;
  newCityCourse: CityCourse = {
    cityName: "",
    count: 1,
    courseName: ""
  };

  constructor(private http: HttpClient, private route: ActivatedRoute) {
    this.route.params.subscribe(params => {
      this.cityName = params.cityName;
      this.getCityCourses(this.cityName);
    });
  }

  ngOnInit() {
  }

  getCityCourses(cityName: string) {
    return this.http.get(environment.apiUrl + this.cityCoursesUrl.replace("{cityName}", cityName))
      .subscribe((data: CityCoursesResponse) => {
        this.cityCourse = data.cityCourses;
      });
  }

  addNew() {
    this.http.put(environment.apiUrl + this.addCityCoursesUrl, {
      cityName: this.cityName,
      courseName: this.newCityCourse.courseName,
      count: this.newCityCourse.count
    }).subscribe(() => window.location.reload());
  }
}
