using Antlr4.Runtime.Tree;
using LCT.Analysis;
using LCT.Generation.Preparation.Intermediate;
using LCT.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LCT.Generation.Preparation
{
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
            foreach(var ld in context.list())
            {
                LctListVisitor visitor = new LctListVisitor();
                lists.AddOrReplace(visitor.Visit(ld));
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

            foreach(var element in context.ELEMENT())
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

        public override LCTList VisitListAutoList(LCTGrammarParser.ListAutoListContext context)
        {
            LCTList lctList = new LCTList();

            if (context.ELEMENT(0) != null && context.ELEMENT(1) != null)
            {
                decimal fromVal = 0m;
                decimal toVal = 0m;

                decimal.TryParse(context.ELEMENT(0).GetText(), out fromVal);
                {

                }
                
                if (decimal.TryParse(context.ELEMENT(1).GetText(), out toVal))
                {

                }

                for (decimal i = fromVal; i <= toVal; i++)
                {
                    lctList.Elements.Add(i);
                }
            }

            return lctList;
        }
    }
}
