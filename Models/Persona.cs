using System;
using System.Collections.Generic;

namespace ApiPersonasDoc.Models;

public partial class Persona
{
    public long Id { get; set; }

    public string Nombre { get; set; } = null!;

    public string? Apellido { get; set; }

    public long TipoDocumentoId { get; set; }

    public virtual TipoDocumento TipoDocumento { get; set; } = null!;
}
