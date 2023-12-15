import { NgModule } from "@angular/core";
import { FormsModule, ReactiveFormsModule } from "@angular/forms";
import { HTTP_INTERCEPTORS, HttpClientModule } from "@angular/common/http";
import { BrowserModule } from "@angular/platform-browser";
import { BrowserAnimationsModule } from "@angular/platform-browser/animations";
import { HashLocationStrategy, LocationStrategy } from "@angular/common";
import { AppRoutingModule } from "./app-routing.module";
import { AccordionModule } from "primeng/accordion";
import { AutoCompleteModule } from "primeng/autocomplete";
import { AvatarModule } from "primeng/avatar";
import { AvatarGroupModule } from "primeng/avatargroup";
import { BadgeModule } from "primeng/badge";
import { BreadcrumbModule } from "primeng/breadcrumb";
import { ButtonModule } from "primeng/button";
import { CalendarModule } from "primeng/calendar";
import { CardModule } from "primeng/card";
import { CarouselModule } from "primeng/carousel";
import { CascadeSelectModule } from "primeng/cascadeselect";
import { ChartModule } from "primeng/chart";
import { CheckboxModule } from "primeng/checkbox";
import { ChipModule } from "primeng/chip";
import { ChipsModule } from "primeng/chips";
import { CodeHighlighterModule } from "primeng/codehighlighter";
import { ConfirmDialogModule } from "primeng/confirmdialog";
import { ConfirmPopupModule } from "primeng/confirmpopup";
import { ColorPickerModule } from "primeng/colorpicker";
import { ContextMenuModule } from "primeng/contextmenu";
import { DataViewModule } from "primeng/dataview";
import { DialogModule } from "primeng/dialog";
import { DividerModule } from "primeng/divider";
import { DropdownModule } from "primeng/dropdown";
import { FieldsetModule } from "primeng/fieldset";
import { FileUploadModule } from "primeng/fileupload";
import { FullCalendarModule } from "@fullcalendar/angular";
import { GalleriaModule } from "primeng/galleria";
import { ImageModule } from "primeng/image";
import { InplaceModule } from "primeng/inplace";
import { InputNumberModule } from "primeng/inputnumber";
import { InputMaskModule } from "primeng/inputmask";
import { InputSwitchModule } from "primeng/inputswitch";
import { InputTextModule } from "primeng/inputtext";
import { InputTextareaModule } from "primeng/inputtextarea";
import { KnobModule } from "primeng/knob";
import { LightboxModule } from "primeng/lightbox";
import { ListboxModule } from "primeng/listbox";
import { MegaMenuModule } from "primeng/megamenu";
import { MenuModule } from "primeng/menu";
import { MenubarModule } from "primeng/menubar";
import { MessagesModule } from "primeng/messages";
import { MessageModule } from "primeng/message";
import { MultiSelectModule } from "primeng/multiselect";
import { OrderListModule } from "primeng/orderlist";
import { OrganizationChartModule } from "primeng/organizationchart";
import { OverlayPanelModule } from "primeng/overlaypanel";
import { PaginatorModule } from "primeng/paginator";
import { PanelModule } from "primeng/panel";
import { PanelMenuModule } from "primeng/panelmenu";
import { PasswordModule } from "primeng/password";
import { PickListModule } from "primeng/picklist";
import { ProgressBarModule } from "primeng/progressbar";
import { RadioButtonModule } from "primeng/radiobutton";
import { RatingModule } from "primeng/rating";
import { RippleModule } from "primeng/ripple";
import { ScrollPanelModule } from "primeng/scrollpanel";
import { ScrollTopModule } from "primeng/scrolltop";
import { SelectButtonModule } from "primeng/selectbutton";
import { SidebarModule } from "primeng/sidebar";
import { SkeletonModule } from "primeng/skeleton";
import { SlideMenuModule } from "primeng/slidemenu";
import { SliderModule } from "primeng/slider";
import { SplitButtonModule } from "primeng/splitbutton";
import { SplitterModule } from "primeng/splitter";
import { StepsModule } from "primeng/steps";
import { TabMenuModule } from "primeng/tabmenu";
import { TableModule } from "primeng/table";
import { TabViewModule } from "primeng/tabview";
import { TagModule } from "primeng/tag";
import { TerminalModule } from "primeng/terminal";
import { TieredMenuModule } from "primeng/tieredmenu";
import { TimelineModule } from "primeng/timeline";
import { ToastModule } from "primeng/toast";
import { ToggleButtonModule } from "primeng/togglebutton";
import { ToolbarModule } from "primeng/toolbar";
import { TooltipModule } from "primeng/tooltip";
import { TreeModule } from "primeng/tree";
import { TreeTableModule } from "primeng/treetable";
import { VirtualScrollerModule } from "primeng/virtualscroller";
import { AppComponent } from "./app.component";
import { AppMainComponent } from "./app.main.component";
import { AppMenuComponent } from "./app.menu.component";
import { AppMenuitemComponent } from "./app.menuitem.component";
import { AppTopBarComponent } from "./app.topbar.component";
import { AppFooterComponent } from "./app.footer.component";
import dayGridPlugin from "@fullcalendar/daygrid";
import timeGridPlugin from "@fullcalendar/timegrid";
import interactionPlugin from "@fullcalendar/interaction";
import { HomeComponent } from "./Components/home/home.component";
import { EmpleadoComponent } from "./Components/empleado/empleado.component";
import { PeriodoComponent } from "./Components/periodo/periodo.component";
import { RolComponent } from "./Components/rol/rol.component";
import { AuthenticationComponent } from "./Components/authentication/authentication.component";
import { PuestoComponent } from "./Components/puesto/puesto.component";
import { UsuarioComponent } from './Components/usuario/usuario.component';
import { TipoComponenteComponent } from './Components/tipo-componente/tipo-componente.component';
import { MessageService } from "primeng/api";
import { DialogService } from "primeng/dynamicdialog";
import { ComponenteFormComponent } from "./Components/componentes/componente-form/componente-form.component";
import { RolFormComponent } from "./Components/rol/rol-form/rol-form.component";
import { UsuarioFormComponent } from "./Components/usuario/usuario-form/usuario-form.component";
import { FooterDialogComponent } from "./Components/Shared/footer-dialog/footer-dialog.component";
import { ComponentesComponent } from "./Components/componentes/componentes.component";
import { CambiarPasswordComponent } from "./Components/usuario/cambiar-password/cambiar-password.component";
import { EmpleadoFormComponent } from './Components/empleado/empleado-form/empleado-form.component';
import { PuestoFormComponent } from './Components/puesto/puesto-form/puesto-form.component';
import { TipoComponenteFormComponent } from './Components/tipo-componente/tipo-componente-form/tipo-componente-form.component';
import { AuthInterceptor } from "./interceptor/auth-interceptor/auth.interceptor";
import { SpinnerInterceptor } from "./interceptor/spinner-interceptor/spinner.interceptor";
import { SpinnerModule } from "./Components/spinner/spinner.module";
import { ConfiguracionGlobalComponent } from './Components/configuracion-global/configuracion-global.component';
import { ConfiguracionGlobalFormComponent } from './Components/configuracion-global/configuracion-global-form/configuracion-global-form.component';
import { MenuService } from "./app.menu.service";
import { ErrorLogComponent } from "./Components/error-log/error-log.component";
import { ErrorFormComponent } from "./Components/error-log/error-form/error-form.component";
import { PeriodoFormComponent } from "./Components/periodo/periodo-form/periodo-form.component";
import { TipoDatoComponent } from "./Components/tipo-dato/tipo-dato.component";
import { TipoDatoFormComponent } from "./Components/tipo-dato/tipo-dato-form/tipo-dato-form.component";
import { ColumnaExcelComponent } from "./Components/columna-excel/columna-excel.component";
import { ColumnaExcelFormComponent } from "./Components/columna-excel/columna-excel-form/columna-excel-form.component";
import { PlanillaPagosComponent } from "./Components/planilla-pagos/planilla-pagos.component";
import { BoletaPagoComponent } from "./Components/boleta-pago/boleta-pago.component";
import { CargarPlanillaComponent } from "./Components/planilla-pagos/cargar-planilla/cargar-planilla.component";
import { TablaPagosComponent } from "./Components/planilla-pagos/tabla-pagos/tabla-pagos.component";
import { DetalleBoletaComponent } from "./Components/boleta-pago/detalle-boleta/detalle-boleta.component";

FullCalendarModule.registerPlugins([
    dayGridPlugin,
    timeGridPlugin,
    interactionPlugin,
]);

@NgModule({
    imports: [
        BrowserModule,
        FormsModule,
        AppRoutingModule,
        HttpClientModule,
        BrowserAnimationsModule,
        AccordionModule,
        AutoCompleteModule,
        AvatarModule,
        AvatarGroupModule,
        BadgeModule,
        BreadcrumbModule,
        ButtonModule,
        CalendarModule,
        CardModule,
        CarouselModule,
        CascadeSelectModule,
        ChartModule,
        CheckboxModule,
        ChipModule,
        ChipsModule,
        CodeHighlighterModule,
        ConfirmDialogModule,
        ConfirmPopupModule,
        ColorPickerModule,
        ContextMenuModule,
        DataViewModule,
        DialogModule,
        DividerModule,
        DropdownModule,
        FieldsetModule,
        FileUploadModule,
        FullCalendarModule,
        GalleriaModule,
        ImageModule,
        InplaceModule,
        InputNumberModule,
        InputMaskModule,
        InputSwitchModule,
        InputTextModule,
        InputTextareaModule,
        KnobModule,
        LightboxModule,
        ListboxModule,
        MegaMenuModule,
        MenuModule,
        MenubarModule,
        MessageModule,
        MessagesModule,
        MultiSelectModule,
        OrderListModule,
        OrganizationChartModule,
        OverlayPanelModule,
        PaginatorModule,
        PanelModule,
        PanelMenuModule,
        PasswordModule,
        PickListModule,
        ProgressBarModule,
        RadioButtonModule,
        RatingModule,
        RippleModule,
        ScrollPanelModule,
        ScrollTopModule,
        SelectButtonModule,
        SidebarModule,
        SkeletonModule,
        SlideMenuModule,
        SliderModule,
        SplitButtonModule,
        SplitterModule,
        StepsModule,
        TableModule,
        TabMenuModule,
        TabViewModule,
        TagModule,
        TerminalModule,
        TimelineModule,
        TieredMenuModule,
        ToastModule,
        ToggleButtonModule,
        ToolbarModule,
        TooltipModule,
        TreeModule,
        TreeTableModule,
        VirtualScrollerModule,
        ReactiveFormsModule,
        SpinnerModule
    ],
    declarations: [
        AppComponent,
        AppMainComponent,
        AppMenuComponent,
        AppMenuitemComponent,
        AppTopBarComponent,
        AppFooterComponent,
        HomeComponent,
        EmpleadoComponent,
        PeriodoComponent,
        RolComponent,
        AuthenticationComponent,
        PuestoComponent,
        UsuarioComponent,
        TipoComponenteComponent,
        RolFormComponent,
        ComponentesComponent,
        ComponenteFormComponent,
        FooterDialogComponent,
        UsuarioFormComponent,
        CambiarPasswordComponent,
        EmpleadoFormComponent,
        PeriodoFormComponent,
        PuestoFormComponent,
        TipoComponenteFormComponent,
        ConfiguracionGlobalComponent,
        ConfiguracionGlobalFormComponent,
        TipoDatoComponent,
        TipoDatoFormComponent,
        ErrorLogComponent,
        ErrorFormComponent,
        ColumnaExcelComponent,
        ColumnaExcelFormComponent,
        PlanillaPagosComponent,
        BoletaPagoComponent,
        CargarPlanillaComponent,
        TablaPagosComponent,
        BoletaPagoComponent,
        DetalleBoletaComponent

    ],
    entryComponents: [
        ComponenteFormComponent,
        RolFormComponent,
        UsuarioFormComponent,
    ],
    providers: [
        { provide: LocationStrategy, useClass: HashLocationStrategy },
        { provide: HTTP_INTERCEPTORS, useClass: AuthInterceptor, multi: true },
        { provide: HTTP_INTERCEPTORS, useClass: SpinnerInterceptor, multi: true },
        MessageService,
        DialogService,
        MenuService
    ],
    bootstrap: [AppComponent],
})
export class AppModule { }
