using System;
using System.Collections.Generic;

namespace JavaToCSharp.Comments.Syntax
{
    internal class JavaDoc
    {
        public JavaDoc()
        {
            this.See = new List<String>();
            this.Author = new List<String>();
            this.Return = new List<String>();
            this.Parameters = new List<JavaDocParameter>();
            this.Exceptions = new List<String>();
        }

        public String Summary { get; set; }
        public IList<String> See { get; set; }
        public IList<String> Author { get; set; }
        public IList<String> Return { get; set; }
        public IList<JavaDocParameter> Parameters { get; set; }
        public IList<String> Exceptions { get; set; }
        public String Depricated { get; set; }
        public String Since { get; set; }
    }
}