import { Component, OnInit } from '@angular/core';
import { User, UserManager, UserManagerSettings } from 'oidc-client';
import { Router, ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-auth-callback',
  templateUrl: './auth-callback.component.html',
  styleUrls: ['./auth-callback.component.css']
})
export class AuthCallbackComponent implements OnInit {
  private user: User | null;
  private manager = new UserManager(getClientSettings());
  error: boolean;

  constructor(private router: Router, private route: ActivatedRoute ) { }

  async ngOnInit() {

    if (this.route.snapshot.fragment.indexOf('error') >= 0) {
      this.error=true;   
      return;
    }
    this.user = await this.manager.signinRedirectCallback();    
    this.router.navigate(['/fetc-data']);  
  }

}

export function getClientSettings(): UserManagerSettings {
  return {
      authority: 'https://localhost:4001',
      client_id: 'angular_spa',
      redirect_uri: 'https://localhost:5001/auth-callback/',
      post_logout_redirect_uri: 'https://localhost:5001/',
      response_type:"id_token token",
      scope:"openid profile email api.read",
      filterProtocolClaims: true,
      loadUserInfo: true,
      automaticSilentRenew: true,
      silent_redirect_uri: 'https://localhost:5001/silent-refresh.html'
  };
}
