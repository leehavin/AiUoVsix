using SqlSugar;
 
namespace AiUoVsix.Command.SqlSugarGen.Common
{
  public class ConnectionElement
  {
    public string Name { get; set; }

    public DbType DatabaseType { get; set; }

    public string ConnectionString { get; set; }

    public string OutputPath { get; set; }

    public string Namespace { get; set; }

    public bool UseSubPath { get; set; }

    public bool UseSugarConfigId { get; set; }

    public PartialMode Partial { get; set; } = PartialMode.None;
  }
}
