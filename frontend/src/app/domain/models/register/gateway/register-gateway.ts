import { Observable } from 'rxjs';
import { RegisterInput } from '../register-input';
import { RegisterOutput } from '../register-output';

export abstract class RegisterGateway {
    abstract register(_data: RegisterInput): Observable<RegisterOutput>;
}
