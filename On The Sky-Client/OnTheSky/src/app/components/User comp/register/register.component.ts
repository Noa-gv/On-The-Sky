import { Component } from '@angular/core';
import { ReactiveFormsModule, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router, RouterModule } from '@angular/router';
import { NgIf } from '@angular/common';
import { AuthService } from '../../../Services/auth.service';

@Component({
  selector: 'app-register',
  standalone: true,
  imports: [RouterModule, NgIf, ReactiveFormsModule],
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent {
  registerForm: FormGroup;
  message = '';

  constructor(
    private fb: FormBuilder,
    private authService: AuthService,
    private router: Router
  ) {
    this.registerForm = this.fb.group({
      userName: ['', Validators.required],
      password: ['', [Validators.required, Validators.minLength(6)]],
    });
  }

  onSubmit() {
    if (this.registerForm.invalid) return;

    const user = this.registerForm.value;

    this.authService.register(user).subscribe({
      next: () => {
        this.authService.login(user).subscribe({
          next: (res) => {
            this.authService.saveToken(res.token);

            this.authService.getCurrentUser().subscribe({
              next: (user: any) => {
                this.authService.setCurrentUser(user);

                if (user.role === 'manager') {
                  this.router.navigate(['/admin']);
                } else if (user.role === 'passanger' && user.id) {
                  this.router.navigate(['/my-travels', user.id]);
                } else {
                  this.message = 'Unknown role or missing user ID.';
                }
              },
              error: () => {
                this.message = 'שגיאה באימות המשתמש';
              }
            });
          },
          error: () => {
            this.message = 'התחברות אוטומטית נכשלה לאחר ההרשמה';
          }
        });
      },
      error: (err) => {
        this.message = typeof err.error === 'string'
          ? err.error
          : (err.error?.message || 'Registration failed.');
      }
    });
  }
}
