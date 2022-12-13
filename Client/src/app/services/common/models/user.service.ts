import { Injectable } from '@angular/core';
import { firstValueFrom, Observable } from 'rxjs';
import { Create_User } from '../../../contracts/users/create_user';
import { User } from '../../../entities/user';
import { CustomToastrService } from '../../ui/custom-toastr.service';
import { HttpClientService } from '../http-client.service';

@Injectable({
  providedIn: 'root'
})
export class UserService {
  constructor(private httpClientService: HttpClientService, private toastrService: CustomToastrService) { }

  async create(user: User): Promise<Create_User> {
    const observable: Observable<Create_User | User> = this.httpClientService.post<Create_User | User>({
      controller: "auth"
    }, user);
    return await firstValueFrom(observable) as Create_User;
  }
  async updatePassword(userId: string, resetToken: string, password: string, passwordConfirm: string) {
    const observable: any = this.httpClientService.post({
      action: "update-password",
      controller: "users"
    }, {
      userId: userId,
      resetToken: resetToken,
      password: password,
      passwordConfirm: passwordConfirm
    });



  }
}
