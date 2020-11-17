# DummyReflection
Custom Reflection For Getting Privated/Internalized Items in C#

# Use:

## Getting Properties and Fields

To get a field or property you have to use an instance of a type, everything in DummyReflection uses instances of Types to get privated items.

Let's start with something really simple, Fields/Properties will from here be referenced as "Variables", Variables define both a Field and Property!

So now let's use this example here:

public class PrivateItems\
{\
   private int I = 10;\
}\
\
//Make a new instance of a class\
var pItems = new PrivateItems();

//This returns a Variable, this contains information on the Field/Property\
Variable vari = pItems.GetVariable("I");\

### Getting A Value

//Returns a T\
vari.GetValue<T>();\
\
//Returns an Object\
vari.GetValue();

### Setting A Value
//Takes A T to insure value\
vari.SetValue<T>(T val)\
\
//Takes an object for value\
vari.SetValue(object val);\

#### You can get other information about the Variable as well

## Calling and Getting Methods
To get a method you will have to do the same thing as above but with GetMethod(string name), let's see\

public class PrivateItems\
{\
    private void CallMe()\
    {\
         Console.WriteLine("Called");\
    }\
    
    private void CallMe(string write)\
    {\
         Console.WriteLine(write);\
    }\
}\
