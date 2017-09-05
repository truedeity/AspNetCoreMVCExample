﻿/*using System.Collections.Generic;
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
	public class MyDbContextWritier : DbContextWriter
	{
		private const string EntityLambdaIdentifier = "entity";

		private ScaffoldingUtilities ScaffoldingUtilities { get; }
		private IndentedStringBuilder _sb;
		private ModelConfiguration _model;
		private bool _foundFirstFluentApiForEntity;

		public MyDbContextWritier(ScaffoldingUtilities scaffoldingUtilities, CSharpUtilities cSharpUtilities) : base(scaffoldingUtilities, cSharpUtilities)
		{

			ScaffoldingUtilities = scaffoldingUtilities;
		}

		public override string WriteCode(ModelConfiguration modelConfiguration)
		{

			_model = modelConfiguration;
			_sb = new IndentedStringBuilder();

			_sb.AppendLine("using Microsoft.EntityFrameworkCore;");
			_sb.AppendLine("using Microsoft.EntityFrameworkCore.Metadata;");
			_sb.AppendLine();
			_sb.AppendLine("namespace " + _model.Namespace());
			_sb.AppendLine("{");
			using (_sb.Indent())
			{
				AddClass();
			}
			_sb.Append("}");

			return _sb.ToString();
		}

		public override void AddClass()
		{
			var className =
					string.IsNullOrWhiteSpace(_model.CustomConfiguration.ContextClassName)
							? _model.ClassName()
							: _model.CustomConfiguration.ContextClassName;
			_sb.AppendLine("public partial class " + className + " : DbContext");
			_sb.AppendLine("{");
			using (_sb.Indent())
			{
				AddOnConfiguring();
				AddOnModelCreating();
				AddDbSetProperties();
				AddEntityTypeErrors();
			}
			_sb.AppendLine("}");
		}

		public override void AddOnConfiguring()
		{
			_sb.AppendLine("protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)");
			_sb.AppendLine("{");

			using (_sb.Indent())
			{
				//_sb.AppendLine("#warning " + CommandsStrings.SensitiveInformationWarning);

				foreach (var optionsBuilderConfig in _model.OnConfiguringConfigurations)
				{
					if (optionsBuilderConfig.FluentApiLines.Count == 0)
					{
						continue;
					}

					_sb.Append("optionsBuilder." + optionsBuilderConfig.FluentApiLines.First());
					using (_sb.Indent())
					{
						foreach (var line in optionsBuilderConfig.FluentApiLines.Skip(1))
						{
							_sb.AppendLine();
							_sb.Append(line);
						}
					}
					_sb.AppendLine(";");
				}
			}
			_sb.AppendLine("}");
		}

		public override void AddOnModelCreating()
		{
			_sb.AppendLine();
			_sb.AppendLine("protected override void OnModelCreating(ModelBuilder modelBuilder)");
			_sb.AppendLine("{");

			using (_sb.Indent())
			{
				var first = true;
				foreach (var entityConfig in _model.EntityConfigurations)
				{
					var fluentApiConfigurations = entityConfig.GetFluentApiConfigurations(_model.CustomConfiguration.UseFluentApiOnly);
					var propertyConfigurations = entityConfig.GetPropertyConfigurations(_model.CustomConfiguration.UseFluentApiOnly);
					var relationshipConfigurations = entityConfig.GetRelationshipConfigurations(_model.CustomConfiguration.UseFluentApiOnly);
					if (fluentApiConfigurations.Count == 0
							&& propertyConfigurations.Count == 0
							&& relationshipConfigurations.Count == 0)
					{
						continue;
					}

					if (!first)
					{
						_sb.AppendLine();
					}
					first = false;

					_sb.AppendLine("modelBuilder.Entity<"
							+ entityConfig.EntityType.Name + ">("
							+ EntityLambdaIdentifier + " =>");
					_sb.AppendLine("{");
					using (_sb.Indent())
					{
						_foundFirstFluentApiForEntity = false;
						AddEntityFluentApi(fluentApiConfigurations);
						AddPropertyConfigurations(propertyConfigurations);
						AddRelationshipConfigurations(relationshipConfigurations);
					}
					_sb.AppendLine("});");
				}

				foreach (var sequenceConfig in _model.SequenceConfigurations)
				{
					if (!first)
					{
						_sb.AppendLine();
					}
					first = false;

					_sb.Append("modelBuilder.HasSequence")
							.Append(!string.IsNullOrEmpty(sequenceConfig.TypeIdentifier) ? "<" + sequenceConfig.TypeIdentifier + ">" : "")
							.Append("(" + sequenceConfig.NameIdentifier)
							.Append(!string.IsNullOrEmpty(sequenceConfig.SchemaNameIdentifier) ? ", " + sequenceConfig.SchemaNameIdentifier : "")
							.Append(")");

					AddFluentConfigurations(sequenceConfig.FluentApiConfigurations);
				}
			}

			_sb.AppendLine("}");
		}

		public override void AddEntityFluentApi(
				List<IFluentApiConfiguration> fluentApiConfigurations)
		{

			foreach (var entityFluentApi in fluentApiConfigurations)
			{
				if (entityFluentApi.FluentApiLines.Count == 0)
				{
					continue;
				}

				if (_foundFirstFluentApiForEntity)
				{
					_sb.AppendLine();
				}
				_foundFirstFluentApiForEntity = true;

				_sb.Append(EntityLambdaIdentifier + "." + entityFluentApi.FluentApiLines.First());
				if (entityFluentApi.FluentApiLines.Count > 1)
				{
					using (_sb.Indent())
					{
						foreach (var line in entityFluentApi.FluentApiLines.Skip(1))
						{
							_sb.AppendLine();
							_sb.Append(line);
						}
					}
				}
				_sb.AppendLine(";");
			}
		}

		public override void AddPropertyConfigurations(List<PropertyConfiguration> propertyConfigurations)
		{

			foreach (var propertyConfig in propertyConfigurations)
			{
				var fluentApiConfigurations =
						propertyConfig.GetFluentApiConfigurations(_model.CustomConfiguration.UseFluentApiOnly);
				if (fluentApiConfigurations.Count == 0)
				{
					continue;
				}

				if (_foundFirstFluentApiForEntity)
				{
					_sb.AppendLine();
				}
				_foundFirstFluentApiForEntity = true;

				_sb.Append(EntityLambdaIdentifier
						+ ".Property(e => e." + propertyConfig.Property.Name + ")");

				AddFluentConfigurations(fluentApiConfigurations);
			}
		}

		private void AddFluentConfigurations(List<FluentApiConfiguration> fluentApiConfigurations)
		{
			if (fluentApiConfigurations.Count > 1)
			{
				_sb.AppendLine();
				_sb.IncrementIndent();
			}

			var first = true;
			foreach (var fluentApiConfiguration in fluentApiConfigurations)
			{
				if (!first)
				{
					_sb.AppendLine();
				}
				first = false;

				foreach (var line in fluentApiConfiguration.FluentApiLines)
				{
					_sb.Append("." + line);
				}
			}

			_sb.AppendLine(";");
			if (fluentApiConfigurations.Count > 1)
			{
				_sb.DecrementIndent();
			}
		}

		public override void AddRelationshipConfigurations(
			 List<RelationshipConfiguration> relationshipConfigurations)
		{

			foreach (var relationshipConfig in relationshipConfigurations)
			{
				if (_foundFirstFluentApiForEntity)
				{
					_sb.AppendLine();
				}
				_foundFirstFluentApiForEntity = true;
				ScaffoldingUtilities.LayoutRelationshipConfigurationLines(
						_sb, EntityLambdaIdentifier, relationshipConfig, "d", "p");
			}
		}

		public override void AddDbSetProperties()
		{
			var first = true;
			foreach (var entityConfig in _model.EntityConfigurations)
			{
				if (first)
				{
					_sb.AppendLine();
					first = false;
				}

				_sb.AppendLine("public virtual DbSet<"
						+ entityConfig.EntityType.Name
						+ "> " + entityConfig.EntityType.Name
						+ " { get; set; }");
			}
		}

		public override void AddEntityTypeErrors()
		{
			if (_model.Model.Scaffolding().EntityTypeErrors.Count == 0)
			{
				return;
			}

			_sb.AppendLine();
			foreach (var entityConfig in _model.Model.Scaffolding().EntityTypeErrors)
			{
				_sb.Append("// ")
						.Append(entityConfig.Value)
						.AppendLine(" Please see the warning messages.");
			}
		}
	}
}
*/