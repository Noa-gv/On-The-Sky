import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { Router,RouterModule} from '@angular/router';
import { NgIf } from '@angular/common';
import { AuthService, LoginModel } from '../../../Services/auth.service';
import { FlyPlaneDirective} from '../../../Directive & Pipe/fly-plane.directive'

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [FormsModule, NgIf, FlyPlaneDirective,RouterModule],
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css'],
})
export class LoginComponent {
  userName = '';
  password = '';
  error = '';
  isPlaneFlying = false;

  private pendingUserId: number | null = null;

  constructor(private auth: AuthService, private router: Router) {}

  onSubmit() {
    const loginModel: LoginModel = {
      userName: this.userName,
      password: this.password,
    };

    this.auth.login(loginModel).subscribe({
      next: (res) => {
        this.auth.saveToken(res.token);

        this.auth.getCurrentUser().subscribe({
          next: (user: any) => {
            this.auth.setCurrentUser(user);
            console.log('User:', user);

            if (user.role === 'manager') {
              this.router.navigate(['/admin']);
            } else if (user.role === 'passanger' && user.id) {
              this.pendingUserId = user.id;
              this.isPlaneFlying = true; // מתחילים את האנימציה
            } else {
              this.error = 'Unknown role or user ID missing';
            }
          },
          error: () => {
            this.error = 'שגיאה באימות המשתמש';
          },
        });
      },
      error: () => {
        this.error = 'שם משתמש או סיסמה לא נכונים';
      },
    });
  }

  onAnimationDone() {
    if (this.pendingUserId !== null) {
      this.isPlaneFlying = false;
      this.router.navigate(['/my-travels', this.pendingUserId]);
      this.pendingUserId = null;
    }
  }
}
