using System;
using System.Collections.Generic;

namespace FoldingAtomata.PyON
{
    public class StringManip
    {
        public static string Between(String str, String header, String footer, int start = 0)
        {
            var index = Between(str, new Pair<int, int>(start, str.Length - 1), header, footer);
            return str.Substring(index.First, index.Second - index.First);
        }
        public static Pair<int, int> Between(String str, Pair<int, int> indexes, String header, String footer)
        {
            int head = str.IndexOf(header, indexes.First);
            int foot = str.IndexOf(footer, head);
            int startIndex = head + header.Length;
            return new Pair<int, int>(startIndex, foot);
        }
        public static List<String> ExplodeAndTrim(String str, char delim, String whitespaces)
        {
            //var tokens = Explode(str, delim);
            var tokens = str.Split(delim);
            //std::cout << "explodeTrim: " << tokens[0] << "," << tokens[1] << std::endl;

            //string token;
            //std.transform(tokens[0], tokens[tokens.Count-1], tokens[0], out token);
            //token = Trim(token, whitespaces);
            for (int i = 0; i < tokens.Length; i++)
            {
                tokens[i] = Trim(tokens[i], whitespaces);
            }

            return new List<string>(tokens);
        }
        public static List<String> Explode(String str, char delim)
        {
            //var indexes = Explode(str, new Pair<int, int>(0, str.Length), delim);
            List<string> tokens = new List<string>(str.Split(delim));
            //foreach (var pair in indexes)
            //    tokens.Add(str.Substring(pair.First, pair.Second - pair.First + 1));
            return tokens;
        }
        /*
        public static List<Pair<int, int>> Explode(String str, Pair<int, int> indexes, char delim)
        {
            List<Pair<int, int>> indexesVector = new List<Pair<int,int>>();
            int startIndex = indexes.First;
            while (startIndex < indexes.Second)
            {
                int lastIndex = str.IndexOf(delim, startIndex);
                if (lastIndex == -1)
                {
                    indexesVector.Add(new Pair<int, int>(startIndex, indexes.Second - 1));
                    break;
                }

                indexesVector.Add(new Pair<int, int>(startIndex, lastIndex - 1));
                startIndex = lastIndex + 1;
            }

            return indexesVector;
        }
        */
        public static String Trim(String str, String whitespaces)
        {
            //std::cout << "asked to trim: " << str << std::endl;

            //var index = Trim(str, new Pair<int, int>(0, str.Length - 1), whitespaces);
            //std::cout << "trimIs: " << index.first << "()" << index.second << std::endl;
            //std::cout << "Tresult: " << str.substr(index.first, index.second - index.first + 1) << std::endl;

            // return str.Substring(index.First, index.Second - index.First + 1);
            var chars = whitespaces.ToCharArray();
            return str.TrimStart(chars).TrimEnd(chars);
        }
        /*
        public static Pair<int, int> Trim(String str, Pair<int, int> indexes, String whitespaces)
        {
            int start = str.Find_First_Not_Of(whitespaces, indexes.First);
            int end = str.Find_Last_Not_Of(whitespaces, indexes.Second);

            //std::cout << "trim: " << start << "," << end << std::endl;

            if (start != -1)
                return new Pair<int, int>(start, end);
            //std::cout << "returned blank" << std::endl;
            return new Pair<int, int>(indexes.First, indexes.First - 1);
        }
        */
        public static bool StartsWith(String a, String b)
        {
            return a.StartsWith(b);
        }
    }
}
