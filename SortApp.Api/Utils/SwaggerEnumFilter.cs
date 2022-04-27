using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.ComponentModel;
using System.Reflection;
using System.Text;

namespace SortApp.Api.Utils
{
    public class SwaggerEnumFilter : ISchemaFilter
    {

        public void Apply(OpenApiSchema schema, SchemaFilterContext context)
        {
            if (schema.Enum != null && schema.Enum.Count > 0 &&
                context.Type != null && context.Type.IsEnum)
            {
                var enumDescprtionDict = GetEnumDescritionDict(context.Type);
                if (enumDescprtionDict.Any())
                {
                    AddDescriptionsToSwaggerDoc(schema, enumDescprtionDict);
                }
            }
        }

        private void AddDescriptionsToSwaggerDoc(OpenApiSchema schema, Dictionary<int, string> enumDescprtionDict)
        {
            var sb = new StringBuilder();
            sb.Append("<p>Description:</p><ul>");

            enumDescprtionDict.ToList().ForEach(kv =>
            {
                sb.Append($"<li><i>{kv.Key}</i> - {kv.Value}</li>");
            });

            sb.Append("</ul>");
            schema.Description = sb.ToString();
        }

        private Dictionary<int, string> GetEnumDescritionDict(Type enumType)
        {
            var dict = new Dictionary<int, string>();

            var enumNames = enumType.GetEnumNames();
            foreach (var enumName in enumNames)
            {
                FieldInfo fi = enumType.GetField(enumName);

                var attributes = fi.GetCustomAttributes(typeof(DescriptionAttribute), false)
                    .Cast<DescriptionAttribute>();

                if (attributes.Any())
                {
                    dict.Add((int)Enum.Parse(enumType, enumName), attributes.FirstOrDefault()?.Description);
                }
            }

            return dict;
        }
    }
}
