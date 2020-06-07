import { UserProfile } from './../models/UserProfile';
import { Component, OnInit } from '@angular/core';
import { ProfileService } from '../profile.service';
import { FormBuilder, FormControl } from '@angular/forms';
import * as moment from 'moment';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.css']
})
export class ProfileComponent implements OnInit {
  private profile: UserProfile;
  public profileForm;
  public buttonMessage = 'Create profile';

  constructor(
      private profileService: ProfileService,
      private formBuilder: FormBuilder) {
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


  onSubmit(updateProfil: UserProfile) {

    this.profileService.updateUserProfile(updateProfil)
      .subscribe(
        x => this.buttonMessage = 'Update profile'
    );
    console.warn('Your profile has been updated', updateProfil);
  }
}
