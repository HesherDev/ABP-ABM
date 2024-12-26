import { Component, EventEmitter, Output, Injector } from '@angular/core';
import { BsModalRef } from 'ngx-bootstrap/modal';
import { TeamServiceProxy, TeamDto } from '@shared/service-proxies/service-proxies';
import { AppComponentBase } from '@shared/app-component-base';

@Component({
  templateUrl: './create-team-dialog.component.html'
})
export class CreateTeamDialogComponent extends AppComponentBase {
  saving = false;
  team = new TeamDto();

  @Output() onSave = new EventEmitter<any>();

  constructor(
    public bsModalRef: BsModalRef, 
    protected _teamService: TeamServiceProxy,
    injector: Injector
  ) {
    super(injector);
  }

  save(): void {
    this.saving = true;

    this._teamService.create(this.team).subscribe(
      () => {
        this.notify.success(this.l('SavedSuccessfully'));
        this.bsModalRef.hide();
        this.onSave.emit();
      },
      () => {
        this.saving = false;
      }
    );
  }
}
