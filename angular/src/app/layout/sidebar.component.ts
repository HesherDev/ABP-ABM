import { ChangeDetectionStrategy, Component, OnInit, Renderer2 } from '@angular/core';
import { LayoutStoreService } from '@shared/layout/layout-store.service';

@Component({
  selector: 'sidebar',
  templateUrl: './sidebar.component.html',
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class SidebarComponent implements OnInit {
  sidebarExpanded: boolean;

  constructor(private renderer: Renderer2, private _layoutStore: LayoutStoreService) {}

  ngOnInit(): void {
    this._layoutStore.sidebarExpanded.subscribe((value) => {
      this.sidebarExpanded = value;
      this.toggleSidebar();
    });
  }

  toggleSidebar(): void {
    if (this.sidebarExpanded) {
      this.showSidebar();
    } else {
      this.hideSidebar();
    }
  }

  showSidebar(): void {
    this.renderer.removeClass(document.body, 'sidebar-collapse');
    this.renderer.addClass(document.body, 'sidebar-open');
  }

  hideSidebar(): void {
    this.renderer.removeClass(document.body, 'sidebar-open');
    this.renderer.addClass(document.body, 'sidebar-collapse');
  }
}
