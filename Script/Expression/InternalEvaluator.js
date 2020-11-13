package Kodama.Script.Expression {

    public class InternalEvaluator {

        public static function Eval(expression : Object, unsafe : boolean) : Object
        {
            if (unsafe)
            {
                return eval(expression, "unsafe");
            }
            else{
                return eval(expression);
            }
        }

        public static function Eval(expression : Object) : Object
        {
            return eval(expression);
        }
    }
}
