using Emprestai.Domain.Common;

namespace Emprestai.Domain.Entities;

public class Item : BaseEntity
{
    public bool IsUnderMaintenance { get; set; }
}
