using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ValveFormat
{
    public class DataNode
    {
        public string Key { get; set; }
        public string Value { get; set; }
        public DataNode Parent { get; set; }
        public List<DataNode> SubNodes { get; set; }
        
        /// <summary>
        /// Initializes a DataNode without a key a value or subnodes.
        /// </summary>
        public DataNode()
        {
            SubNodes = new List<DataNode>();
        }
        /// <summary>
        /// Initializes a DataNode with a key and a value
        /// </summary>
        /// <param name="key">The key</param>
        /// <param name="value">The value</param>
        public DataNode(string key, string value) : this()
        {
            Key = key;
            Value = value;
        }

        /// <summary>
        /// Initializes a DataNode with only a key (this DataNode can contain SubNodes instead of a value)
        /// </summary>
        /// <param name="key">The key</param>
        public DataNode(string key) : this()
        {
            Key = key;
        }

        /// <summary>
        /// Initializes a DataNode with a key and a value
        /// </summary>
        /// <param name="key">The key</param>
        /// <param name="value">The value</param>
        /// <param name="parent">The parent of this DataNode</param>
        public DataNode(string key, string value, DataNode parent) : this(key,value)
        {
            Parent = parent;
        }

        /// <summary>
        /// Initializes a DataNode with only a key (this DataNode can contain SubNodes instead of a value)
        /// </summary>
        /// <param name="key">The key</param>
        /// <param name="parent">The parent of this DataNode</param>
        public DataNode(string key, DataNode parent) : this(key)
        {
            Parent = parent;
        }

        /// <summary>
        /// Finds the level that this DataNode has (how many Parents until the root node)
        /// </summary>
        /// <returns>The level number</returns>
        public int FindLevelNumber()
        {
            DataNode cur = this;
            int ret = 0;
            while (true)
            {
                cur = cur.Parent;
                if (cur != null) ret++;
                else return ret;
            }
        }
        /// <summary>
        /// Gets a string containing the required number of \t for correct indentation in a file.
        /// </summary>
        /// <returns>A string with \t repeated the node's level times (see also <seealso cref="FindLevelNumber"/>)</returns>
        public string GetIndentString()
        {
            int lvl = FindLevelNumber();
            return String.Concat(Enumerable.Repeat("\t", lvl));
        }
        public override string ToString()
        {
            string s = "";
            foreach (DataNode n in SubNodes)
            {
                s += n.GetIndentString() +  n;
            }
            return string.Format(Key + " = " + (Value == null ? null : Value.Replace("\r\n", "\r\n" + GetIndentString())) + "\r\n" + s);
        }

    }
}
