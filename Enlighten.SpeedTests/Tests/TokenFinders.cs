using BenchmarkDotNet.Attributes;
using BigBook;
using Enlighten.Tokenizer;
using Enlighten.Tokenizer.BaseClasses;
using Enlighten.Tokenizer.Languages.English.Enums;
using Enlighten.Tokenizer.Languages.English.TokenFinders;
using Enlighten.Tokenizer.Utils;
using System.Linq;

namespace Enlighten.SpeedTests.Tests
{
    [MemoryDiagnoser]
    public class TokenFinders
    {
        [Params(3, 5, 10, 100, 1000)]
        public int Count;

        public string TextToParse;

        [Benchmark()]
        public Token NoSpanImplementation()
        {
            return new EllipsisNoSpan().IsMatch(new TokenizableStream<char>(TextToParse.ToCharArray()));
        }

        [GlobalSetup]
        public void Setup()
        {
            TextToParse = new string(Count.Times(_ => '.').ToArray());
        }

        [Benchmark(Baseline = true)]
        public Token SpanImplementation()
        {
            return new Ellipsis().IsMatch(new TokenizableStream<char>(TextToParse.ToCharArray()));
        }

        /// <summary>
        /// Finds ellipsis
        /// </summary>
        /// <seealso cref="TokenFinderBaseClass"/>
        private class EllipsisNoSpan : TokenFinderBaseClass
        {
            /// <summary>
            /// Gets the order.
            /// </summary>
            /// <value>The order.</value>
            public override int Order => 3;

            /// <summary>
            /// The actual implementation of the IsMatch done by the individual classes.
            /// </summary>
            /// <param name="tokenizer">The tokenizer.</param>
            /// <returns>The token.</returns>
            protected override Token IsMatchImpl(TokenizableStream<char> tokenizer)
            {
                if (tokenizer.End() || tokenizer.Current != '.')
                    return null;

                var StartPosition = tokenizer.Index;
                var EndPosition = StartPosition;

                var Count = 0;
                var FoundEllipsis = false;
                string FinalValue = "";
                while (!tokenizer.End() && (tokenizer.Current == '.' || char.IsWhiteSpace(tokenizer.Current)))
                {
                    FinalValue += tokenizer.Current;
                    if (tokenizer.Current == '.')
                    {
                        ++Count;
                        FoundEllipsis |= Count >= 3;
                        EndPosition = tokenizer.Index;
                    }
                    tokenizer.Consume();
                }
                if (!FoundEllipsis)
                    return null;

                return new Token(
                    EndPosition,
                    StartPosition,
                    TokenType.Ellipsis,
                    new string(FinalValue.Trim())
                );
            }
        }
    }
}