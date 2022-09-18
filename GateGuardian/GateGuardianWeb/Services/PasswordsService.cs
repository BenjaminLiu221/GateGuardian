using GateGuardianWeb.Models;
using System;

public interface IPasswordsService
{
    public Task<List<Password>> GetAllPasswords();
}

public class PasswordsService: IPasswordsService
{
    public Task<List<Password>> GetAllPasswords()
    {
        throw new NotImplementedException();
    }
}