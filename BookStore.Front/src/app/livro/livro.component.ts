import { CurrencyPipe } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { FormArray, FormBuilder, FormGroup, Validators } from '@angular/forms';
import * as bootstrap from 'bootstrap';
import { Assunto } from 'src/models/assunto.model';
import { Autor } from 'src/models/autor.model';
import { Livro } from 'src/models/livro.model';
import { PrecoLivro } from 'src/models/preco-livro.model';
import { AssuntoService } from 'src/services/assunto.service';
import { AutorService } from 'src/services/autor.service';
import { LivroService } from 'src/services/livro.service';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-livro',
  templateUrl: './livro.component.html',
  styleUrls: ['./livro.component.css'],
  providers: [CurrencyPipe]
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
  precoValue: string = "";

  constructor(
    private fb: FormBuilder,
    private toastr: ToastrService,
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
      precos: this.fb.array([]),
      autoresIds: [[], Validators.required],
      assuntosIds: [[], Validators.required]
    });
  }

  ngOnInit(): void {
    this.loadAssuntos();
    this.loadAutores();
    this.loadLivros();
  }

  get precos(): FormArray {
    return this.livroForm.get('precos') as FormArray;
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
          assuntosIds: livro.assuntosIds,
          precos: livro.precos
        });
        const precosArray = this.livroForm.get('precos') as FormArray;
        livro.precos.forEach(preco => {
          precosArray.push(this.fb.group({
            tipoCompra: preco.tipoCompra,
            valor: preco.valor
          }));
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

        this.livroForm.reset();
        this.livroForm.setControl('precos', this.fb.array([]));
      } else {
        console.error('Modal instance not found');
      }
    } else {
      console.error('Modal element not found');
    }
  }

  saveLivro() {
    Object.keys(this.livroForm.controls).forEach(control => {
      this.livroForm.get(control)?.markAsTouched();
    });

    if (this.livroForm.valid) {
      this.isLoading = true;

      if (this.livroForm.valid) {
        const livroData = this.livroForm.value;

        livroData.precos.forEach((preco: any) => {
          preco.codLivro = this.livroForm.value.codLivro;
          preco.tipoCompra = Number(preco.tipoCompra);
          preco.valor = parseFloat(preco.valor.replace(',', '.'));
        });

        if (this.isEditing) {
          this.livroService.update(this.livroForm.value).subscribe((data) => {
            this.toastr.success('Livro alterado com sucesso!');
            this.loadLivros();
          })
        } else {
          this.livroService.create(this.livroForm.value).subscribe((data) => {
            this.toastr.success('Livro cadastrado com sucesso!');
            this.loadLivros();
          })
        }
        this.closeModal();
      }
    }
  }

  confirmDelete(livro: Livro) {
    const confirmDelete = confirm('Tem certeza que deseja excluir este livro?');
    if (confirmDelete) {
      this.isLoading = true;

      this.livroService.delete(livro.codLivro).subscribe(() => {
        this.toastr.success('Livro excluído com sucesso!');
        this.loadLivros();
      }, (error) => {
        this.isLoading = false;
        this.toastr.error(error);
      })
    }
  }

  adicionarPreco() {
    const tipoCompraForm = this.fb.group({
      codLivro: [''],
      valor: [''],
      tipoCompra: ['']
    });

    this.precos.push(tipoCompraForm); // Adiciona o FormGroup ao FormArray
  }

  removerPreco(index: number) {
    const precosArray = this.livroForm.get('precos') as FormArray;
    precosArray.removeAt(index);
  }

  onPrecoChange(value: string) {
    // Aqui você pode formatar o valor, se necessário
    this.precoValue = value.replace('R$', '').trim(); // Se precisar remover a string "R$"
  }
}
