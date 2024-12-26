import { ChangeDetectorRef, Component, Injector, OnInit, ViewChild } from '@angular/core';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { Paginator } from 'primeng/paginator';
import { Table } from 'primeng/table';
import { finalize } from 'rxjs/operators';
import { PagedListingComponentBase } from '@shared/paged-listing-component-base';
import { TeamDto, TeamDtoPagedResultDto, TeamServiceProxy } from '@shared/service-proxies/service-proxies';
import { CreateTeamDialogComponent } from './create-team/create-team-dialog.component';
import { EditTeamDialogComponent } from './edit-team/edit-team-dialog.component';
import { LazyLoadEvent } from '@node_modules/primeng/api';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-teams',
  templateUrl: './teams.component.html',
  styleUrls: ['./teams.component.css'],
})
export class TeamsComponent extends PagedListingComponentBase<TeamDto> {
  Teams: TeamDto[] = [];
  keyword = '';
  isActive: boolean | null = null;
  advancedFiltersVisible = false;

  @ViewChild('dataTable', { static: true }) dataTable: Table;
  @ViewChild('paginator', { static: true }) paginator: Paginator;

  constructor(
    injector: Injector,
    private _teamService: TeamServiceProxy,
    private _modalService: BsModalService,
    private _activatedRoute: ActivatedRoute,
    cd: ChangeDetectorRef
  ) {
    super(injector, cd);
    this.keyword = this._activatedRoute.snapshot.queryParams['filterText'] || '';
  }

  createTeam(): void {
    this.showCreateOrEditTeamDialog();
  }

  editTeam(team: TeamDto): void {
    this.showCreateOrEditTeamDialog(team.id);
  }

  clearFilters(): void {
    this.keyword = '';
    this.isActive = null;
    this.refresh();  // Refresh to clear the table
  }

  list(event?: LazyLoadEvent): void {
    // Reset pagination if necessary
    if (this.primengTableHelper.shouldResetPaging(event)) {
      this.paginator.changePage(0);
  
      if (this.primengTableHelper.records && this.primengTableHelper.records.length > 0) {
        return;
      }
    }
  
    this.primengTableHelper.showLoadingIndicator();
  
    // Get pagination parameters
    const skipCount = this.primengTableHelper.getSkipCount(this.paginator, event ?? {});
    const maxResultCount = this.primengTableHelper.getMaxResultCount(this.paginator, event);
  
    // Prepare sorting and filters
    const sorting = this.primengTableHelper.getSorting(this.dataTable);
    const sortingString = sorting ? sorting.toString() : '';  // Convert to string if any
    const keyword = this.keyword ?? '';  // Ensure keyword is a string
    const isActive = this.isActive;  // Get the active status filter
  
    // Llamar al servicio con los parámetros individuales
    this._teamService
      .getAll(sortingString, keyword, isActive, skipCount, maxResultCount)
      .pipe(finalize(() => this.primengTableHelper.hideLoadingIndicator()))
      .subscribe((result: TeamDtoPagedResultDto) => {
        this.primengTableHelper.records = result.items;
        this.primengTableHelper.totalRecordsCount = result.totalCount;
        this.cd.detectChanges();  // Asegúrate de que la UI se actualice después de cargar los datos
      });
  }
  
  delete(team: TeamDto): void {
    abp.message.confirm(
      this.l('TeamDeleteWarningMessage', team.name),
      undefined,
      (result: boolean) => {
        if (result) {
          this._teamService.delete(team.id).subscribe(() => {
            abp.notify.success(this.l('SuccessfullyDeleted'));
            this.refresh();  // Refresh the table
          });
        }
      }
    );
  }

  private showCreateOrEditTeamDialog(id?: number): void {
    let modalRef: BsModalRef;

    if (id) {
      modalRef = this._modalService.show(EditTeamDialogComponent, {
        class: 'modal-lg',
        initialState: { id },
      });
    } else {
      modalRef = this._modalService.show(CreateTeamDialogComponent, {
        class: 'modal-lg',
      });
    }

    modalRef.content.onSave.subscribe(() => this.refresh());
  }
}
