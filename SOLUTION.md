## 1- Runtime error if user press enter (null reference exception)
    handled by making constructor that intilize the the field of the Sale class.
## 2- Runtime error (out of range exception)
    solving out of range exception in class InputParse in Join function, by replace 
    the fourth parameter (wordCount) with (wordCount - 3) so we only take the product name
    and escape first and last word (quantity and price) and 'at' word.
## 3- Runtime error (null reference exception)
    the case that handle that the user should type at least 4 word, is handled in a wrong way
    in class InputParse
            input must have at least 4 words ant it's was handled in a wrong way
            "if (wordCount > 4)" should replace by "if (wordCount < 4)"
## 4- Runtime error (null reference exception)
    the case when the format of the input isn't right we got null reference exception becasue 
    in Sale/Add function we access members of Saleline object which we got from InputParse/ProcessInput 
    which returned null if it was in invalid format
## 5- Logical error 
    in SaleLine class instead of adding import tax to the existing tax rate, the code replaces the tax rate
    entirely with 5%, as this may erase the 10% basic tax already applied to general items
    which are not books, medical items or food
## 6- user should type 'at' word before price
    in InputParser class, i handled that the input should contain 'at' word before price to correctly specify the price, 
    it's important for valid format, and without it the last word may be noticed that it's related to product name not price
## 7- handle case when user type a character in uppercase in word 'imported' by mistake
    for example we should treat 'Imported' like 'imported', in case if user typed it by mistake
## 8- handle case when user click multiple enters 
    this can cause unexpected result, it's handled in InputParse class
    eg. ['12', '','', '','at', '10'] should convert to ['12', '','', '','at', '10']
        which if it's not converted, empty strings will be treated as product, and it's  not product at all
## 9- calculated tax in better way
    in some cases tax could be wrong
    eg. if price = 10.501 and tax rate = 10, the provided code gives wrong result
        the tax should be 1.06 and it's 1.05, and the reason is that it's rounded first 
        before we get moduls of 0.05 and this could cause loosing 0.0001
## 10- check that the product contain book/books, chip/chips, tablet/tablets
    the original code check the word of the input as it's and it might written as
    Book, Chips or Tablet (in upper case) which may cause wrong result
## 11- formating in ToString() functions
    in Sale class and SaleLine class the original code format the number with floating point to one way, which 
    may differ from one country to another (Germany, Uk ...etc.) and it's important topic in dotnet 
    that should be handled correctly
## 12- adding Sale Level rounding instead of line level rounding as mentioned in the instructions and mention that in the receipt
## 13- apply principles like single reponsiplity and open for extension (SOLID) for readable, Maintainable, Reusable and Testable
    made in InputParser and SaleLine class
## 14- handle the case when the whole name of the input doesn't contain product name and contain (quantity, price, 'at', 'imported')
    in case if user typed by mistake "1 imported at 10" => here the product name wasn't set by user (he forgot that)
    so we should handle this case unless it counted in the receipt as "1 imported imported: 10' which is wrong
