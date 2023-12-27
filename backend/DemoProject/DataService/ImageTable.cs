using System;
using System.Collections.Generic;

namespace DataService;

public partial class ImageTable
{
    public int Id { get; set; }

    public byte[]? Data { get; set; }
}
