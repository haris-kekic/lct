using Antlr4.Runtime.Tree;
using LCT.Analysis;
using LCT.Generation.Preparation.Intermediate;
using LCT.Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LCT.Generation.Preparation
{
    //TODO: Handle Errors
    internal class StatementVisitor : LCTGrammarBaseVisitor<Statement>
    {
        public override Statement VisitListShowStatement(LCTGrammarParser.ListShowStatementContext context)
        {
            Statement statement = new Statement();
            statement.ListsShow = context.Accept<ListsShow>(new ListsShowVisitor());
            return statement;
        }

        public override Statement VisitListDefinitionsStatement(LCTGrammarParser.ListDefinitionsStatementContext context)
        {
            Statement statement = new Statement();
            statement.ListDefinitions = context.Accept<LctUniqueList>(new ListDefinitionsVisitor());
            return statement;
        }

        public override Statement VisitListComprehensionStatement(LCTGrammarParser.ListComprehensionStatementContext context)
        {
            Statement statement = new Statement();
            statement.ListComprehension = context.Accept<ListComprehension>(new ListComprehensionVisitor());
            return statement;
        }
    }

    internal class ListComprehensionVisitor : LCTGrammarBaseVisitor<ListComprehension>
    {
        public override ListComprehension VisitListComprehensionStatement(LCTGrammarParser.ListComprehensionStatementContext context)
        {
            ListComprehension comprehension = new ListComprehension();
            comprehension.ListDefinitions = new ListDefinitionsVisitor().Visit(context.listDefinitions());
            comprehension.ArithmeticExpresssionContext = context.listArithExpression();
            return comprehension;
        }
    }

    public class ArithmeticCalculationVisitor : LCTGrammarBaseVisitor<decimal>
    {
        public Dictionary<string, decimal> Variables { get; set; }

        public ArithmeticCalculationVisitor()
            : base()
        {
            this.Variables = new Dictionary<string, decimal>();
        }

        public ArithmeticCalculationVisitor(Dictionary<string, decimal> variables)
            : base()
        {
            this.Variables = variables;
        }

        public override decimal VisitListArithExpression(LCTGrammarParser.ListArithExpressionContext context)
        {
            return base.Visit(context.arithExpression());
        }

        public override decimal VisitPower(LCTGrammarParser.PowerContext context)
        {
            return (decimal)Math.Pow((double)base.Visit(context.arithExpression(0)), (double)base.Visit(context.arithExpression(1)));
        }

        public override decimal VisitMulDiv(LCTGrammarParser.MulDivContext context)
        {
            if (context.MUL() != null)
            {
                return base.Visit(context.arithExpression(0)) * base.Visit(context.arithExpression(1));
            }

            else
            {
                return base.Visit(context.arithExpression(0)) / base.Visit(context.arithExpression(1));
            }
        }

        public override decimal VisitAddSub(LCTGrammarParser.AddSubContext context)
        {
            if (context.ADD() != null)
            {
                return base.Visit(context.arithExpression(0)) + base.Visit(context.arithExpression(1));
            }

            else
            {
                return base.Visit(context.arithExpression(0)) - base.Visit(context.arithExpression(1));
            }
        }

        public override decimal VisitPar(LCTGrammarParser.ParContext context)
        {
            return base.Visit(context.arithExpression());
        }

        public override decimal VisitNum(LCTGrammarParser.NumContext context)
        {
            decimal result = 0m;
            decimal.TryParse(context.NUMBER().GetText(), out result);

            return result;
        }

        public override decimal VisitVar(LCTGrammarParser.VarContext context)
        {
            string varName = context.IDENTIFIER().GetText();
            decimal result = 0m;

            if (this.Variables.ContainsKey(varName))
            {
                result = this.Variables[varName];
            }

            return result;
        }

    }

    internal class ListsShowVisitor : LCTGrammarBaseVisitor<ListsShow>
    {
        public override ListsShow VisitListShowStatement(LCTGrammarParser.ListShowStatementContext context)
        {
            ListsShow listsShow = new ListsShow();
            listsShow.Parameters = null; //TODO: Possible parameters for the show command
            return listsShow;
        }
    }

    internal class ListDefinitionsVisitor : LCTGrammarBaseVisitor<LctUniqueList>
    {
        public override LctUniqueList VisitListDefinitions(LCTGrammarParser.ListDefinitionsContext context)
        {
            LctUniqueList lists = new LctUniqueList();
            foreach (var ld in context.list())
            {
                lists.AddOrReplace(new LctListVisitor().Visit(ld));
            }
            return lists;
        }
    }

    internal class LctListVisitor : LCTGrammarBaseVisitor<LCTList>
    {
        public override LCTList VisitList(LCTGrammarParser.ListContext context)
        {
            LCTList lctList = this.Visit(context.listElements());
            lctList.Name = context.IDENTIFIER(0).GetText();
            return lctList;
        }

        public override LCTList VisitListElements(LCTGrammarParser.ListElementsContext context)
        {
            if (context.listManualList() != null)
                return this.Visit(context.listManualList());
            else
                return this.Visit(context.listAutoList());
        }

        public override LCTList VisitListManualList(LCTGrammarParser.ListManualListContext context)
        {
            LCTList lctList = new LCTList();

            foreach (var element in context.ELEMENT())
            {
                decimal decVal = 0m;
                if (decimal.TryParse(element.GetText(), out decVal))
                {
                    lctList.Elements.Add(decVal);
                }
                else
                {
                    lctList.Elements.Add(element.GetText());
                }
            }

            return lctList;
        }

        public override LCTList VisitAutoLimitedList(LCTGrammarParser.AutoLimitedListContext context)
        {
            //TODO: Handle Errors
            LCTList lctList = new LCTList();

            decimal fromVal = 0m;
            decimal toVal = 0m;
            decimal.TryParse(context.ELEMENT(0).GetText(), out fromVal);
            decimal.TryParse(context.ELEMENT(1).GetText(), out toVal);

            for (decimal i = fromVal; i <= toVal; i++)
            {
                lctList.Elements.Add(i);
            }

            return lctList;
        }

        public override LCTList VisitAutoLeftLimited(LCTGrammarParser.AutoLeftLimitedContext context)
        {
            //TODO: Handle Errors
            LCTList lctList = new LCTList();

            decimal fromVal = 0m;
            decimal.TryParse(context.ELEMENT().GetText(), out fromVal);

            for (decimal i = fromVal; i <= Int16.MaxValue; i++)
            {
                lctList.Elements.Add(i);
            }

            return lctList;
        }


        public override LCTList VisitAutoRightLimited(LCTGrammarParser.AutoRightLimitedContext context)
        {
            //TODO: Handle Errors
            LCTList lctList = new LCTList();

            decimal toVal = 0m;
            decimal.TryParse(context.ELEMENT().GetText(), out toVal);

            for (decimal i = Int16.MinValue; i <= toVal; i++)
            {
                lctList.Elements.Add(i);
            }

            return lctList;
        }
    }
}
