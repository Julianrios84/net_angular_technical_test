import { Observable } from 'rxjs';
import { LoginInput } from '../login-input';
import { LoginOutput } from '../login-output';

export abstract class LoginGateway {
    abstract login(_data: LoginInput): Observable<LoginOutput>;
}
