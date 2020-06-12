import { UserProfile } from './../models/UserProfile';
import { Component, OnInit } from '@angular/core';
import { ProfileService } from '../profile.service';
import { FormBuilder, FormControl } from '@angular/forms';
import * as moment from 'moment';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.css']
})
export class ProfileComponent implements OnInit {
  private profile: UserProfile;
  public profileForm;
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

    this.profileForm = this.formBuilder.group({
      height: new FormControl(180, []),
      birthDate: new FormControl(moment(new Date()).format('YYYY-MM-DD'), []),
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
