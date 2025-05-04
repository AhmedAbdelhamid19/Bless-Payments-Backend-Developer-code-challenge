#                                                                 Errors Solved
#                                                            _________________________

## 1- Runtime error if user press enter (null reference exception)
    handled by making constructor that intilize the the field of the Sale class
## 2- Runtime error (out of range exception)
    solving out of range exception in class InputParse in Join function, by replace 
    the fourth parameter (wordCount) with (wordCount - 3) so we only take the product name
    and escape first and last word (quantity and price) and 'at' word with refere to count and price
## 3- Runtime error (null reference exception)
    the case that handle that the user should type at least 4 word, is handled in a wrong way
    in class InputParse
            input must have at least 4 words <- was handled in a wrong way
            handle the case that the product should contain at lease 4 words 
            one to quantity, ont to description, one to 'at' word, and one to price
            "if (wordCount > 4)" should replace by "if (wordCount < 4)"

## 4- Runtime error (null reference exception)
    the case when the format of the input isn't right we got null reference exception becasue 
    in Sale/Add function we access members of saleline which we got from InputParse/ProcessInput which is null here
    due to first word or last word isn't number, count of word less than 4 (<qty> <des> 'at' <unit price>)
## 5- Logical error 
    instead of adding import tax to the existing tax rate, the code replaces the tax rate
    entirely with 5%, as this may erase the 10% basic tax already applied to general items
    which are not books, medical items or food
        this handled in SaleLine class 
