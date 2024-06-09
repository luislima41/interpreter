using System;

namespace Calc {
    public class Lexer {
        public static char EOF = '\0';

        public string Input { get; set; }
        private int _ptr;
        public SymbolTable Table { get; private set; }

        public Lexer(string input, SymbolTable table) {
            Input = input;
            _ptr = 0;
            Table = table;
        }

        private char Scan() {
            if (_ptr == Input.Length)
                return EOF;
            return Input[_ptr++];
        }

        private int ParseInt(char c) {
            return c switch
            {
                '0' => 0,
                '1' => 1,
                '2' => 2,
                '3' => 3,
                '4' => 4,
                '5' => 5,
                '6' => 6,
                '7' => 7,
                '8' => 8,
                '9' => 9,
                _ => -1,
            };
        }

        public Token NextToken() {
            char? c = Scan();
            if (c == null)
                return new Token { Type = TokenType.EOF };

            while (c == ' ' || c == '\t')
            {
                c = Scan();
            }
            switch (c)
            {
                case '+': return new Token { Type = TokenType.SUM };
                case '-': return new Token { Type = TokenType.SUB };
                case '(': return new Token { Type = TokenType.OPEN };
                case ')': return new Token { Type = TokenType.CLOSE };
                case '=': return new Token { Type = TokenType.EQ };
            }

            if (Char.IsDigit(c.Value))
            {
                var x = ParseInt(c.Value);
                c = Scan();
                while (Char.IsDigit(c.Value))
                {
                    x = x * 10 + ParseInt(c.Value);
                    c = Scan();
                }
                _ptr--;
                return new Token { Type = TokenType.NUM, Value = x };
            }
            if (Char.IsUpper(c.Value))
            { 
                if (c == 'P')
                {
                    var lookahead = Scan();
                    if (lookahead == 'R')
                    {
                        lookahead = Scan();
                        if (lookahead == 'I')
                        {
                            lookahead = Scan();
                            if (lookahead == 'N')
                            {
                                lookahead = Scan();
                                if (lookahead == 'T')
                                {
                                    return new Token { Type = TokenType.PRINT };
                                }
                            }
                        }
                    }
                }
                return new Token { Type = TokenType.UNK };
            }
            if (Char.IsLower(c.Value))
            { 
                var v = c.ToString();
                c = Scan();
                while (Char.IsLower(c.Value))
                {
                    v = v + c.ToString();
                    c = Scan();
                }
                _ptr--;
                return new Token { Type = TokenType.VAR, Value = Table.AddSymbol(v) };
            }

            return new Token { Type = TokenType.UNK };
        }
    }

    public struct Token {
        public TokenType Type;
        public int Value;
    }

    public enum TokenType {
        VAR, NUM, EQ, SUB, SUM, OPEN, CLOSE, PRINT, EOF, UNK
    }
}
