﻿namespace ShaderTools.CodeAnalysis.Syntax
{
    public enum SyntaxTokenSource
    {
        /// <summary>
        /// Token was lexed directly from a file. This applies to most tokens.
        /// </summary>
        File,

        /// <summary>
        /// Token was created as part of a macro expansion. The MacroReference property 
        /// contains the macro that was responsible for creating this token.
        /// </summary>
        MacroExpansion,

        /// <summary>
        /// Token was generated by the parser as a stand-in for a token that was required
        /// by the node being parsed, but not present.
        /// </summary>
        MissingToken
    }
}
