using System;
using System.Collections.Generic;
using XNA;

namespace FoldingAtomata.Modeling.Shading
{
    public class ShaderSnippet
    {
        public ShaderSnippet() : this("", "", "")
        {

        }
        public ShaderSnippet(string fields, string methods, string mainBodyCode)
        {
            _fields = fields;
            _methods = methods;
            _mainBodyCode = mainBodyCode;
        }

        public string GetFields()
        {
            return _fields;
        }
        public string GetMethods()
        {
            return _methods;
        }
        public string GetMainBodyCode()
        {
            return _mainBodyCode;
        }

        string _fields, _methods, _mainBodyCode;
    }
}
