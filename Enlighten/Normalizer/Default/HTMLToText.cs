using Enlighten.Normalizer.Interfaces;
using HtmlAgilityPack;
using Microsoft.Extensions.ObjectPool;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Enlighten.Normalizer.Default
{
    /// <summary>
    /// HTML to text normalizer
    /// </summary>
    /// <seealso cref="ITextNormalizer"/>
    public class HTMLToText : ITextNormalizer
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="HTMLToText"/> class.
        /// </summary>
        /// <param name="objectPool">The object pool.</param>
        public HTMLToText(ObjectPool<StringBuilder> objectPool)
        {
            ObjectPool = objectPool;
        }

        /// <summary>
        /// The contains HTML regex
        /// </summary>
        private static readonly Regex ContainsHtmlRegex = new Regex("<[^>]*>", RegexOptions.Compiled);

        /// <summary>
        /// The indent tags
        /// </summary>
        private readonly HashSet<string> IndentTags = new HashSet<string>(new[] { "li", "blockquote", "td", "th", "dd" });

        /// <summary>
        /// The line breaks
        /// </summary>
        private readonly HashSet<string> LineBreaks = new HashSet<string>(new[] { "p", "br", "table", "tr", "h1", "h2", "h3", "h4", "h5", "h6", "ol", "ul", "dt", "dd", "hr" });

        /// <summary>
        /// Gets the name.
        /// </summary>
        /// <value>The name.</value>
        public string Name { get; } = nameof(HTMLToText);

        /// <summary>
        /// Gets the object pool.
        /// </summary>
        /// <value>The object pool.</value>
        public ObjectPool<StringBuilder> ObjectPool { get; }

        /// <summary>
        /// Gets the order.
        /// </summary>
        /// <value>The order.</value>
        public int Order { get; } = int.MinValue;

        /// <summary>
        /// Normalizes the specified text.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <returns>The normalized text.</returns>
        public string Normalize(string text)
        {
            if (!ContainsHTML(text))
                return text;
            return RemoveHTML(text);
        }

        /// <summary>
        /// Appends the indent.
        /// </summary>
        /// <param name="builder">The builder.</param>
        /// <param name="indent">The indent.</param>
        private void AppendIndent(StringBuilder builder, int indent)
        {
            for (int x = 0; x < indent; ++x)
            {
                builder.Append(' ');
            }
        }

        /// <summary>
        /// Determines whether the specified text contains HTML.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <returns><c>true</c> if the specified text contains HTML; otherwise, <c>false</c>.</returns>
        private bool ContainsHTML(string text)
        {
            return ContainsHtmlRegex.IsMatch(text);
        }

        /// <summary>
        /// Gets the text from nodes.
        /// </summary>
        /// <param name="builder">The builder.</param>
        /// <param name="childNodes">The child nodes.</param>
        /// <param name="indent">The indent.</param>
        private void GetTextFromNodes(StringBuilder builder, HtmlNodeCollection childNodes, int indent)
        {
            foreach (var Node in childNodes)
            {
                if (string.Equals(Node.Name, "style", StringComparison.OrdinalIgnoreCase)
                    || string.Equals(Node.Name, "script", StringComparison.OrdinalIgnoreCase))
                {
                    continue;
                }

                if (Node.HasChildNodes)
                {
                    if (IndentTags.Contains(Node.Name))
                    {
                        GetTextFromNodes(builder, Node.ChildNodes, indent + 1);
                    }
                    else
                    {
                        GetTextFromNodes(builder, Node.ChildNodes, indent);
                    }
                }
                else
                {
                    var InnerText = Node.InnerText;
                    if (!string.IsNullOrWhiteSpace(InnerText))
                    {
                        AppendIndent(builder, indent);
                        builder.Append(Node.InnerText);
                        if (builder.Length > 0 && builder[^1] != ' ')
                        {
                            builder.Append(' ');
                        }
                    }
                }

                if (LineBreaks.Contains(Node.Name) && builder.Length > 0 && builder[builder.Length - 1] != '\n')
                {
                    builder.AppendLine();
                }
            }
        }

        /// <summary>
        /// Removes the HTML.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <returns></returns>
        private string RemoveHTML(string text)
        {
            var Builder = ObjectPool.Get();
            var Doc = new HtmlDocument();
            Doc.LoadHtml(text);

            GetTextFromNodes(Builder, Doc.DocumentNode.ChildNodes, 0);

            var ReturnValue = Builder.ToString();
            ObjectPool.Return(Builder);
            return ReturnValue;
        }
    }
}