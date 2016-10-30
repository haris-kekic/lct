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
: LIST_BEGIN listArithExpression PIPE listDefinitions listLogicExpression  LIST_END
;

listShowStatement 
: SHOW
;

listDefinitions : list (COMMA list)*;

listArithExpression : arithExpression;

listLogicExpression : (COMMA logicOperation)*;

arithExpression : arithExpression POW<assoc=right> arithExpression #Power	
				| arithExpression ( MUL | DIV ) arithExpression			 #MulDiv	    
				| arithExpression  ( ADD | SUB ) arithExpression	     #AddSub
				| NUMBER												 #Num		    
				| IDENTIFIER										     #Var													
				| LP arithExpression RP								     #Par	
				;

logicOperation : IDENTIFIER ( EQ | GT | GTE | LT | LTE ) NUMBER;
 
list
: IDENTIFIER ELEMENT_OF (listElements | referencedList=IDENTIFIER)
;

listElements 
: LIST_BEGIN (listManualList | listAutoList)? LIST_END
;

listManualList
: NUMBER (COMMA NUMBER)*
;

listAutoList
: 
	NUMBER SPAN NUMBER	#AutoLimitedList
	| NUMBER SPAN			#AutoLeftLimited
	| SPAN NUMBER			#AutoRightLimited
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

 LTE : '<=';

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

 

 /*
 * Will be extended to other types as well
 */
 NUMBER
 : DIGIT+ ('.' DIGIT+)?
 ; 

 STRING : '"' ('""'|~'"')* '"' ;

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
