import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import * as bootstrap from 'bootstrap';
import { Assunto } from 'src/models/assunto.model';
import { Autor } from 'src/models/autor.model';
import { Livro } from 'src/models/livro.model';
import { AssuntoService } from 'src/services/assunto.service';
import { AutorService } from 'src/services/autor.service';
import { LivroService } from 'src/services/livro.service';

@Component({
  selector: 'app-livro',
  templateUrl: './livro.component.html',
  styleUrls: ['./livro.component.css']
})
export class LivroComponent implements OnInit {
  isLoading = false;
  livros: Livro[] = [];
  filterTitulo: string = '';
  filterAutor: number = 0;
  livroForm: FormGroup;
  modalTitle: string = '';
  autores: Autor[] = [];
  assuntos: Assunto[] = [];
  isEditing: boolean = false;

  constructor(
    private fb: FormBuilder,
    private autorService: AutorService,
    private assuntoService: AssuntoService,
    private livroService: LivroService
  ) {
    this.livroForm = this.fb.group({
      codLivro: [0],
      titulo: ['', Validators.required],
      editora: ['', Validators.required],
      anoPublicacao: ['', Validators.required],
      edicao: ['', Validators.required],
      autoresIds: [[], Validators.required],
      assuntosIds: [[], Validators.required]
    });
  }

  ngOnInit(): void {
    this.loadAssuntos();
    this.loadAutores();
    this.loadLivros();
  }

  loadAutores() {
    this.autorService.getAll().subscribe((data) => {
      if (data) {
        this.autores = data;
      }
    })
  }

  loadAssuntos() {
    this.assuntoService.getAll().subscribe((data) => {
      if (data) {
        this.assuntos = data;
      }
    })
  }

  loadLivros() {
    this.isLoading = true;

    this.livroService.getAll().subscribe((data) => {
      this.livros = data.filter(livro => {
        const matchTitulo = !this.filterTitulo || livro.titulo.toLowerCase().includes(this.filterTitulo.toLowerCase());
        const matchAutor = !this.filterAutor || livro.autoresIds.includes(Number(this.filterAutor));
        //const matchAssunto = !this.filterAssunto || livro.assuntosIds.includes(this.filterAssunto);

        return matchTitulo && matchAutor;
      });

      this.isLoading = false;
    })
  }

  openModal(isEditMode: boolean = false, livro?: Livro) {
    const modalElement = document.getElementById('modalLivro');
    if (modalElement) {
      const modal = new bootstrap.Modal(modalElement);
      this.isEditing = isEditMode;
      this.modalTitle = isEditMode ? 'Editar Livro' : 'Novo Livro';

      if (isEditMode && livro) {
        this.livroForm.patchValue({
          codLivro: livro.codLivro,
          titulo: livro.titulo,
          editora: livro.editora,
          anoPublicacao: livro.anoPublicacao,
          edicao: livro.edicao,
          autoresIds: livro.autoresIds,
          assuntosIds: livro.assuntosIds
        });
      } else {
        this.livroForm.reset();
      }

      modal.show();
    } else {
      console.error('Modal element not found');
    }
  }

  closeModal() {
    const modalElement = document.getElementById('modalLivro');

    if (modalElement) {
      const modal = bootstrap.Modal.getInstance(modalElement); // Obter a instância da modal
      if (modal) {
        modal.hide();
      } else {
        console.error('Modal instance not found');
      }
    } else {
      console.error('Modal element not found');
    }
  }

  saveLivro() {
    this.isLoading = true;

    if (this.livroForm.valid) {
      if (this.isEditing) {
        this.livroService.update(this.livroForm.value).subscribe((data) => {
          this.loadLivros();
        })
      } else {
        this.livroService.create(this.livroForm.value).subscribe((data) => {
          this.loadLivros();
        })
      }
      this.closeModal();
    }
  }

  confirmDelete(livro: any) {
    const confirmDelete = confirm('Tem certeza que deseja excluir este livro?');
    if (confirmDelete) {
      // this.isLoading = true;

      // this.assuntoService.delete(assunto.codAssunto).subscribe(() => {
      //   this.loadAssuntos();
      // });
    }
  }
}
