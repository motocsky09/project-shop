import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ShoppingCartService } from 'src/services/shopping-cart.service';

@Component({
  selector: 'app-confirm-order',
  templateUrl: './confirm-order.component.html',
  styleUrls: ['./confirm-order.component.css']
})
export class ConfirmOrderComponent implements OnInit {

  constructor(
    private shoppingCartService: ShoppingCartService, // Injectează serviciul pentru coșul de cumpărături
    private router: Router // Injectează Router-ul pentru redirecționare
  ) {}

  ngOnInit(): void {
    this.clearShoppingCart();

    // Redirecționare către pagina Home după 2 minute (120000 milisecunde)
    setTimeout(() => {
      this.router.navigate(['/home']);
    }, 5000); // 120000 ms = 2 minute
  }

  clearShoppingCart() {
    // Apelează metoda care șterge toate produsele din coș
    this.shoppingCartService.clearCart().subscribe(
      () => {
        console.log('Produsele din coș au fost șterse.');
      },
      error => {
        console.error('Eroare la ștergerea produselor din coș:', error);
      }
    );
  }
}
