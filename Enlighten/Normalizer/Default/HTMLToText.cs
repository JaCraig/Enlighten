using Enlighten.Normalizer.Interfaces;
using System;

namespace Enlighten.Normalizer.Default
{
    /// <summary>
    /// HTML to text normalizer
    /// </summary>
    /// <seealso cref="ITextNormalizer"/>
    public class HTMLToText : ITextNormalizer
    {
        public string Name => throw new NotImplementedException();

        public int Order => throw new NotImplementedException();

        public string Normalize(string text) => throw new NotImplementedException();
    }
}