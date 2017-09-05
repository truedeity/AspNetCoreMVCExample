/*using System.Collections.Generic;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore.Scaffolding.Configuration.Internal;
using Microsoft.EntityFrameworkCore.Scaffolding.Internal;
using Microsoft.EntityFrameworkCore.Internal;
using System.Linq;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Scaffolding.Configuration;
using Microsoft.EntityFrameworkCore.Utilities;


namespace CoreFamework.DesignTime
{
	public class MyEntityTypeWriter : EntityTypeWriter
	{

		private ScaffoldingUtilities ScaffoldingUtilities { get; }
		private CSharpUtilities CSharpUtilities { get; }
		private IndentedStringBuilder _sb;
		private EntityConfiguration _entity;

		public MyEntityTypeWriter(
				ScaffoldingUtilities scaffoldingUtilities,
				CSharpUtilities cSharpUtilities) : base(cSharpUtilities)
		{
			ScaffoldingUtilities = scaffoldingUtilities;
			CSharpUtilities = cSharpUtilities;
		}

		public override string WriteCode(
				EntityConfiguration entityConfiguration)
		{

			_entity = entityConfiguration;
			_sb = new IndentedStringBuilder();

			_sb.AppendLine("using System;");
			_sb.AppendLine("using System.Collections.Generic;");
			if (!_entity.ModelConfiguration.CustomConfiguration.UseFluentApiOnly)
			{
				_sb.AppendLine("using System.ComponentModel.DataAnnotations;");
				_sb.AppendLine("using System.ComponentModel.DataAnnotations.Schema;");
			}

			foreach (var ns in _entity.EntityType.GetProperties()
					.Select(p => p.ClrType.Namespace)
					.Where(ns => ns != "System" && ns != "System.Collections.Generic")
					.Distinct())
			{
				_sb
						.Append("using ")
						.Append(ns)
						.AppendLine(';');
			}

			_sb.AppendLine();
			_sb.AppendLine("namespace " + _entity.ModelConfiguration.Namespace());
			_sb.AppendLine("{");
			using (_sb.Indent())
			{
				AddClass();
			}
			_sb.AppendLine("}");

			return _sb.ToString();
		}

		public override void AddClass()
		{
			AddAttributes(_entity.AttributeConfigurations);
			if (_entity.EntityType.Name.Contains("AspNet")) {
				_sb.AppendLine("public partial class " + _entity.EntityType.Name);
			} else {
				_sb.AppendLine("public partial class " + _entity.EntityType.Name + " : IEntity");
			}
			
			_sb.AppendLine("{");
			using (_sb.Indent())
			{
				AddConstructor();
				AddProperties();
				AddNavigationProperties();
			}
			_sb.AppendLine("}");
		}

		public override void AddConstructor()
		{
			if (_entity.NavigationPropertyInitializerConfigurations.Count > 0)
			{
				_sb.AppendLine("public " + _entity.EntityType.Name + "()");
				_sb.AppendLine("{");
				using (_sb.Indent())
				{
					foreach (var navPropInitializer in _entity.NavigationPropertyInitializerConfigurations)
					{
						_sb.AppendLine(
								navPropInitializer.NavigationPropertyName
								+ " = new HashSet<"
								+ navPropInitializer.PrincipalEntityTypeName + ">();");
					}
				}
				_sb.AppendLine("}");
				_sb.AppendLine();
			}
		}

		public override void AddProperties()
		{
			foreach (var property in _entity.EntityType.GetProperties().OrderBy(p => p.Scaffolding().ColumnOrdinal))
			{
				PropertyConfiguration propertyConfiguration = _entity.FindPropertyConfiguration(property);
				if (propertyConfiguration != null)
				{
					AddAttributes(propertyConfiguration.AttributeConfigurations);
				}

				_sb.AppendLine("public "
						+ CSharpUtilities.GetTypeName(property.ClrType)
						+ " " + property.Name + " { get; set; }");
			}
		}

		public override void AddNavigationProperties()
		{
			if (_entity.NavigationPropertyConfigurations.Count > 0)
			{
				_sb.AppendLine();
				foreach (var navProp in _entity.NavigationPropertyConfigurations)
				{
					if (navProp.ErrorAnnotation != null)
					{
						_sb.AppendLine("// " + navProp.ErrorAnnotation);
					}
					else
					{
						AddAttributes(navProp.AttributeConfigurations);
						_sb.AppendLine("public virtual " + navProp.Type
								+ " " + navProp.Name + " { get; set; }");
					}
				}
			}
		}

		public override void AddAttributes(
				IEnumerable<IAttributeConfiguration> attributeConfigurations)
		{
			if (!_entity.ModelConfiguration.CustomConfiguration.UseFluentApiOnly)
			{
				foreach (var attrConfig in attributeConfigurations)
				{
					_sb.AppendLine("[" + attrConfig.AttributeBody + "]");
				}
			}
		}
	}

	
}


*/