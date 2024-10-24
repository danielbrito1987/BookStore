import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Autor } from 'src/models/autor.model';
import { AutorService } from 'src/services/autor.service';
import { ToastrService } from 'ngx-toastr';
import * as bootstrap from 'bootstrap';

@Component({
  selector: 'app-autor',
  templateUrl: './autor.component.html',
  styleUrls: ['./autor.component.css']
})
export class AutorComponent implements OnInit {
  isLoading = false;
  autores: Autor[] = [];
  autorForm: FormGroup;
  selectedAutor: Autor | null = null;
  isEditing: boolean = false;
  filterNome: string = '';

  constructor(
    private authorService: AutorService,
    private toastr: ToastrService,
    private fb: FormBuilder
  ) {
    this.autorForm = this.fb.group({
      codAutor: [''],
      nome: ['', [Validators.required, Validators.maxLength(40)]]
    });
  }

  ngOnInit() {
    this.loadAutores();
  }

  loadAutores() {
    this.isLoading = true;

    this.authorService.getAll().subscribe(data => {
      this.autores = data.filter(author =>
        author.nome.toLowerCase().includes(this.filterNome.toLowerCase())
      );

      this.isLoading = false;
    });
  }

  openModal(autor?: Autor) {
    if (autor) {
      this.selectedAutor = autor;
      this.isEditing = true;
      this.autorForm.patchValue(autor);
    } else {
      this.isEditing = false;
      this.autorForm.reset();
    }

    const modalElement = document.getElementById('modalAutor');

    if (modalElement) {
      const modal = new bootstrap.Modal(modalElement);
      modal.show();
    } else {
      console.error('Modal element not found');
    }
  }

  closeModal() {
    const modalElement = document.getElementById('modalAutor');

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

  saveAutor() {
    if (this.autorForm.valid) {
      this.isLoading = true;

      if (this.isEditing) {
        this.authorService.update(this.autorForm.value).subscribe(() => {
          this.toastr.success('Autor alterado com sucesso!');
          this.loadAutores();
        }, (error) => {
          this.isLoading = false;
          this.toastr.error(error);
        });
      } else {
        this.authorService.create(this.autorForm.value).subscribe(() => {
          this.toastr.success('Autor cadastrado com sucesso!');
          this.loadAutores();
        }, (error) => {
          this.isLoading = false;
          this.toastr.error(error);
        });
      }

      this.closeModal();
    }
  }

  confirmDelete(autor: Autor) {
    const confirmDelete = confirm('Tem certeza que deseja excluir este autor?');
    if (confirmDelete) {
      this.isLoading = true;

      this.authorService.delete(autor.codAutor).subscribe(() => {
        this.toastr.success('Autor excluído com sucesso!');
        this.loadAutores();
      }, (error) => {
        this.isLoading = false;
        this.toastr.error(error);
      });
    }
  }
}
