# DummyReflection
Custom Reflection For Getting Privated/Internalized Items in C#

# Use:

## Getting Properties and Fields

To get a field or property you have to use an instance of a type, everything in DummyReflection uses instances of Types to get privated items.

Let's start with something really simple, Fields/Properties will from here be referenced as "Variables", Variables define both a Field and Property!

So now let's use this example here:

      public class PrivateItems
      {
         private int I = 10;
      }

      //Make a new instance of a class\
      var pItems = new PrivateItems();

      //This returns a Variable, this contains information on the Field/Property\
      Variable vari = pItems.GetVariable("I");\

### Getting A Value

      //Returns a T
      vari.GetValue<T>();

      //Returns an Object
      vari.GetValue();

### Setting A Value
      //Takes A T to insure value
      vari.SetValue<T>(T val)

      //Takes an object for value
      vari.SetValue(object val);

#### You can get other information about the Variable as well

## Calling and Getting Methods
To get a method you will have to do the same thing as above but with GetMethod(string name), let's see\

      public class PrivateItems
      {
           private void CallMe()
           {
               Console.WriteLine("Hello World");
           }
  
           private void CallMe(string arg)
           {
               Console.WriteLine(arg);
           }
           
           private string GetHello()
           {
              return "Hello";
           }
    
      }

      var pItems = new PrivateItems();
      
      //Get A Method By Its Name, this will only work if there is no Overload for the method
      //var method = pItems.GetMethod("Call");

      //So how do we get the Specific Method?
      var method = pItems.GetMethod("Call", new Type[] { typeof(string) }).Call(new object[] { "ARG(s) HERE" });
      
      //Q: What do I do if the Method Returns a Value?
      //A: Good question, when calling a method there is two overloads for it, the first returns an object meaning the value of the return and the other returns a T type

      var retMethod = pItems.GetMethod("GetHello").Call<string>();
      //Alternative: var retMethod = (string)pItems.GetMethod("GetHello").Call();
     
### You can again get more info off of the Method Class

##Getting Privated Types

         //Luckily I tried making getting privated types very easy!
         //There is three overloads to FindType so let's go over them all
         
                                                             //This class is a Data class that builds constructors for you, use it only if you need to get a specific Constructor
         //Extension:         Load an Assembly     Type name in Assembly   Parameters(object[], Type[])                              
         1. Assembly FindType(this Assembly assem, string typeName, Parameters parameters = null)
         
         2. DummyReflect.FindType<TypeInAssembly>(string typeName, Parameters parameters = null)
         //This gets a Privated/Internal type by an existing Public type inside of an already Referenced Assembly!
         
         3. DummyReflect.FindType(object instance, string typeName, Parameters parameters = null)
         //This works like Num:2 but uses an instances of a class instead

## Getting an Attribute Method/Variable/Type
   
        public class NameMethod : Attribute
        {
            public string Name { get; set; }
        
            public NameMethod(string name)
            {
                 Name = name;
            }
        }
         
        //When you FindType, GetVariable, and or GetMethod you can use the method GetAttribute, let's see how to use it, this example works for the Methods/Variables/Types
        //instance.GetMethod("MethodName").GetAttribute(string typeNameOfAttribute, bool inherited, params Type[] constructor)
        //Use:
        FoundAttribute fa = instance.GetMethod("MethodName").GetAttribute("ATTRIBUTENAME", false, new Type[] { typeof(string) });
        
        //This will retreive the Attribute for the method with its actual value!
        string name = fa.GetValue<string>("Name");
        
         
         
         
         
         
         
