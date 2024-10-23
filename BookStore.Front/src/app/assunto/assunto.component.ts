import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Assunto } from 'src/models/assunto.model';
import { AssuntoService } from 'src/services/assunto.service';
import * as bootstrap from 'bootstrap';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-assunto',
  templateUrl: './assunto.component.html',
  styleUrls: ['./assunto.component.css']
})
export class AssuntoComponent implements OnInit {
  isLoading = false;
  assuntos: Assunto[] = [];
  assuntoForm: FormGroup;
  selectedAutor: Assunto | null = null;
  isEditing: boolean = false;
  filterDescricao: string = '';

  constructor(
    private toastr: ToastrService,
    private assuntoService: AssuntoService,
    private fb: FormBuilder
  ) {
    this.assuntoForm = this.fb.group({
      codAssunto: [''],
      descricao: ['', Validators.required]
    });
  }

  ngOnInit(): void {
    this.loadAssuntos();
  }

  loadAssuntos() {
    this.isLoading = true;

    this.assuntoService.getAll().subscribe(data => {
      this.assuntos = data.filter(assunto =>
        assunto.descricao.toLowerCase().includes(this.filterDescricao.toLowerCase())
      );

      this.isLoading = false;
    });
  }

  openModal(assunto?: Assunto) {
    if (assunto) {
      this.selectedAutor = assunto;
      this.isEditing = true;
      this.assuntoForm.patchValue(assunto);
    } else {
      this.isEditing = false;
      this.assuntoForm.reset();
    }

    const modalElement = document.getElementById('modalAssunto');

    if (modalElement) {
      const modal = new bootstrap.Modal(modalElement);
      modal.show();
    } else {
      console.error('Modal element not found');
    }
  }

  closeModal() {
    const modalElement = document.getElementById('modalAssunto');

    if (modalElement) {
      const modal = bootstrap.Modal.getInstance(modalElement);
      if (modal) {
        modal.hide();
      } else {
        console.error('Modal instance not found');
      }
    } else {
      console.error('Modal element not found');
    }
  }

  saveAssunto() {
    if (this.assuntoForm.valid) {
      this.isLoading = true;

      if (this.isEditing) {
        this.assuntoService.update(this.assuntoForm.value).subscribe(() => {
          this.toastr.success('Assunto alterado com sucesso!');
          this.loadAssuntos();
        });
      } else {
        this.assuntoService.create(this.assuntoForm.value).subscribe(() => {
          this.toastr.success('Assunto cadastrado com sucesso!');
          this.loadAssuntos();
        });
      }

      this.closeModal();
    }
  }

  confirmDelete(assunto: Assunto) {
    const confirmDelete = confirm('Tem certeza que deseja excluir este assunto?');
    if (confirmDelete) {
      this.isLoading = true;

      this.assuntoService.delete(assunto.codAssunto).subscribe(() => {
        this.toastr.success('Assunto excluído com sucesso!');
        this.loadAssuntos();
      }, (error) => {
        this.isLoading = false;
        this.toastr.error(error);
      });
    }
  }
}
