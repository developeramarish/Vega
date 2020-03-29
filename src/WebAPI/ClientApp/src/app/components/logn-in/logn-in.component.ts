import { Component, OnInit } from '@angular/core';
import { UserManager, UserManagerSettings, User} from 'oidc-client';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-logn-in',
  templateUrl: './logn-in.component.html',
  styleUrls: ['./logn-in.component.css']
})

export class LognInComponent implements OnInit {

  private manager = new UserManager(getClientSettings());
  private user: User | null;

  constructor(private http:HttpClient) {
    this.manager.getUser().then(user => {
      this.user = user;
    });

  }

  ngOnInit() {
  }

  login() {
    return this.manager.signinRedirect();
  }
}

export function getClientSettings(): UserManagerSettings {
  return {
      authority: 'https://localhost:4001',
      client_id: 'angular_spa',
      redirect_uri: 'https://localhost:5001',
      post_logout_redirect_uri: 'https://localhost:5001',
      response_type:"id_token token",
      scope:"openid profile email api.read",
      filterProtocolClaims: true,
      loadUserInfo: true,
      automaticSilentRenew: true,
      silent_redirect_uri: 'https://localhost:5001/silent-refresh.html'
  };
}
