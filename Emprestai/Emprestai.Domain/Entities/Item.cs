using Emprestai.Domain.Common;

namespace Emprestai.Domain.Entities;

public class Item : BaseEntity
{
    public string Name { get; private set; }
    public string? Description { get; private set; }
    public bool IsUnderMaintenance { get; private set; }

    public Item(string name, string? description)
    {
        ValidateProperties(name, description);

        Name = name.Trim();
        Description = string.IsNullOrWhiteSpace(description) ? null : description.Trim();
    }

    private static void ValidateProperties(string name, string? description)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Nome é obrigatório");

        if (name.Trim().Length > 100)
            throw new ArgumentException("Nome deve ter no máximo 100 caracteres.");

        if (!string.IsNullOrWhiteSpace(description) && description.Trim().Length > 200)
            throw new ArgumentException("Descrição deve ter no máximo 200 caracteres.");
    }

    public void StartMaintenance()
    {
        if (IsUnderMaintenance)
            throw new InvalidOperationException("O item já está em manutenção.");
        IsUnderMaintenance = true;
    }

    public void FinishMaintenance()
    {
        if (!IsUnderMaintenance)
            throw new InvalidOperationException("O item não se encontra em manutenção.");
        IsUnderMaintenance = false;
    }
}
