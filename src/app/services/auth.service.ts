import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { TokenModel } from '../models/token.model';
import { jwtDecode, JwtPayload } from 'jwt-decode';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  tokenDecode: TokenModel = new TokenModel();

  constructor(private router: Router) {}

  isAuthenticated() {
    const token: string | null = localStorage.getItem('token');

    if (token) {
      const decode: JwtPayload | any = jwtDecode(token);
      this.tokenDecode.id = decode['http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier'];
      this.tokenDecode.email = decode['http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress'];
      this.tokenDecode.name = decode['http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name'];
      this.tokenDecode.username =  decode['Username'];

      const exp = decode.exp;
      const now = new Date().getTime() / 1000;
      if (exp < now) {
        localStorage.removeItem('token');
        this.router.navigate(['/login']);
        return false;
      }

      return true;
    }

    this.router.navigate(['/login']);
    return false;
  }
}
