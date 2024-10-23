import { Component, OnInit } from '@angular/core';
import { LivroService } from 'src/services/livro.service';
import { ToastrService } from 'ngx-toastr';
import { Autor } from 'src/models/autor.model';
import { AutorService } from 'src/services/autor.service';
import { RelatorioFilter } from 'src/models/relatorio-filter.model';

@Component({
  selector: 'app-relatorio',
  templateUrl: './relatorio.component.html',
  styleUrls: ['./relatorio.component.css']
})
export class RelatorioComponent implements OnInit {
  isLoading: boolean = false;
  filterTitulo: string = '';
  filterAutor: number = 0;
  autores: Autor[] = [];

  constructor(
    private toastrService: ToastrService,
    private livroService: LivroService,
    private autorService: AutorService
  ) { }

  ngOnInit(): void {
    this.loadAutores();
  }

  loadAutores() {
    this.autorService.getAll().subscribe((data) => {
      if (data) {
        this.autores = data;
      }
    })
  }

  abrirRelatorio() {
    this.isLoading = true;

    const filter: RelatorioFilter = {
      titulo: this.filterTitulo,
      codAutor: Number(this.filterAutor)
    }

    this.livroService.gerarRelatorio(filter).subscribe((response: Blob) => {
      this.isLoading = false;
      const fileURL = URL.createObjectURL(response);
      window.open(fileURL, '_blank'); // Abre o PDF em nova aba
    }, error => {
      this.isLoading = false;
      this.toastrService.error('Erro ao gerar o relatório!');
    });
  }
}
