using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp36
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        abstract class Pair
        {
            public abstract Pair Add(Pair other);
            public abstract Pair Subtract(Pair other);
            public abstract Pair Multiply(Pair other);
            public abstract Pair Divide(Pair other);
        }

        
        class Money : Pair
        {
            public int Hryvnia { get; set; }
            public int Kopiyka { get; set; }

            public override Pair Add(Pair other)
            {
                Money money = (Money)other;
                Money result = new Money();
                result.Hryvnia = this.Hryvnia + money.Hryvnia;
                result.Kopiyka = this.Kopiyka + money.Kopiyka;
                if (result.Kopiyka >= 100)
                {
                    result.Hryvnia += result.Kopiyka / 100;
                    result.Kopiyka %= 100;
                }
                return result;
            }

            public override Pair Subtract(Pair other)
            {
                Money money = (Money)other;
                Money result = new Money();
                result.Hryvnia = this.Hryvnia - money.Hryvnia;
                result.Kopiyka = this.Kopiyka - money.Kopiyka;
                if (result.Kopiyka < 0)
                {
                    result.Hryvnia -= 1;
                    result.Kopiyka += 100;
                }
                return result;
            }

            public override Pair Multiply(Pair other)
            {
                Money money = (Money)other;
                Money result = new Money();
                int totalKopiyka1 = this.Hryvnia * 100 + this.Kopiyka;
                int totalKopiyka2 = money.Hryvnia * 100 + money.Kopiyka;
                int totalKopiykaResult = totalKopiyka1 * totalKopiyka2;
                result.Hryvnia = totalKopiykaResult / 10000;
                result.Kopiyka = totalKopiykaResult % 100;
                return result;
            }

            public override Pair Divide(Pair other)
            {
                Money money = (Money)other;
                Money result = new Money();
                int totalKopiyka1 = this.Hryvnia * 100 + this.Kopiyka;
                int totalKopiyka2 = money.Hryvnia * 100 + money.Kopiyka;
                int totalKopiykaResult = totalKopiyka1 / totalKopiyka2;
                result.Hryvnia = totalKopiykaResult / 100;
                result.Kopiyka = totalKopiykaResult % 100;
                return result;
            }
            public override string ToString()
            {
                return $"{Hryvnia}.{Kopiyka:00} UAH";
            }
        }

        
        class Fraction : Pair
        {
            public int Numerator { get; set; }
            public int Denominator { get; set; }

            public override Pair Add(Pair other)
            {
                Fraction fraction = (Fraction)other;
                Fraction result = new Fraction();
                result.Numerator = this.Numerator * fraction.Denominator + fraction.Numerator * this.Denominator;
                result.Denominator = this.Denominator * fraction.Denominator;
                return Simplify(result);
            }
            public override Pair Subtract(Pair other)
            {
                Fraction fraction = (Fraction)other;
                Fraction result = new Fraction();
                result.Numerator = this.Numerator * fraction.Denominator - fraction.Numerator * this.Denominator;
                result.Denominator = this.Denominator * fraction.Denominator;
                return Simplify(result);
            }

            public override Pair Multiply(Pair other)
            {
                Fraction fraction = (Fraction)other;
                Fraction result = new Fraction();
                result.Numerator = this.Numerator * fraction.Numerator;
                result.Denominator = this.Denominator * fraction.Denominator;
                return Simplify(result);
            }

            public override Pair Divide(Pair other)
            {
                Fraction fraction = (Fraction)other;
                Fraction result = new Fraction();
                result.Numerator = this.Numerator * fraction.Denominator;
                result.Denominator = this.Denominator * fraction.Numerator;
                return Simplify(result);
            }

          
            private Fraction Simplify(Fraction fraction)
            {
                int gcd = GCD(fraction.Numerator, fraction.Denominator);
                fraction.Numerator /= gcd;
                fraction.Denominator /= gcd;
                return fraction;
            }

            
            private int GCD(int a, int b)
            {
                if (b == 0)
                    return a;
                return GCD(b, a % b);
            }
            public override string ToString()
            {
                return $"{Numerator}/{Denominator}";
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int hryvnia1 = int.Parse(textBox1.Text);
            int kopiyka1 = int.Parse(textBox2.Text);
            int hryvnia2 = int.Parse(textBox3.Text);
            int kopiyka2 = int.Parse(textBox4.Text);
            int numerator1 = int.Parse(textBox5.Text);
            int denominator1 = int.Parse(textBox6.Text);
            int numerator2 = int.Parse(textBox7.Text);
            int denominator2 = int.Parse(textBox8.Text);


            Money money1 = new Money { Hryvnia = hryvnia1, Kopiyka = kopiyka1 };
            Money money2 = new Money { Hryvnia = hryvnia2, Kopiyka = kopiyka2 };
            Fraction fraction1 = new Fraction { Numerator = numerator1, Denominator = denominator1 };
            Fraction fraction2 = new Fraction { Numerator = numerator2, Denominator = denominator2 };


            Pair result1 = money1.Add(money2);
            Pair result2 = money1.Subtract(money2);
            Pair result3 = money1.Multiply(money2);
            Pair result4 = money1.Divide(money2);
            Pair result5 = fraction1.Add(fraction2);
            Pair result6 = fraction1.Subtract(fraction2);
            Pair result7 = fraction1.Multiply(fraction2);
            Pair result8 = fraction1.Divide(fraction2);

            textBox9.Text = result1.ToString();
            textBox10.Text = result2.ToString();
            textBox11.Text = result3.ToString();
            textBox12.Text = result4.ToString();
            textBox13.Text = result5.ToString();
            textBox14.Text = result6.ToString();
            textBox15.Text = result7.ToString();
            textBox16.Text = result8.ToString();
        }

      
    }
}