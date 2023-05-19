﻿using System;

namespace Jurassic.Compiler
{
    /// <summary>
    /// Represents the unit of compilation.
    /// </summary>
    internal abstract class MethodGenerator
    {
        /// <summary>
        /// Creates a new MethodGenerator instance.
        /// </summary>
        /// <param name="source"> The source of javascript code. </param>
        /// <param name="options"> Options that influence the compiler. </param>
        protected MethodGenerator(ScriptSource source, CompilerOptions options)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));
            if (options == null)
                throw new ArgumentNullException(nameof(options));
            this.Source = source;
            this.Options = options;
            this.StrictMode = this.Options.ForceStrictMode;
        }

        /// <summary>
        /// Gets a reference to any compiler options.
        /// </summary>
        public CompilerOptions Options
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the source of javascript code.
        /// </summary>
        public ScriptSource Source { get; private set; }

        /// <summary>
        /// Gets a value that indicates whether strict mode is enabled.
        /// </summary>
        public bool StrictMode { get; protected set; }

        /// <summary>
        /// Gets the root node of the abstract syntax tree.  This will be <c>null</c> until Parse()
        /// is called.
        /// </summary>
        public Statement AbstractSyntaxTree { get; protected set; }

        /// <summary>
        /// Gets the top-level scope.  This will be <c>null</c> until Parse() is called.
        /// </summary>
        public Scope BaseScope { get; protected set; }

        /// <summary>
        /// Gets or sets optimization information.
        /// </summary>
        public MethodOptimizationHints MethodOptimizationHints
        {
            get;
            set;
        }

        /// <summary>
        /// Gets the generated IL.  This will be <c>null</c> until GenerateCode() is
        /// called.
        /// </summary>
        public ILGenerator ILGenerator
        {
            get;
            protected set;
        }

        /// <summary>
        /// Gets a delegate to the emitted dynamic method, plus any dependencies.  This will be
        /// <c>null</c> until GenerateCode() is called.
        /// </summary>
        public GeneratedMethod GeneratedMethod
        {
            get;
            protected set;
        }

        /// <summary>
        /// Gets a name for the generated method.
        /// </summary>
        /// <returns> A name for the generated method. </returns>
        protected abstract string GetMethodName();

        /// <summary>
        /// Gets a name for the function, as it appears in the stack trace.
        /// </summary>
        /// <returns> A name for the function, as it appears in the stack trace, or <c>null</c> if
        /// this generator is generating code in the global scope. </returns>
        protected virtual string GetStackName()
        {
            return null;
        }

        /// <summary>
        /// Gets an array of types - one for each parameter accepted by the method generated by
        /// this context.
        /// </summary>
        /// <returns> An array of parameter types. </returns>
        protected virtual Type[] GetParameterTypes()
        {
            return new Type[] {
                typeof(ExecutionContext),   // The script engine, scope, this value, etc.
            };
        }

        /// <summary>
        /// Gets an array of names - one for each parameter accepted by the method being generated.
        /// </summary>
        /// <returns> An array of parameter names. </returns>
        protected virtual string[] GetParameterNames()
        {
            return new string[] { "executionContext" };
        }

        /// <summary>
        /// Retrieves a delegate for the generated method.
        /// </summary>
        /// <returns> The delegate type that matches the method parameters. </returns>
        protected abstract Type GetDelegate();

        /// <summary>
        /// Parses the source text into an abstract syntax tree.
        /// </summary>
        public abstract void Parse();

        /// <summary>
        /// Optimizes the abstract syntax tree.
        /// </summary>
        public void Optimize()
        {
        }

        /// <summary>
        /// Generates IL for the script.
        /// </summary>
        public void GenerateCode()
        {
            // Generate the abstract syntax tree if it hasn't already been generated.
            if (this.AbstractSyntaxTree == null)
            {
                Parse();
                Optimize();
            }

            // Initialize global code-gen information.
            var optimizationInfo = new OptimizationInfo();
            optimizationInfo.AbstractSyntaxTree = this.AbstractSyntaxTree;
            optimizationInfo.StrictMode = this.StrictMode;
            optimizationInfo.MethodOptimizationHints = this.MethodOptimizationHints;
            optimizationInfo.FunctionName = this.GetStackName();
            optimizationInfo.Source = this.Source;

            // DynamicMethod requires full trust because of generator.LoadMethodPointer in the
            // FunctionExpression class.

            // Create a new dynamic method.
            System.Reflection.Emit.DynamicMethod dynamicMethod = new System.Reflection.Emit.DynamicMethod(
                GetMethodName(),                                        // Name of the generated method.
                typeof(object),                                         // Return type of the generated method.
                GetParameterTypes(),                                    // Parameter types of the generated method.
                typeof(MethodGenerator),                                // Owner type.
                true);                                                  // Skip visibility checks.
#if USE_DYNAMIC_IL_INFO
            ILGenerator generator = new DynamicILGenerator(dynamicMethod);
#else
            ILGenerator generator = new ReflectionEmitILGenerator(dynamicMethod, emitDebugInfo: false);
#endif

            ILGenerator loggingILGenerator = null;
            if (this.Options.EnableILAnalysis)
            {
                // Replace the generator with one that logs.
                generator = loggingILGenerator = new LoggingILGenerator(generator);
            }

#if DEBUG
            // Replace the generator with one that verifies correctness.
            generator = new VerifyingILGenerator(generator);
#endif

            // Initialization code will appear to come from line 1.
            optimizationInfo.MarkSequencePoint(generator, new SourceCodeSpan(1, 1, 1, 1));

            // Generate the IL.
            GenerateCode(generator, optimizationInfo);
            generator.Complete();

            // Create a delegate from the method.
            this.GeneratedMethod = new GeneratedMethod(dynamicMethod.CreateDelegate(GetDelegate()), optimizationInfo.NestedFunctions);
            
            if (loggingILGenerator != null)
            {
                // Store the disassembled IL so it can be retrieved for analysis purposes.
                this.GeneratedMethod.DisassembledIL = loggingILGenerator.ToString();
            }
        }

        /// <summary>
        /// Generates IL for the script.
        /// </summary>
        /// <param name="generator"> The generator to output the CIL to. </param>
        /// <param name="optimizationInfo"> Information about any optimizations that should be performed. </param>
        protected abstract void GenerateCode(ILGenerator generator, OptimizationInfo optimizationInfo);
    }

}