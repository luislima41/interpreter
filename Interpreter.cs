using System;

namespace Calc {
    public class Interpreter {
        private SymbolTable _table;

        public Interpreter() {
            _table = new SymbolTable();
        }

        public double Exec(string? command) {
            if (command == null) return double.NaN;

            var lexer = new Lexer(command, _table);
            return Parser.Cmd(lexer);
        }
    }
}
