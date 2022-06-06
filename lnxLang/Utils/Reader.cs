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

        /* Peeks the next character without changing the position */
        public char PeekChar()
        {
            return _text[_position];
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

    }
}
