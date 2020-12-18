import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from 'src/app/services/auth.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  model: any = {};

  constructor(private authService: AuthService, private router: Router) { }

  ngOnInit(): void {
  }

  login() {
    this.authService.login(this.model)
      .subscribe(response => {
        console.log(response);
        this.router.navigateByUrl('/');
      }, error => {
        console.log(error)
      })
  }
}
