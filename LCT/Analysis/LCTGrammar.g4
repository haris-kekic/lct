grammar LCTGrammar;

/*
 * Parser Rules (Syntax Rules or Syntactic Strucuture)
 */

statement
	: listDefinitionsStatement 
	| listComprehensionStatement
	| listShowStatement;

listDefinitionsStatement 
: DEF listDefinitions 
;


listComprehensionStatement 
: LIST_BEGIN listArithExpression PIPE listDefinitions  (COMMA listLogicExpression)* LIST_END
;

listShowStatement 
: SHOW
;

listDefinitions : list (COMMA list)*;

listArithExpression : arithExpression;

listLogicExpression : logicExpression;

arithExpression : arithExpression POW<assoc=right> arithExpression #Power	
				| arithExpression ( MUL | DIV ) arithExpression			 #MulDiv	    
				| arithExpression  ( ADD | SUB ) arithExpression	     #AddSub		    
				| IDENTIFIER										     #Var
				| NUMBER												 #Num														
				| LP arithExpression RP								     #Par	
				;

logicExpression : arithExpression ( EQ | GT | GTE | LT | LTE ) arithExpression;
 
list
: IDENTIFIER ELEMENT_OF (listElements | referencedList=IDENTIFIER)
;

listElements 
: LIST_BEGIN (listManualList | listAutoList)? LIST_END
;

listManualList
: ELEMENT? (COMMA ELEMENT)*
;

listAutoList
: 
	ELEMENT SPAN ELEMENT	#AutoLimitedList
	| ELEMENT SPAN			#AutoLeftLimited
	| SPAN ELEMENT			#AutoRightLimited
;

/*
 * Lexer Rules (Symbol or Token rules)
 */
 POW : '^' ;

 LP : '(' ;

 RP : ')';

 ADD : '+';

 SUB : '-';

 MUL : '*';

 DIV : '/';

 EQ : '=';

 GT : '>';

 GTE : '>=';

 LT : '<';

 LTE : '<';

 SPAN
 : '..'
 ;

 COMMA
 : ','
 ;

 PIPE
 : '|'
 ;

 LIST_BEGIN
 : '['
 ;

 LIST_END
 : ']'
 ;

 ELEMENT :
  NUMBER;

 NUMBER
 : DIGIT+ ('.' DIGIT+)?
 ;

 ELEMENT_OF
 : '<-'
 ;

 DEF
 : 'def'
 ;

 SHOW
 : 'show'
 ;

 IDENTIFIER 
	: LETTER (LETTER|DIGIT)* 
	;

 fragment
LETTER
	:  [a-zA-Z]
	;
fragment
DIGIT
	: [0-9]
	;

WS
	: [ \t\r\n]+ -> skip 
	;
