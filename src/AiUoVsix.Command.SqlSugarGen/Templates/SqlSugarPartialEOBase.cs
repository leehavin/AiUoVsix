using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using System.Text;

namespace AiUoVsix.Command.SqlSugarGen.Templates
{
    [GeneratedCode("Microsoft.VisualStudio.TextTemplating", "17.0.0.0")]
    public class SqlSugarPartialEOBase
    {
        private StringBuilder generationEnvironmentField;
        private CompilerErrorCollection errorsField;
        private List<int> indentLengthsField;
        private string currentIndentField = "";
        private bool endsWithNewline;
        private IDictionary<string, object> sessionField;
        private SqlSugarPartialEOBase.ToStringInstanceHelper toStringHelperField = new SqlSugarPartialEOBase.ToStringInstanceHelper();

        public StringBuilder GenerationEnvironment
        {
            get
            {
                if (this.generationEnvironmentField == null)
                    this.generationEnvironmentField = new StringBuilder();
                return this.generationEnvironmentField;
            }
            set => this.generationEnvironmentField = value;
        }

        public CompilerErrorCollection Errors
        {
            get
            {
                if (this.errorsField == null)
                    this.errorsField = new CompilerErrorCollection();
                return this.errorsField;
            }
        }

        private List<int> indentLengths
        {
            get
            {
                if (this.indentLengthsField == null)
                    this.indentLengthsField = new List<int>();
                return this.indentLengthsField;
            }
        }

        public string CurrentIndent => this.currentIndentField;

        public virtual IDictionary<string, object> Session
        {
            get => this.sessionField;
            set => this.sessionField = value;
        }

        public void Write(string textToAppend)
        {
            if (string.IsNullOrEmpty(textToAppend))
                return;
            if (this.GenerationEnvironment.Length == 0 || this.endsWithNewline)
            {
                this.GenerationEnvironment.Append(this.currentIndentField);
                this.endsWithNewline = false;
            }
            if (textToAppend.EndsWith(Environment.NewLine, StringComparison.CurrentCulture))
                this.endsWithNewline = true;
            if (this.currentIndentField.Length == 0)
            {
                this.GenerationEnvironment.Append(textToAppend);
            }
            else
            {
                textToAppend = textToAppend.Replace(Environment.NewLine, Environment.NewLine + this.currentIndentField);
                if (this.endsWithNewline)
                    this.GenerationEnvironment.Append(textToAppend, 0, textToAppend.Length - this.currentIndentField.Length);
                else
                    this.GenerationEnvironment.Append(textToAppend);
            }
        }

        public void WriteLine(string textToAppend)
        {
            this.Write(textToAppend);
            this.GenerationEnvironment.AppendLine();
            this.endsWithNewline = true;
        }

        public void Write(string format, params object[] args)
        {
            this.Write(string.Format((IFormatProvider)CultureInfo.CurrentCulture, format, args));
        }

        public void WriteLine(string format, params object[] args)
        {
            this.WriteLine(string.Format((IFormatProvider)CultureInfo.CurrentCulture, format, args));
        }

        public void Error(string message)
        {
            this.Errors.Add(new CompilerError()
            {
                ErrorText = message
            });
        }

        public void Warning(string message)
        {
            this.Errors.Add(new CompilerError()
            {
                ErrorText = message,
                IsWarning = true
            });
        }

        public void PushIndent(string indent)
        {
            if (indent == null)
                throw new ArgumentNullException(nameof(indent));
            this.currentIndentField += indent;
            this.indentLengths.Add(indent.Length);
        }

        public string PopIndent()
        {
            string str = "";
            if (this.indentLengths.Count > 0)
            {
                int indentLength = this.indentLengths[this.indentLengths.Count - 1];
                this.indentLengths.RemoveAt(this.indentLengths.Count - 1);
                if (indentLength > 0)
                {
                    str = this.currentIndentField.Substring(this.currentIndentField.Length - indentLength);
                    this.currentIndentField = this.currentIndentField.Remove(this.currentIndentField.Length - indentLength);
                }
            }
            return str;
        }

        public void ClearIndent()
        {
            this.indentLengths.Clear();
            this.currentIndentField = "";
        }

        public SqlSugarPartialEOBase.ToStringInstanceHelper ToStringHelper => this.toStringHelperField;

        public class ToStringInstanceHelper
        {
            private IFormatProvider formatProviderField = (IFormatProvider)CultureInfo.InvariantCulture;

            public IFormatProvider FormatProvider
            {
                get => this.formatProviderField;
                set
                {
                    if (value == null)
                        return;
                    this.formatProviderField = value;
                }
            }

            public string ToStringWithCulture(object objectToConvert)
            {
                MethodInfo methodInfo = objectToConvert != null ? objectToConvert.GetType().GetMethod("ToString", new Type[1]
                {
          typeof (IFormatProvider)
                }) : throw new ArgumentNullException(nameof(objectToConvert));
                if (methodInfo == (MethodInfo)null)
                    return objectToConvert.ToString();
                return (string)methodInfo.Invoke(objectToConvert, new object[1]
                {
          (object) this.formatProviderField
                });
            }
        }
    }
}
