using System;

namespace Calc {
    class CalcInt {
        public static void Main(string[] args) {
            var interpreter = new Interpreter();
            Console.Beep();
            Console.WriteLine("Calc Interpreter");
            string? command = "";
            do {
                Console.Write(">");
                command = Console.ReadLine();
                double result = interpreter.Exec(command); 
                string output = result.ToString(); 
                Console.WriteLine(output);
            } while (!string.IsNullOrEmpty(command));
        }
    }
}
