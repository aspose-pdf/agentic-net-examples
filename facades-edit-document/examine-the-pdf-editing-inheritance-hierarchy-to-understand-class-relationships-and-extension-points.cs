using System;
using System.IO;
using System.Reflection;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Optional: load a PDF to demonstrate facade binding (required by lifecycle rules)
        const string samplePath = "sample.pdf";
        if (File.Exists(samplePath))
        {
            // Document must be wrapped in a using block for deterministic disposal
            using (Document doc = new Document(samplePath))
            {
                // Bind the document to a facade to show typical usage
                using (PdfContentEditor editor = new PdfContentEditor())
                {
                    editor.BindPdf(doc);
                    // No modifications – just demonstrating binding
                }
            }
        }

        // List of facade types we want to inspect
        Type[] facadeTypes = new Type[]
        {
            typeof(Facade),
            typeof(SaveableFacade),
            typeof(PdfContentEditor),
            typeof(PdfPageEditor),
            typeof(PdfFileMend),
            typeof(PdfBookmarkEditor),
            typeof(PdfViewer),
            typeof(PdfFileSecurity),
            typeof(PdfFileEditor),
            typeof(PdfFileSignature)
        };

        Console.WriteLine("Aspose.Pdf.Facades inheritance hierarchy:");
        Console.WriteLine();

        foreach (Type t in facadeTypes)
        {
            PrintTypeInfo(t);
            Console.WriteLine(new string('-', 60));
        }
    }

    // Prints the inheritance chain and implemented interfaces for a given type
    static void PrintTypeInfo(Type type)
    {
        Console.WriteLine($"Type: {type.FullName}");

        // Build the inheritance chain (base types up to System.Object)
        Console.WriteLine("Inheritance chain:");
        Type current = type;
        while (current != null)
        {
            Console.WriteLine($"  -> {current.FullName}");
            current = current.BaseType;
        }

        // List directly implemented interfaces
        Type[] interfaces = type.GetInterfaces();
        if (interfaces.Length > 0)
        {
            Console.WriteLine("Implemented interfaces:");
            foreach (Type iface in interfaces)
            {
                Console.WriteLine($"  * {iface.FullName}");
            }
        }
        else
        {
            Console.WriteLine("Implemented interfaces: None");
        }
    }
}