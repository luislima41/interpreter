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
                return value; // Retornando o valor diretamente
            }
            else
            {
                return Atrib(); // Corrigindo o retorno para chamar o método Atrib()
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
                value = double.Parse(_lookahead.Value.ToString()); // Convertendo para int
                Match(TokenType.NUM);
            } else if (_lookahead.Type == TokenType.VAR) {
                // Aqui você irá lidar com variáveis.
                // Por enquanto, vamos apenas retornar 0 para demonstrar que a lógica precisa ser implementada.
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
            string variableName = _lookahead.Value.ToString(); // Converter o valor para string
            Match(TokenType.VAR);
            Match(TokenType.EQ);
            double value = Expr();
            // Aqui você iria atribuir o valor 'value' à variável 'variableName' em sua lógica de interpretação.
            // Por exemplo, você pode armazenar essas atribuições em algum lugar para usá-las posteriormente.
            return value; // Retornando o valor diretamente por enquanto
        }

        // Função hipotética para obter o valor de uma variável
        private double GetValueOfVariable(string variableName) {
            // Implemente esta função conforme necessário para sua lógica de interpretação
            return 0; // Apenas um valor de exemplo, substitua conforme necessário
        }
    }
}
