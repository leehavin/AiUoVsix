using System.Collections.Generic;

namespace AiUoVsix.Command.EntityFrameworkCore.Models
{
    public enum DatabaseObjectType
    {
        Table,
        View,
        Function,
        StoredProcedure
    }

    public class DatabaseObject
    {
        public string Name { get; set; } = string.Empty;
        public DatabaseObjectType Type { get; set; }
        public List<TableColumn> Columns { get; set; } = new();
        public bool IsExpanded { get; set; }
    }

    public class TableColumn
    {
        public string Name { get; set; } = string.Empty;
        public string DataType { get; set; } = string.Empty;
        public bool IsNullable { get; set; }
        public string? DefaultValue { get; set; }
        public int? MaxLength { get; set; }
        public int? Precision { get; set; }
        public int? Scale { get; set; }
        public string Extra { get; set; } = string.Empty;
        public bool IsPrimaryKey { get; set; }
        public string? Comment { get; set; }
        
        public string DisplayType
        {
            get
            {
                var type = DataType;
                if (MaxLength.HasValue && MaxLength > 0)
                {
                    type += $"({MaxLength})";
                }
                else if (Precision.HasValue && Scale.HasValue)
                {
                    type += $"({Precision},{Scale})";
                }
                else if (Precision.HasValue)
                {
                    type += $"({Precision})";
                }
                return type;
            }
        }
        
        public string NullableDisplay => IsNullable ? "NULL" : "NOT NULL";
    }
}