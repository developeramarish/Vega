import { Component, OnInit } from '@angular/core';
import { Register } from '../../models/register';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs/Observable';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {
  userRegistration: Register = {
    name: '',
    email: '',
    password: '',
    confirmPassword: ''
  };
  private apiBase: string = "https://localhost:4001";

  constructor(private http: HttpClient) { }

  ngOnInit() {
  }

  onSubmit() {
    console.log(this.userRegistration);
    return this.http
      .post<Register>(this.apiBase + "/api/account", this.userRegistration)
      .subscribe(res => console.log(res));
  }

}
