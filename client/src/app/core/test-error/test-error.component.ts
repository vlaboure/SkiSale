import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-test-error',
  templateUrl: './test-error.component.html',
  styleUrls: ['./test-error.component.scss']
})
export class TestErrorComponent implements OnInit {
  validationErrors: any;
  apiUrl = environment.apiUrl;
  constructor(private httpErr: HttpClient) { }

  ngOnInit(): void {
  }

  getError404(){
    return this.httpErr.get(`${this.apiUrl}products/402`).subscribe(res =>{
      console.log(res);
    }, error => {console.log(error); });
  }
  getError500(){
    return this.httpErr.get(`${this.apiUrl}buggy/servererror`).subscribe(res =>{
      console.log(res);
    }, error => {
      console.log(error);
    });
  }
  getError400(){
    return this.httpErr.get(`${this.apiUrl}buggy/badrequest`).subscribe(res =>{
      console.log(res);
    }, error => {console.log(error); });
  }
  getError400Valid(){
    return this.httpErr.get(`${this.apiUrl}products/quatre`).subscribe(res =>{
      console.log(res);
    }, error => {
      console.log(error);
      this.validationErrors = error.errors;
    });
  }
}
