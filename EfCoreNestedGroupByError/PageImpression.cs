using System;

namespace EfCoreNestedGroupByError;

public class PageImpression
{
    public int Id { get; set; }
    public string IpAddress { get; set; } = null!;
    public string UserAgent { get; set; } = null!;
    public DateTime CreationDateTime { get; set; }
}
