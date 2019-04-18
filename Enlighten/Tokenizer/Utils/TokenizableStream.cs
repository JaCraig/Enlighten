/*
Copyright 2019 James Craig

Licensed under the Apache License, Version 2.0 (the "License");
you may not use this file except in compliance with the License.
You may obtain a copy of the License at

    http://www.apache.org/licenses/LICENSE-2.0

Unless required by applicable law or agreed to in writing, software
distributed under the License is distributed on an "AS IS" BASIS,
WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
See the License for the specific language governing permissions and
limitations under the License.
*/

using System;
using System.Collections.Generic;

namespace Enlighten.Tokenizer.Utils
{
    /// <summary>
    /// Tokenizable stream
    /// </summary>
    /// <typeparam name="TData">The type of the data.</typeparam>
    public class TokenizableStream<TData>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TokenizableStream{TData}"/> class.
        /// </summary>
        /// <param name="content">The content.</param>
        public TokenizableStream(TData[] content)
        {
            Index = 0;
            Content = content ?? Array.Empty<TData>();
            SnapshotIndexes = new Stack<int>();
        }

        /// <summary>
        /// Gets the current item.
        /// </summary>
        /// <value>The current item.</value>
        public TData Current => EOF(0) ? default : Content[Index];

        /// <summary>
        /// Gets or sets the current index.
        /// </summary>
        /// <value>The index.</value>
        public int Index { get; private set; }

        /// <summary>
        /// Gets or sets the content.
        /// </summary>
        /// <value>The content.</value>
        private TData[] Content { get; }

        /// <summary>
        /// Gets or sets the snapshot indexes.
        /// </summary>
        /// <value>The snapshot indexes.</value>
        private Stack<int> SnapshotIndexes { get; }

        /// <summary>
        /// Commits the snapshot.
        /// </summary>
        public void CommitSnapshot()
        {
            SnapshotIndexes.Pop();
        }

        /// <summary>
        /// Consumes the next item in the stream.
        /// </summary>
        public void Consume()
        {
            Index++;
        }

        /// <summary>
        /// Determines if this stream is done.
        /// </summary>
        /// <returns>True if it is done, false otherwise.</returns>
        public bool End()
        {
            return EOF(0);
        }

        /// <summary>
        /// Peeks at the next item.
        /// </summary>
        /// <param name="lookahead">The amount of items to look ahead.</param>
        /// <returns>The item at the spot specified.</returns>
        public TData Peek(int lookahead)
        {
            return EOF(lookahead) ? default : Content[Index + lookahead];
        }

        /// <summary>
        /// Rolls back the snapshot.
        /// </summary>
        public void RollbackSnapshot()
        {
            Index = SnapshotIndexes.Pop();
        }

        /// <summary>
        /// Returns a slice of the array starting at startIndex and ending at endIndex.
        /// </summary>
        /// <param name="startIndex">The start index.</param>
        /// <param name="endIndex">The end index.</param>
        /// <returns>The span.</returns>
        public Span<TData> Slice(int startIndex, int endIndex)
        {
            return Content.AsSpan(startIndex, (endIndex - startIndex) + 1);
        }

        /// <summary>
        /// Takes a snapshot.
        /// </summary>
        public void TakeSnapshot()
        {
            SnapshotIndexes.Push(Index);
        }

        /// <summary>
        /// Determines if the look ahead amount would be after the end of file.
        /// </summary>
        /// <param name="lookahead">The number of items to look ahead.</param>
        /// <returns>True if this is the end of file, false otherwise.</returns>
        protected bool EOF(int lookahead)
        {
            return Index + lookahead >= Content.Length;
        }
    }
}