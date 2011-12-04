using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;


namespace ValveFormat
{
    public class ValveFormatParser
    {
        public string Path { get; set; }
        public DataNode RootNode { get; set; }
        public char[] TrimChars = new[] {'\r', '\n', '\t'};
		internal static bool _indent = true;
        /// <summary>
        /// Creates a new Valve Format Parser
        /// </summary>
        /// <param name="path">File to parse</param>
        public ValveFormatParser(string path)
        {
            Path = path;
        }

        /// <summary>
        /// Creates a new Valve Format Parser
        /// </summary>
        public ValveFormatParser()
        {
            
        }

        /// <summary>
        /// Finds the parent node of the given
        /// </summary>
        /// <param name="node">The node to find the parent of</param>
        /// <returns>The parent node or null when the node has no parent</returns>
        public DataNode FindParentNode(DataNode node)
        {
            foreach (DataNode n in RootNode.SubNodes)
            {
                if (n == node) return n.Parent;
                var subN = FindInSubNodes(node, n);
                if (subN != null) return subN;
            }
            return null;
        }
        private DataNode FindInSubNodes(DataNode searchFor, DataNode source)
        {
            foreach (DataNode n in source.SubNodes)
            {
                if (n == searchFor) return n;
                var subN = FindInSubNodes(searchFor, n);
                if (subN != null) return subN;
            }
            return null;
        }
        /// <summary>
        /// Clears all nodes and loads them from <see cref="Path"/>
        /// </summary>
        public void LoadFile()
        {
            string[] lines = File.ReadAllLines(Path);
            RootNode = new DataNode();
            Regex regex;
            MatchCollection matches;
            var currentNode = new DataNode();
            bool multiLineValue = false;
            DataNode multiLineNode = new DataNode();
            foreach (string _line in lines)
            {
                string line = _line.TrimEnd(TrimChars).TrimStart(TrimChars).Replace("\r\n", ""); //Can't change the value of _line, trim whitespace chars and assign to line
                if (multiLineValue)
                {
                    regex = new Regex("(.*)\"");
                    matches = regex.Matches(line);
                    if (matches.Count == 1)
                    {
                        multiLineNode.Value += matches[0].Groups[1];
                        multiLineValue = false;
                        currentNode.SubNodes.Add(multiLineNode);
                        Console.Write(matches[0].Groups[1] + "\r\n");
                        continue;
                    }
                    regex = new Regex("(.*)");
                    matches = regex.Matches(line);
                    if (matches.Count == 1)
                    {
                        multiLineNode.Value += matches[0].Groups[1] + "\r\n";
                        Console.Write(matches[0].Groups[1] + "\r\n");
                        continue;
                    }
                }
                regex = new Regex("\"(.*?)\"");
                matches = regex.Matches(line);
                if (matches.Count == 1 && line.Count(f => f == '"') == 2)
                {
                    string k = matches[0].Groups[1].Value;
                    if (currentNode.Key == null)
                        currentNode = new DataNode(k);
                    else
                    {
                        var no = new DataNode(k, currentNode);
                        currentNode.SubNodes.Add(no);
                        currentNode = no;
                    }
                    continue;
                }

                if (line.Trim(new[] { ' ', '\r', '\n', '\t' }) == "{")
                {
                    continue;
                }
                if (line.Trim(new[] { ' ', '\r', '\n', '\t' }) == "}")
                {
                    currentNode = currentNode.Parent ?? currentNode;
                    continue;
                }
                regex = new Regex(@"""(.*?)""\s*""(.*?)""");
                matches = regex.Matches(line);
                if (matches.Count > 1)
                {
                    throw new MultipleKeyValuePairException(line);
                }
                if (matches.Count == 1)
                {
                    var k = matches[0].Groups[1].Value;
                    var v = matches[0].Groups[2].Value;
                    currentNode.SubNodes.Add(new DataNode(k, v, currentNode));
                    continue;
                }
                regex = new Regex(@"""(.*?)""\s*""(.*)");
                matches = regex.Matches(line);
                if (matches.Count == 1)
                {
                    multiLineValue = true;
                    multiLineNode = new DataNode(matches[0].Groups[1].Value, matches[0].Groups[2].Value + "\r\n", currentNode);
                    continue;
                }

            }
            RootNode = currentNode;

        }

    	/// <summary>
    	/// Saves the file
    	/// </summary>
    	/// <param name="path">The path to save the file</param>
    	/// <param name="indent">Defines whether to indent the nodes or not</param>
    	public void SaveFile(string path, bool indent = true)
    	{
    		_indent = indent;
            File.WriteAllText(path, GetWriteText(RootNode));
        }
        private string GetWriteText(DataNode node)
        {
            string append = "";
            if (!String.IsNullOrEmpty(node.Key))
            {
                append += (_indent ? node.GetIndentString() : "") + "\"" + node.Key + "\"";
            }
            if (node.Value != null)
            {
                append += "\t\"" + node.Value + "\"\r\n";
            }
            else
            {
				append += "\r\n" + (_indent ? node.GetIndentString() : "") +"{\r\n";
            }
            foreach (DataNode sub in node.SubNodes)
            {
                append += GetWriteText(sub);
            }
            if (node.Value == null)
            {
				append += (_indent ? node.GetIndentString() : "") + "}\r\n";
            }
            return append;
        }
    }
}
