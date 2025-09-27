import { Component, OnInit } from '@angular/core';
import { ProductService } from '../../../services/product.service';
import { Product } from '../../models/product.model';
import { CommonModule } from '@angular/common';
import { AuthService } from '../../../services/auth.service';
import { Router } from '@angular/router';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-dashboard',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.css']
})
export class DashboardComponent implements OnInit {
  products: Product[] = [];
  filteredProducts: Product[] = [];
  searchTerm: string = '';
  selectedCategory: string = '';
  showModal: boolean = false;
  isEditMode: boolean = false;
  currentProduct: Partial<Product> = {};

  constructor(
    private productService: ProductService,
    private authService: AuthService,
    private router: Router
  ) { }

  ngOnInit(): void {
    this.loadProducts();
  }

  loadProducts(): void {
    this.productService.getProducts().subscribe(products => {
      this.products = products;
      this.filteredProducts = products;
    });
  }

  logout(): void {
    this.authService.logout();
    this.router.navigate(['/login']);
  }

  filterProducts(): void {
    let tempProducts = this.products;

    if (this.searchTerm) {
      tempProducts = tempProducts.filter(p => 
        p.name.toLowerCase().includes(this.searchTerm.toLowerCase())
      );
    }

    if (this.selectedCategory) {
      tempProducts = tempProducts.filter(p => p.category === this.selectedCategory);
    }

    this.filteredProducts = tempProducts;
  }

  getTotalProducts(): number {
    return this.products.length;
  }

  getTotalStock(): number {
    return this.products.reduce((acc, p) => acc + p.stock, 0);
  }

  getTotalValue(): number {
    return this.products.reduce((acc, p) => acc + (p.price * p.stock), 0);
  }

  getLowStockCount(threshold: number = 10): number {
    return this.products.filter(p => p.stock < threshold).length;
  }

  getStockBadgeClass(stock: number): string {
    if (stock === 0) return 'bg-danger';
    if (stock < 10) return 'bg-warning';
    return 'bg-success';
  }

  openAddModal(): void {
    this.isEditMode = false;
    this.currentProduct = { name: '', description: '', category: '', price: 0, stock: 0, imageUrl: '' };
    this.showModal = true;
  }

  openEditModal(product: Product): void {
    this.isEditMode = true;
    this.currentProduct = { ...product };
    this.showModal = true;
  }

  closeModal(): void {
    this.showModal = false;
  }

  saveProduct(): void {
    if (this.isEditMode && this.currentProduct.id) {
      this.productService.updateProduct(this.currentProduct as Product).subscribe(() => {
        this.loadProducts();
        this.closeModal();
      });
    } else {
      this.productService.addProduct(this.currentProduct).subscribe(() => {
        this.loadProducts();
        this.closeModal();
      });
    }
  }

  deleteProduct(id: number): void {
    if (confirm('¿Estás seguro de que quieres eliminar este producto?')) {
      this.productService.deleteProduct(id).subscribe(() => {
        this.loadProducts();
      });
    }
  }
}
