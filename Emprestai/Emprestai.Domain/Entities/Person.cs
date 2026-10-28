using Emprestai.Domain.Common;

namespace Emprestai.Domain.Entities;

public class Person : BaseEntity
{
    public string Name { get; private set; }
    public string? Email { get; private set; }
    public string? Phone { get; private set; }

    public Person(string name, string? email, string? phone)
    {
        ValidateProperties(name, email, phone);

        Name = name.Trim();
        Email = string.IsNullOrWhiteSpace(email) ? null : email.Trim();
        Phone = string.IsNullOrWhiteSpace(phone) ? null : phone.Trim();
    }

    private void ValidateProperties(string name, string? email, string? phone)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Nome é obrigatório");

        if (name.Trim().Length > 100)
            throw new ArgumentException("Nome deve ter no máximo 100 caracteres.");

        if (string.IsNullOrWhiteSpace(email) && string.IsNullOrWhiteSpace(phone))
            throw new ArgumentException("É obrigatório pelo menos uma informação para contato");
    }
}
