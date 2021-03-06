// Copyright (c) Nate McMaster.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using McMaster.Extensions.CommandLineUtils.Conventions;

namespace McMaster.Extensions.CommandLineUtils
{
    /// <summary>
    /// Methods for adding commonly used conventions
    /// </summary>
    public static class ConventionBuilderExtensions
    {
        /// <summary>
        /// Applies a collection of default conventions, such as applying options in attributes
        /// on the model type,
        /// </summary>
        /// <returns>The builder.</returns>
        public static IConventionBuilder UseDefaultConventions(this IConventionBuilder builder)
        {
            if (builder == null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            return builder
                .UseAttributes()
                .SetAppNameFromEntryAssembly()
                .SetRemainingArgsPropertyOnModel()
                .SetSubcommandPropertyOnModel()
                .SetParentPropertyOnModel()
                .UseOnExecuteMethodFromModel()
                .UseOnValidationErrorMethodFromModel()
                .UseConstructorInjection();
        }

        /// <summary>
        /// Applies a collection of default conventions, such as applying options in attributes
        /// on the model type,
        /// </summary>
        /// <returns>The builder.</returns>
        public static IConventionBuilder UseAttributes(this IConventionBuilder builder)
        {
            if (builder == null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            return builder
                .UseCommandAttribute()
                .UseVersionOptionFromMemberAttribute()
                .UseVersionOptionAttribute()
                .UseHelpOptionAttribute()
                .UseOptionAttributes()
                .UseArgumentAttributes()
                .UseSubcommandAttributes();
        }

        /// <summary>
        /// Sets a property named "RemainingArgs" or "RemainingArguments" on the model type to the value
        /// of <see cref="CommandLineApplication.RemainingArguments" />.
        /// </summary>
        /// <param name="builder">The builder.</param>
        /// <returns>The builder.</returns>
        public static IConventionBuilder SetRemainingArgsPropertyOnModel(this IConventionBuilder builder)
            => builder.AddConvention(new RemainingArgsPropertyConvention());

        /// <summary>
        /// Sets a property named "Subcommand" on the model type to the value
        /// of the model of the selected subcommand.
        /// </summary>
        /// <param name="builder">The builder.</param>
        /// <returns>The builder.</returns>
        public static IConventionBuilder SetSubcommandPropertyOnModel(this IConventionBuilder builder)
            => builder.AddConvention(new SubcommandPropertyConvention());

        /// <summary>
        /// Sets a property named "Parent" on the model type to the value
        /// of the model of the parent command.
        /// </summary>
        /// <param name="builder">The builder.</param>
        /// <returns>The builder.</returns>
        public static IConventionBuilder SetParentPropertyOnModel(this IConventionBuilder builder)
            => builder.AddConvention(new ParentPropertyConvention());

        /// <summary>
        /// Sets <see cref="CommandLineApplication.Name" /> to match the name of
        /// <see cref="System.Reflection.Assembly.GetEntryAssembly" />
        /// </summary>
        /// <param name="builder">The builder.</param>
        /// <returns>The builder.</returns>
        public static IConventionBuilder SetAppNameFromEntryAssembly(this IConventionBuilder builder)
            => builder.AddConvention(new AppNameFromEntryAssemblyConvention());

        /// <summary>
        /// Applies settings from <see cref="CommandAttribute" /> on the model type.
        /// </summary>
        /// <param name="builder">The builder.</param>
        /// <returns>The builder.</returns>
        public static IConventionBuilder UseCommandAttribute(this IConventionBuilder builder)
            => builder.AddConvention(new CommandAttributeConvention());

        /// <summary>
        /// Applies settings from <see cref="VersionOptionFromMemberAttribute" /> on the model type.
        /// </summary>
        /// <param name="builder">The builder.</param>
        /// <returns>The builder.</returns>
        public static IConventionBuilder UseVersionOptionFromMemberAttribute(this IConventionBuilder builder)
            => builder.AddConvention(new VersionOptionFromMemberAttributeConvention());

        /// <summary>
        /// Applies settings from <see cref="VersionOptionAttribute" /> on the model type.
        /// </summary>
        /// <param name="builder">The builder.</param>
        /// <returns>The builder.</returns>
        public static IConventionBuilder UseVersionOptionAttribute(this IConventionBuilder builder)
            => builder.AddConvention(new VersionOptionAttributeConvention());

        /// <summary>
        /// Applies settings from <see cref="HelpOptionAttribute" /> on the model type.
        /// </summary>
        /// <param name="builder">The builder.</param>
        /// <returns>The builder.</returns>
        public static IConventionBuilder UseHelpOptionAttribute(this IConventionBuilder builder)
            => builder.AddConvention(new HelpOptionAttributeConvention());

        /// <summary>
        /// Applies settings from <see cref="OptionAttribute" /> on the model type.
        /// </summary>
        /// <param name="builder">The builder.</param>
        /// <returns>The builder.</returns>
        public static IConventionBuilder UseOptionAttributes(this IConventionBuilder builder)
            => builder.AddConvention(new OptionAttributeConvention());

        /// <summary>
        /// Applies settings from <see cref="ArgumentAttribute" /> on the model type.
        /// </summary>
        /// <param name="builder">The builder.</param>
        /// <returns>The builder.</returns>
        public static IConventionBuilder UseArgumentAttributes(this IConventionBuilder builder)
            => builder.AddConvention(new ArgumentAttributeConvention());

        /// <summary>
        /// Adds subcommands for each <see cref="SubcommandAttribute" /> on the model type.
        /// </summary>
        /// <param name="builder">The builder.</param>
        /// <returns>The builder.</returns>
        public static IConventionBuilder UseSubcommandAttributes(this IConventionBuilder builder)
            => builder.AddConvention(new SubcommandAttributeConvention());

        /// <summary>
        /// Invokes a method named "OnValidationError" on the model type.
        /// </summary>
        /// <param name="builder">The builder.</param>
        /// <returns>The builder.</returns>
        public static IConventionBuilder UseOnValidationErrorMethodFromModel(this IConventionBuilder builder)
            => builder.AddConvention(new ValidationErrorMethodConvention());

        /// <summary>
        /// Sets a method named "OnExecute" or "OnExecuteAsync" on the model type to handle
        /// <see cref="CommandLineApplication.Invoke" />
        /// </summary>
        /// <param name="builder">The builder.</param>
        /// <returns>The builder.</returns>
        public static IConventionBuilder UseOnExecuteMethodFromModel(this IConventionBuilder builder)
            => builder.AddConvention(new ExecuteMethodConvention());

        /// <summary>
        /// Enables using constructor injection to initialize the model type.
        /// </summary>
        /// <param name="builder"></param>
        public static IConventionBuilder UseConstructorInjection(this IConventionBuilder builder)
            => builder.AddConvention(new ConstructorInjectionConvention());

        /// <summary>
        /// Enables using constructor injection to initialize the model type.
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="additionalServices">Additional services that should be passed to the service provider.</param>
        public static IConventionBuilder UseConstructorInjection(this IConventionBuilder builder, IServiceProvider additionalServices)
            => builder.AddConvention(new ConstructorInjectionConvention(additionalServices));
    }
}
