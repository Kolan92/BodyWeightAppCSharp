import { UserProfile } from './../models/UserProfile';
import { Component, OnInit } from '@angular/core';
import { ProfileService } from '../profile.service';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import * as moment from 'moment';
import { ToastrService } from 'ngx-toastr';
import { CustomValidators } from 'ngx-custom-validators';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.css']
})
export class ProfileComponent implements OnInit {
  private profile: UserProfile;
  public profileForm: FormGroup;
  get f() {
    return this.profileForm.controls;
  }
  public moment: any = moment;
  public buttonMessage = 'Create profile';
  private toastOptions = { positionClass: 'toast-top-right' };

  constructor(
      private profileService: ProfileService,
      private formBuilder: FormBuilder,
      private toasterService: ToastrService
      ) {
  }

  ngOnInit() {
    this.profileService.getUserProfile()
      .subscribe(profile => {
        this.profile = profile;
        this.buttonMessage = 'Update profile';
        this.profileForm.get('height').patchValue(this.profile.height);
        this.profileForm.get('birthDate').patchValue(moment(this.profile.birthDate).format('YYYY-MM-DD'));

    });

    const now = new Date();
    const dateValidators = [CustomValidators.maxDate(now), CustomValidators.minDate(new Date().setFullYear(now.getFullYear() - 135))];
    this.profileForm = this.formBuilder.group({
      height: new FormControl(180, [Validators.min(20), Validators.max(300)]),
      birthDate: new FormControl(moment(now).format('YYYY-MM-DD'), dateValidators),
    });
  }


  onSubmit(updateProfile: UserProfile) {

    this.profileService.updateUserProfile(updateProfile)
      .subscribe(
        _ => {
          this.toasterService.success('Successfully updated profile', '', this.toastOptions)
          this.buttonMessage = 'Update profile';
        }
    );
  }
}
