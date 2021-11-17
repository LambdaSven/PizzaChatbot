using System;
using Xunit;
using OrderBot;
using PizzaBot.Interpretation;
using System.Collections.Generic;
using System.Linq;

namespace OrderBot.tests
{
    [Collection("Tests of the Lexing System")]
    public class LexerTests
    {
        [Fact(DisplayName="Perform Basic Lexical Analysis")]
        public void SimpleTest()
        {
          List<Token> test = new List<Token>();
          // 1 pizza with mushrooms
          test.Add(new Token(TokenType.NUMBER, "1"));          
          test.Add(new Token(TokenType.GRAMMAR, "pizza"));          
          test.Add(new Token(TokenType.GRAMMAR, "with"));          
          test.Add(new Token(TokenType.TOPPING, "mushrooms"));          

          List<Token> input = Lexer.scan("1 pizza with mushrooms");

          Assert.True(test.SequenceEqual(input));
        }
        [Fact(DisplayName="Ensure that input casing is irrelevant in lexing")]
        public void casingTest()
        {
          List<Token> test = new List<Token>();
          test.Add(new Token(TokenType.NUMBER, "1"));          
          test.Add(new Token(TokenType.GRAMMAR, "pizza"));          
          test.Add(new Token(TokenType.GRAMMAR, "with"));          
          test.Add(new Token(TokenType.TOPPING, "mushrooms"));          

          List<Token> input = Lexer.scan("1 Pizza WiTh MUSHrooMS");

          Assert.True(test.SequenceEqual(input));
        }

        [Fact(DisplayName = "Ensure that unkown characters get treated appropriately")]
        public void UnknownTest()
        {
          List<Token> test = new List<Token>();

          test.Add(new Token(TokenType.UNKNOWN, "one"));          
          test.Add(new Token(TokenType.GRAMMAR, "pizza"));          
          test.Add(new Token(TokenType.GRAMMAR, "with"));          
          test.Add(new Token(TokenType.TOPPING, "mushrooms"));          
          test.Add(new Token(TokenType.UNKNOWN, "aa"));          
          test.Add(new Token(TokenType.UNKNOWN, ";iqj;"));          

          List<Token> input = Lexer.scan("one pizza with mushrooms aa ;iqj;");

          Assert.True(test.SequenceEqual(input));
        }
        [Fact(DisplayName = "Lex Pizza with size")]
        public void SizeTest()
        {
          List<Token> test = new List<Token>();

          test.Add(new Token(TokenType.NUMBER, "2"));
          test.Add(new Token(TokenType.SIZE, "large"));    
          test.Add(new Token(TokenType.PIZZA, "hawaiian"));          
          test.Add(new Token(TokenType.GRAMMAR, "pizzas"));          
          test.Add(new Token(TokenType.GRAMMAR, "without"));          
          test.Add(new Token(TokenType.TOPPING, "pineapple"));          
                

          List<Token> input = Lexer.scan("2 large hawaiian pizzas without pineapple");

          Assert.True(test.SequenceEqual(input));
        }
        [Fact(DisplayName = "Test Excess Spaces")]
        public void ExcessSpace()
        {
          List<Token> test = new List<Token>();

          test.Add(new Token(TokenType.NUMBER, "1"));          
          test.Add(new Token(TokenType.GRAMMAR, "pizza"));          
          test.Add(new Token(TokenType.GRAMMAR, "with"));          
          test.Add(new Token(TokenType.TOPPING, "mushrooms"));          

          List<Token> input = Lexer.scan("  1   pizza   with  mushrooms  ");

          Assert.True(test.SequenceEqual(input));
        }
    }
}