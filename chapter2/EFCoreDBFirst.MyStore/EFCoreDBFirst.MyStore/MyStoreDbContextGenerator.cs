using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Scaffolding;
using Microsoft.EntityFrameworkCore.Scaffolding.Internal;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace EFCoreDBFirst.MyStore
{
    public class MyStoreDbContextGenerator : CSharpDbContextGenerator
    {
        public MyStoreDbContextGenerator(
#pragma warning disable CS0618 // Type or member is obsolete
            IEnumerable<IScaffoldingProviderCodeGenerator> legacyProviderCodeGenerators,
#pragma warning restore CS0618 // Type or member is obsolete
            IEnumerable<IProviderConfigurationCodeGenerator> providerCodeGenerators,
            IAnnotationCodeGenerator annotationCodeGenerator,
            ICSharpHelper cSharpHelper
        ) : base(legacyProviderCodeGenerators, providerCodeGenerators, annotationCodeGenerator, cSharpHelper)
        {
        }

        public override string WriteCode(IModel model, string @namespace, string contextName, string connectionString,
            bool useDataAnnotations, bool suppressConnectionStringWarning)
        {
            var tabSpace = "    "; // 4 spaces tab
            var nl = Environment.NewLine;
            var code = base.WriteCode(model, @namespace, contextName, connectionString, useDataAnnotations, suppressConnectionStringWarning);

            // ConnectionString deletion
            var connectionStringPattern = @"(protected override void OnConfiguring\(DbContextOptionsBuilder optionsBuilder\)\r\n\s*\{)((.|\n)*)(\r\n\s*\}(\r\n){2})";
            var connectionStringRegex = new Regex(connectionStringPattern, RegexOptions.Compiled);
            code = connectionStringRegex.Replace(code, "$1$4");

            // ValueConverter insertion
            var valueConverterPattern = @"(protected override void OnModelCreating\(ModelBuilder modelBuilder\)\r\n\s*\{)(\s*modelBuilder\.Entity)";
            var valueConverterRegex = new Regex(valueConverterPattern, RegexOptions.Compiled);
            var valueConverterCode = $"{tabSpace}{tabSpace}{tabSpace}var converter = new Microsoft.EntityFrameworkCore.Storage.ValueConversion.ValueConverter<DateTime, DateTime>({nl}"
                                   + $"{tabSpace}{tabSpace}{tabSpace}{tabSpace}v => DateTime.Now,{nl}"
                                   + $"{tabSpace}{tabSpace}{tabSpace}{tabSpace}v => v{nl}"
                                   + $"{tabSpace}{tabSpace}{tabSpace});{nl}";
            code = valueConverterRegex.Replace(code, $"$1{nl}{valueConverterCode}$2");

            // ValueConverter usage
            var modifiedDatePattern = @"(ModifiedDate\)\.HasColumnType\(""datetime""\))(;\r\n)";
            var modifiedDateRegex = new Regex(modifiedDatePattern, RegexOptions.Compiled);
            code = modifiedDateRegex.Replace(code, $"$1.HasConversion(converter)$2");

            return code;
        }
    }
}
