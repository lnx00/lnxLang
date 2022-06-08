using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lnxLang.Utils
{
    internal class Reader
    {
        private readonly string _text;
        private int _position;

        public Reader(string text)
        {
            _text = text;
            _position = 0;
        }

        public int GetPosition()
        {
            return _position;
        }

        /* Sets the current reading position */
        public void Seek(int position)
        {
            _position = position;
        }

        /* Seeks until the next char isn't a whitespace */
        public void SeekStart()
        {
            while (CanRead() && (PeekChar() == ' ' || PeekChar() == '\r' || PeekChar() == '\n'))
            {
                _position++;
            }
        }

        /* Seeks until the given character */
        public void SeekUntil(char end)
        {
            while (CanRead() && ReadChar() != end) { }
        }

        /* Skips the next n characters */
        public void Skip(int n = 1)
        {
            _position += n;
        }

        /* Returns whether there are characters left to read */
        public bool CanRead()
        {
            return _position < _text.Length;
        }

        /* Reads the next character */
        public char ReadChar()
        {
            char result = _text[_position];
            _position++;
            return result;
        }

        /* Peeks the current character without changing the position */
        public char PeekChar()
        {
            return _text[_position];
        }

        /* Peeks the next n characters */
        public string Peek(int n = 1)
        {
            string result = "";

            for (int i = 0; i < n; i++)
            {
                if (_position + i >= _text.Length)
                {
                    break;
                }
                result += _text[_position + i];
            }

            return result;
        }

        /* Reads until the given character */
        public string ReadUntil(char end)
        {
            string result = "";

            while (CanRead())
            {
                char next = ReadChar();
                if (next == end) { break; }

                result += next;
            }

            return result;
        }

        /* Reads until the given string */
        public string ReadUntil(string end)
        {
            string result = "";

            while (CanRead())
            {
                bool found = true;
                int posBackup = _position;
                foreach (char c in end)
                {
                    if (CanRead() && ReadChar() != c)
                    {
                        found = false;
                    }
                }
                _position = posBackup;

                if (found) { break; }
                result += ReadChar();
            }

            return result;
        }

        /* Peeks until the given character */
        public string PeekUntil(char end)
        {
            // TODO: There's probably a better way to do this
            int posBackup = _position;
            string result = ReadUntil(end);
            _position = posBackup;

            return result;
        }

        /* Reads until the next whitespace */
        public string ReadWord()
        {
            return ReadUntil(' ');
        }

        /* Peeks until the next whitespace */
        public string PeekWord()
        {
            return PeekUntil(' ');
        }

        /* Reads until the next linebreak */
        public string ReadLine()
        {
            return ReadUntil("\r\n");
        }

        /* Reads until the end */
        public string ReadAll()
        {
            string result = "";
            while (CanRead())
            {
                result += ReadChar();
            }

            return result;
        }

        public string ReadStack(char start, char end)
        {
            string result = "";
            Stack<char> stack = new();
            SeekUntil(start);
            stack.Push(start);

            while (CanRead())
            {
                char current = ReadChar();

                if (current == start) { stack.Push(current); }
                if (current == end) { stack.Pop(); }
                if (stack.Count == 0) { break; }

                result += current;
            }

            return result;
        }

        public string ReadString(char c = '\"')
        {
            string result = "";
            SeekUntil(c);

            char last = c;
            while (CanRead())
            {
                char current = ReadChar();
                if (current == c && last != '\\')
                {
                    break;
                }

                result += current;
                last = current;
            }

            return result;
        }

    }
}
