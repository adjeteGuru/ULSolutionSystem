# ULSolutionSystem

# Problem-Solving Approach Summary
The Evaluate method processes a mathematical expression string and computes its result by following these steps:

## Validation: The expression is first validated using ExpressionHandlerExtension.ExpressionValidator to ensure it 
does not contain negative numbers or parentheses. If the expression is invalid, an ArgumentException is thrown. 
This choice ensures that only valid expressions are processed, preventing errors during evaluation.
## Tokenization: The expression is tokenized into numbers and operators using ExpressionHandlerExtension.TokenFormatter.
This step simplifies the parsing process by breaking down the expression into manageable parts.
## Stacks Initialization: Two stacks are initialized:
### valuesRecorded for storing numerical values.
### operatorsRecorded for storing operators.Using stacks allows for efficient handling of operator precedence and evaluation order.
## Token Processing:
### Numbers: If a token is a number, it is pushed onto the valuesRecorded stack.
### Operators: If a token is an operator, the method checks the precedence of the operators. Operators with higher
or equal precedence are applied to the top values from the valuesRecorded stack, and the result is pushed 
back onto the stack. The current operator is then pushed onto the operatorsRecorded stack.
This approach ensures that operators are applied in the correct order according to their precedence.
## Final Computation: After processing all tokens, any remaining operators are applied to the values in the 
valuesRecorded stack until the stack is empty.This step ensures that all operations are completed.
## Result: The final result is the last value remaining in the valuesRecorded stack, which is returned as the output.
This choice ensures that the method returns the correct result of the evaluated expression.
## Error Handling
### Stack Empty Check: Before performing operations, the method checks if there are enough values in the stack.
 If not, it throws a custom ExpressionException with a meaningful error message. 
 This ensures that the method handles invalid expressions gracefully and provides clear feedback.
By using stacks and a systematic approach to handle operator precedence, the method efficiently evaluates
mathematical expressions without relying on third-party libraries.
 
This design ensures robustness and clarity in handling various edge cases and also complied to SOLID principles. 
