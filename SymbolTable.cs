// SymbolTable.cs
using System;
using System.Collections.Generic;

namespace Calc {
    public class SymbolTable {
        private List<Entry> _symbols;

        public SymbolTable() {
            _symbols = new List<Entry>();
        }

        public int AddSymbol(string varName) {
            var entry = new Entry { VarName = varName, Value = null };
            _symbols.Add(entry);
            return _symbols.Count - 1;
        }

        public Entry GetSymbol(int index) {
            if (index >= 0 && index < _symbols.Count)
            {
                return _symbols[index];
            }
            throw new IndexOutOfRangeException("Index out of range in SymbolTable.");
        }

        public void SetSymbol(int index, Entry entry) {
            if (index >= 0 && index < _symbols.Count)
            {
                _symbols[index] = entry;
            }
            else
            {
                throw new IndexOutOfRangeException("Index out of range in SymbolTable.");
            }
        }
    }

    public struct Entry {
        public string VarName;
        public double? Value;
    }
}
