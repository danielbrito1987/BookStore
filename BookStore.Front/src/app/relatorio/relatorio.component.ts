import { Component, OnInit } from '@angular/core';
import { LivroService } from 'src/services/livro.service';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-relatorio',
  templateUrl: './relatorio.component.html',
  styleUrls: ['./relatorio.component.css']
})
export class RelatorioComponent implements OnInit {
  isLoading: boolean = false;

  constructor(
    private toastrService: ToastrService,
    private livroService: LivroService
  ) { }

  ngOnInit(): void {
  }

  abrirRelatorio() {
    this.isLoading = true;

    this.livroService.gerarRelatorio().subscribe((response: Blob) => {
      this.isLoading = false;
      const fileURL = URL.createObjectURL(response);
      window.open(fileURL, '_blank'); // Abre o PDF em nova aba
    }, error => {
      this.isLoading = false;
      this.toastrService.error('Erro ao gerar o relatório!');
    });
  }
}
