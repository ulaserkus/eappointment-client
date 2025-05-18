import { Pipe, PipeTransform } from '@angular/core';
import { UserModel } from '../models/user-model';

@Pipe({
  name: 'user',
  standalone: true
})
export class UserPipe implements PipeTransform {

  transform(value: UserModel[], search: string): UserModel[] {
      if (!search) {
        return value;
      }
  
      return value.filter(
        (user) =>
          user.fullName.toLowerCase().includes(search.toLowerCase()) ||
          user.userName.toLowerCase().includes(search.toLowerCase()) ||
          user.email.toLowerCase().includes(search.toLowerCase()) 
      );
    }

}
