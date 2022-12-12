import { Component, OnInit } from '@angular/core';
import { NgxSpinnerService } from 'ngx-spinner';
import { BaseComponent, SpinnerType } from 'src/app/base/base.component';
import { AlertifyService, MessageType, Position } from 'src/app/services/admin/alertify.service';
import { UserAuthService } from 'src/app/services/common/models/user-auth.service';

@Component({
  selector: 'app-password-reset',
  templateUrl: './password-reset.component.html',
  styleUrls: ['./password-reset.component.scss']
})
export class PasswordResetComponent extends BaseComponent implements OnInit {

  constructor(spinner: NgxSpinnerService, private userAuthService: UserAuthService, private alertfyService: AlertifyService) {
    super(spinner);
  }

  ngOnInit(): void {


  }
  passwordReset(mail: string) {
    this.showSpinner(SpinnerType.BallAtom);
    this.userAuthService.passwordReset(mail, () => {
      this.hideSpinner(SpinnerType.BallAtom)
      this.alertfyService.message("Mail başarıyla gönderilmiştir.", {
        messageType: MessageType.Notify,
        position: Position.TopRight
      })
    });

  }
}
