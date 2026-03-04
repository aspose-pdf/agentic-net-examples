using System;
using System.Reflection;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Get the assembly that contains the Facade classes
        Assembly facadeAssembly = typeof(Facade).Assembly;

        // Retrieve all public types in the Aspose.Pdf.Facades namespace
        Type[] facadeTypes = facadeAssembly.GetTypes();
        foreach (Type type in facadeTypes)
        {
            if (type.IsPublic && type.Namespace == "Aspose.Pdf.Facades")
            {
                // Print the type name and its direct base type (if any)
                string baseName = type.BaseType != null ? type.BaseType.FullName : "None";
                Console.WriteLine($"{type.FullName} : inherits from {baseName}");
            }
        }
    }
}