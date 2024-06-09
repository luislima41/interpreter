using System;

namespace Calc {
    public class Parser {
        private Lexer _lexer;
        private Token _lookahead;

        public Parser(Lexer lexer) {
            _lexer = lexer;
            _lookahead = _lexer.NextToken();
        }

        public void Match(TokenType token) {
            if (_lookahead.Type == token)
            {
                _lookahead = _lexer.NextToken();
            }
            else
            {
                throw new Exception("Erro de análise: token inesperado.");
            }
        }

        public static double Cmd(Lexer lexer) {
            var parser = new Parser(lexer);
            return parser.Cmd();
        }

        public double Cmd() {
            if (_lookahead.Type == TokenType.PRINT)
            {
                Match(TokenType.PRINT);
                var value = Expr();
                return value;
            }
            else
            {
                return Atrib();
            }
        }

        public double Expr() {
            double result = Term();

            while (_lookahead.Type == TokenType.SUM || _lookahead.Type == TokenType.SUB) {
                if (_lookahead.Type == TokenType.SUM) {
                    Match(TokenType.SUM);
                    result += Term();
                } else if (_lookahead.Type == TokenType.SUB) {
                    Match(TokenType.SUB);
                    result -= Term();
                }
            }

            return result;
        }

        public double Term() {
            double result = Factor();

            while (_lookahead.Type == TokenType.NUM || _lookahead.Type == TokenType.VAR) {
                result *= Factor();
            }

            return result;
        }

        public double Factor() {
            double value = 0;
            if (_lookahead.Type == TokenType.NUM) {
                value = double.Parse(_lookahead.Value.ToString());
                Match(TokenType.NUM);
            } else if (_lookahead.Type == TokenType.VAR) {
                var variableValue = GetValueOfVariable(_lookahead.Value.ToString());
                value = variableValue;
                Match(TokenType.VAR);
            } else if (_lookahead.Type == TokenType.OPEN) {
                Match(TokenType.OPEN);
                value = Expr();
                Match(TokenType.CLOSE);
            } else {
                throw new Exception("Expressão inválida.");
            }
            return value;
        }

        public double Atrib() {
            string variableName = _lookahead.Value.ToString(); 
            Match(TokenType.VAR);
            Match(TokenType.EQ);
            double value = Expr();
            return value; 
        }
        private double GetValueOfVariable(string variableName) {

            return 0; 
        }
    }
}
