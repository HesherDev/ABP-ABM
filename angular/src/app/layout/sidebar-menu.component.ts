import { Component, Injector, OnInit } from '@angular/core';
import { AppComponentBase } from '@shared/app-component-base';
import { Router, NavigationEnd, PRIMARY_OUTLET } from '@angular/router';
import { BehaviorSubject } from 'rxjs';
import { MenuItem } from '@shared/layout/menu-item';

@Component({
  selector: 'sidebar-menu',
  templateUrl: './sidebar-menu.component.html',
})
export class SidebarMenuComponent extends AppComponentBase implements OnInit {
  menuItems: MenuItem[] = [];
  menuItemsMap: { [key: number]: MenuItem } = {};
  activatedMenuItems: MenuItem[] = [];
  routerEvents: BehaviorSubject<any> = new BehaviorSubject(undefined);
  homeRoute = '/app/about';

  constructor(injector: Injector, private router: Router) {
    super(injector);
  }

  ngOnInit(): void {
    this.menuItems = this.getMenuItems();
    this.initializeMenuItems(this.menuItems);

    // Observa eventos de navegación para actualizar el menú activo.
    this.router.events.subscribe((event) => {
      if (event instanceof NavigationEnd) {
        const currentUrl = event.url !== '/' ? event.url : this.homeRoute;
        this.updateActiveMenuItems(currentUrl);
      }
    });
  }

  /**
   * Devuelve los elementos del menú.
   */
  getMenuItems(): MenuItem[] {
    return [
      new MenuItem(this.l('About'), '/app/about', 'fas fa-info-circle'),
      new MenuItem(this.l('HomePage'), '/app/home', 'fas fa-home'),
      new MenuItem(this.l('Roles'), '/app/roles', 'fas fa-theater-masks'),
      new MenuItem(this.l('Tenants'), '/app/tenants', 'fas fa-building'),
      new MenuItem(this.l('Users'), '/app/users', 'fas fa-users'),
      new MenuItem(this.l('Teams'), '/app/teams', 'fas fa-home'),
    ];
  }

  /**
   * Inicializa los elementos del menú con IDs y jerarquías.
   */
  initializeMenuItems(items: MenuItem[], parentId?: number): void {
    items.forEach((item, index) => {
      item.id = parentId ? Number(`${parentId}${index + 1}`) : index + 1;
      if (parentId) item.parentId = parentId;
      this.menuItemsMap[item.id] = item;

      if (item.children) {
        this.initializeMenuItems(item.children, item.id);
      }
    });
  }

  /**
   * Actualiza los elementos del menú activo en función de la URL.
   */
  updateActiveMenuItems(url: string): void {
    this.deactivateAllMenuItems(this.menuItems);
    const activeItems = this.findMenuItemsByUrl(url, this.menuItems);
    activeItems.forEach((item) => this.activateMenuItem(item));
  }

  /**
   * Desactiva todos los elementos del menú.
   */
  deactivateAllMenuItems(items: MenuItem[]): void {
    items.forEach((item) => {
      item.isActive = false;
      item.isCollapsed = true;
      if (item.children) {
        this.deactivateAllMenuItems(item.children);
      }
    });
  }

  /**
   * Busca elementos del menú que coincidan con una URL.
   */
  findMenuItemsByUrl(url: string, items: MenuItem[]): MenuItem[] {
    let result: MenuItem[] = [];
    items.forEach((item) => {
      if (item.route === url) {
        result.push(item);
      } else if (item.children) {
        result = result.concat(this.findMenuItemsByUrl(url, item.children));
      }
    });
    return result;
  }

  /**
   * Activa un elemento del menú y sus padres.
   */
  activateMenuItem(item: MenuItem): void {
    item.isActive = true;
    item.isCollapsed = false;
    if (item.parentId) {
      const parent = this.menuItemsMap[item.parentId];
      if (parent) this.activateMenuItem(parent);
    }
  }
  //asegura que Angular realice el seguimiento correcto de los elementos en la lista.
  trackByItem(index: number, item: MenuItem): number | string {
    return item.id || index; // Usa el ID del elemento o el índice como fallback
  }
  
  /**
   * Comprueba si un elemento del menú es visible basado en permisos.
   */
  isMenuItemVisible(item: MenuItem): boolean {
    return item.permissionName ? this.permission.isGranted(item.permissionName) : true;
  }
}
