import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { NgxSpinnerService } from 'ngx-spinner';
import { BaseComponent, SpinnerType } from 'src/app/base/base.component';
import { AlertifyService } from 'src/app/services/admin/alertify.service';
import { UserAuthService } from 'src/app/services/common/models/user-auth.service';
import { UserService } from 'src/app/services/common/models/user.service';

@Component({
  selector: 'app-update-password',
  templateUrl: './update-password.component.html',
  styleUrls: ['./update-password.component.scss']
})
export class UpdatePasswordComponent extends BaseComponent implements OnInit {
  state: any = false;

  constructor(spinner: NgxSpinnerService,
    private userAuthService: UserAuthService, private activatedRoute: ActivatedRoute,
    private alertifyService: AlertifyService, private userService: UserService,
    private router: Router) {
    super(spinner);
  }

  ngOnInit(): void {
    this.showSpinner(SpinnerType.BallAtom);

    this.activatedRoute.params.subscribe({
      next: async params => {
        const userId = params["userId"];
        const resetToken = params["resetToken"];
        this.state = await this.userAuthService.verifyResetToken(resetToken, userId);
        this.hideSpinner(SpinnerType.BallAtom);
      }
    })
  }

  updatePassword(txtPassword: string, txtPasswordConfirm: string) {
    // this.showSpinner(SpinnerType.BallAtom);
    this.activatedRoute.params.subscribe({
      next: async params => {
        const userId: string = params["userId"];
        const resetToken: string = params["resetToken"];

        await this.userService.updatePassword(userId, resetToken, txtPassword, txtPasswordConfirm);

        this.router.navigate(["/login"]);


      }
    })

  }
}
