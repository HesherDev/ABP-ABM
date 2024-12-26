import { Component, Injector, OnInit, EventEmitter, Output, ChangeDetectorRef } from '@angular/core';
import { BsModalRef } from 'ngx-bootstrap/modal';
import { TeamServiceProxy, TeamDto } from '../../../shared/service-proxies/service-proxies';
import { AppComponentBase } from '../../../shared/app-component-base';

@Component({
  templateUrl: './edit-team-dialog.component.html'
})
export class EditTeamDialogComponent extends AppComponentBase implements OnInit {
  saving = false;
  team = new TeamDto();
  id: number;

  @Output() onSave = new EventEmitter<any>();

  constructor(
    injector: Injector,
    protected _teamService: TeamServiceProxy,
    public bsModalRef: BsModalRef,
    private cd: ChangeDetectorRef
  ) {
    super(injector);
  }

  ngOnInit(): void {
    this._teamService.get(this.id).subscribe((result) => {
      this.team = result;
    });
  }

  save(): void {
    this.saving = true;

    this._teamService.update(this.team).subscribe(
      () => {
        this.notify.info(this.l('SavedSuccessfully'));
        this.bsModalRef.hide();
        this.onSave.emit();
      },
      () => {
        this.saving = false;
      }
    );
  }
}
