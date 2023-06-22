using System;
using System.Collections.Generic;

namespace ApiPersonasDoc.Models;

public partial class TipoDocumento
{
    public long Id { get; set; }

    public string Nombre { get; set; } = null!;

    public virtual ICollection<Persona> Personas { get; set; } = new List<Persona>();
}
