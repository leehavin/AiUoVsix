using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using AiUoVsix.Command.EntityFrameworkCore.Models;

namespace AiUoVsix.Command.EntityFrameworkCore.Services
{
    public class CodeGeneratorService
    {
        public void GenerateCode(List<TableInfo> selectedTables, string outputPath, string namespaceName, 
            string dbContextName, bool generateDbContext, bool generateEntities, 
            bool useDataAnnotations, bool usePluralizer)
        {
            if (!Directory.Exists(outputPath))
            {
                Directory.CreateDirectory(outputPath);
            }
            
            if (generateEntities)
            {
                GenerateEntityClasses(selectedTables, outputPath, namespaceName, useDataAnnotations);
            }
            
            if (generateDbContext)
            {
                GenerateDbContext(selectedTables, outputPath, namespaceName, dbContextName, usePluralizer);
            }
        }
        
        private void GenerateEntityClasses(List<TableInfo> tables, string outputPath, string namespaceName, bool useDataAnnotations)
        {
            foreach (var table in tables)
            {
                var className = ToPascalCase(table.TableName);
                var fileName = $"{className}.cs";
                var filePath = Path.Combine(outputPath, fileName);
                
                var code = GenerateEntityClass(table, namespaceName, useDataAnnotations);
                File.WriteAllText(filePath, code, Encoding.UTF8);
            }
        }
        
        private string GenerateEntityClass(TableInfo table, string namespaceName, bool useDataAnnotations)
        {
            var className = ToPascalCase(table.TableName);
            var sb = new StringBuilder();
            
            sb.AppendLine("using System;");
            sb.AppendLine("using System.Collections.Generic;");
            if (useDataAnnotations)
            {
                sb.AppendLine("using System.ComponentModel.DataAnnotations;");
                sb.AppendLine("using System.ComponentModel.DataAnnotations.Schema;");
            }
            sb.AppendLine();
            sb.AppendLine($"namespace {namespaceName}");
            sb.AppendLine("{");
            
            if (useDataAnnotations)
            {
                sb.AppendLine($"    [Table(\"{table.TableName}\")]");
            }
            
            if (!string.IsNullOrEmpty(table.Comment))
            {
                sb.AppendLine($"    /// <summary>");
                sb.AppendLine($"    /// {table.Comment}");
                sb.AppendLine($"    /// </summary>");
            }
            
            sb.AppendLine($"    public partial class {className}");
            sb.AppendLine("    {");
            
            // 这里应该添加属性生成逻辑，但为了简化，我们添加一个示例属性
            sb.AppendLine("        public int Id { get; set; }");
            sb.AppendLine();
            sb.AppendLine("        // TODO: 添加其他属性");
            
            sb.AppendLine("    }");
            sb.AppendLine("}");
            
            return sb.ToString();
        }
        
        private void GenerateDbContext(List<TableInfo> tables, string outputPath, string namespaceName, 
            string dbContextName, bool usePluralizer)
        {
            var fileName = $"{dbContextName}.cs";
            var filePath = Path.Combine(outputPath, fileName);
            
            var code = GenerateDbContextClass(tables, namespaceName, dbContextName, usePluralizer);
            File.WriteAllText(filePath, code, Encoding.UTF8);
        }
        
        private string GenerateDbContextClass(List<TableInfo> tables, string namespaceName, 
            string dbContextName, bool usePluralizer)
        {
            var sb = new StringBuilder();
            
            sb.AppendLine("using Microsoft.EntityFrameworkCore;");
            sb.AppendLine("using System;");
            sb.AppendLine();
            sb.AppendLine($"namespace {namespaceName}");
            sb.AppendLine("{");
            sb.AppendLine($"    public partial class {dbContextName} : DbContext");
            sb.AppendLine("    {");
            sb.AppendLine($"        public {dbContextName}()");
            sb.AppendLine("        {");
            sb.AppendLine("        }");
            sb.AppendLine();
            sb.AppendLine($"        public {dbContextName}(DbContextOptions<{dbContextName}> options)");
            sb.AppendLine("            : base(options)");
            sb.AppendLine("        {");
            sb.AppendLine("        }");
            sb.AppendLine();
            
            // 生成 DbSet 属性
            foreach (var table in tables)
            {
                var className = ToPascalCase(table.TableName);
                var propertyName = usePluralizer ? Pluralize(className) : className;
                sb.AppendLine($"        public virtual DbSet<{className}> {propertyName} {{ get; set; }}");
            }
            
            sb.AppendLine();
            sb.AppendLine("        protected override void OnModelCreating(ModelBuilder modelBuilder)");
            sb.AppendLine("        {");
            sb.AppendLine("            // 配置实体映射");
            sb.AppendLine("            base.OnModelCreating(modelBuilder);");
            sb.AppendLine("        }");
            sb.AppendLine("    }");
            sb.AppendLine("}");
            
            return sb.ToString();
        }
        
        private string ToPascalCase(string input)
        {
            if (string.IsNullOrEmpty(input))
                return input;
                
            var words = input.Split(new[] { '_', '-', ' ' }, StringSplitOptions.RemoveEmptyEntries);
            var result = string.Join("", words.Select(word => 
                char.ToUpper(word[0]) + word.Substring(1).ToLower()));
                
            return result;
        }
        
        private string Pluralize(string word)
        {
            // 简单的复数形式转换，实际项目中可以使用更复杂的库
            if (word.EndsWith("y"))
                return word.Substring(0, word.Length - 1) + "ies";
            if (word.EndsWith("s") || word.EndsWith("sh") || word.EndsWith("ch") || word.EndsWith("x") || word.EndsWith("z"))
                return word + "es";
            return word + "s";
        }
    }
}