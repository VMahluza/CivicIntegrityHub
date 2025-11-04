using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities;
public class  Evidence : AuditableEntity
{
    public string FileName { get; private set; } = null!;
    public string MimeType { get; private set; } = null!;
    public long Size { get; private set; }      // in bytes
    public string? Hash { get; private set; }   // optional checksum for integrity

}
