using System;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Vertical.Postman.ApiDoc.Structure
{

    /// <summary>
    /// collection of <see cref="RequestItem"/>s
    /// </summary>
    public class Collection
    {
        [IndexerName("Indexer")]
        public Item this[string path] {
            get {
                string[] chunks = path.Split('.');

                Item current = Item.FirstOrDefault(i => i.Name == chunks[0]);
                if (current == null)
                    throw new Exception($"{chunks[0]} is not part of the collection.");

                for (int index = 1; index < chunks.Length; ++index) {
                    Folder folder = current as Folder;
                    if (folder == null)
                        throw new Exception($"Invalid path. '{current.Name}' is not a folder.");

                    current = folder.Item.FirstOrDefault(i => i.Name == chunks[index]);
                    if (current == null)
                        throw new Exception($"{chunks[index]} is not part of the collection.");
                }

                return current;
            }
        }

        /// <summary>
        /// info about collection
        /// </summary>
        public CollectionInfo Info { get; set; }

        /// <summary>
        /// items in collection
        /// </summary>
        public Item[] Item { get; set; }

        /// <summary>
        /// collection variables
        /// </summary>
        public Variable[] Variable { get; set; }
    }
}
