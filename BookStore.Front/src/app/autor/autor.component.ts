import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { Autor } from 'src/models/autor.model';
import { AutorService } from 'src/services/autor.service';
import * as bootstrap from 'bootstrap';

@Component({
  selector: 'app-autor',
  templateUrl: './autor.component.html',
  styleUrls: ['./autor.component.css']
})
export class AutorComponent implements OnInit {
  autores: Autor[] = [];
  autorForm: FormGroup;
  selectedAutor: Autor | null = null;
  isEditing: boolean = false;
  filterNome: string = '';

  constructor(private authorService: AutorService, private fb: FormBuilder) {
    this.autorForm = this.fb.group({
      id: [''],
      nome: [''],
      email: [''],
    });
  }

  ngOnInit() {
    this.loadAutores();
  }

  loadAutores() {
    this.authorService.getAll().subscribe(data => {
      this.autores = data.filter(author =>
        author.nome.toLowerCase().includes(this.filterNome.toLowerCase())
      );
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

  saveAutor() {
    if (this.isEditing) {
      this.authorService.update(this.autorForm.value).subscribe(() => {
        this.loadAutores();
      });
    } else {
      this.authorService.create(this.autorForm.value).subscribe(() => {
        this.loadAutores();
      });
    }
  }

  confirmDelete(autor: Autor) {
    const confirmDelete = confirm('Tem certeza que deseja excluir este autor?');
    if (confirmDelete) {
      this.authorService.delete(autor.id).subscribe(() => {
        this.loadAutores();
      });
    }
  }
}
