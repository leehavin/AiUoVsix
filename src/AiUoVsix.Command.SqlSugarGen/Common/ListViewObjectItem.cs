namespace AiUoVsix.Command.SqlSugarGen.Common
{
  internal class ListViewObjectItem
  {
    public string Name { get; set; }

    public DbObjectType Type { get; set; }

    public bool Check { get; set; }

    public ListViewObjectItem(string name, DbObjectType type, bool check)
    {
      this.Name = name;
      this.Type = type;
      this.Check = check;
    }
  }
}
