using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Scaffolding.Internal;
using System;
using System.Text.RegularExpressions;

namespace EFCoreDBFirst.MyStore.DAL
{
    public class MyStoreEntityTypeGenerator : CSharpEntityTypeGenerator
    {
        public MyStoreEntityTypeGenerator(ICSharpHelper cSharpHelper) : base(cSharpHelper)
        {
        }

        public override string WriteCode(IEntityType entityType, string @namespace, bool useDataAnnotations)
        {
            var tabSpace = "    "; // 4 spaces tab
            var nl = Environment.NewLine;
            var code = base.WriteCode(entityType, @namespace, useDataAnnotations);

            var createdDatePattern = @"DateTime CreatedDate";
            var createdDateRegex = new Regex(createdDatePattern, RegexOptions.Compiled);
            if (createdDateRegex.IsMatch(code))
            {
                // Backing field insertion
                var entityConstructorPattern = $@"(public {entityType.Name}\(\))";
                var entityConstructorRegex = new Regex(entityConstructorPattern, RegexOptions.Compiled);
                var backingFieldCode = "private DateTime _createdDate = DateTime.Now;";
                code = entityConstructorRegex.Replace(code, $"{backingFieldCode}{nl}{nl}{tabSpace}{tabSpace}$1");

                // CreatedDate property modification
                var createdDatePropertyPattern = @"(public DateTime CreatedDate \{ get; set; \})";
                var createdDatePropertyRegex = new Regex(createdDatePropertyPattern, RegexOptions.Compiled);
                var createdDateCode = $"public DateTime CreatedDate{nl}"
                                    + $"{tabSpace}{tabSpace}{{{nl}"
                                    + $"{tabSpace}{tabSpace}{tabSpace}get{nl}"
                                    + $"{tabSpace}{tabSpace}{tabSpace}{{{nl}"
                                    + $"{tabSpace}{tabSpace}{tabSpace}{tabSpace}return _createdDate;{nl}"
                                    + $"{tabSpace}{tabSpace}{tabSpace}}}{nl}"
                                    + $"{tabSpace}{tabSpace}}}";
                code = createdDatePropertyRegex.Replace(code, createdDateCode);
            }

            return code;
        }
    }
}
