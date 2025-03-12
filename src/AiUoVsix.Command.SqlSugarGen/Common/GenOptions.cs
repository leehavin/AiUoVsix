using System.Collections.Generic;
 
namespace AiUoVsix.Command.SqlSugarGen.Common
{
  public class GenOptions
  {
    public string DefaultElement { get; set; }

    public List<ConnectionElement> Elements { get; set; } = new List<ConnectionElement>();
  }
}
