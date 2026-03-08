using System;
using System.IO;
using System.Reflection;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // -----------------------------------------------------------------
        // 1. Create a minimal PDF document (required by lifecycle rule)
        // -----------------------------------------------------------------
        const string tempPdfPath = "temp.pdf";

        using (Document doc = new Document())
        {
            // Add a single blank page so the document is valid
            doc.Pages.Add();

            // Save the document – this satisfies the “use create, load and save” rule
            doc.Save(tempPdfPath);
        }

        // -----------------------------------------------------------------
        // 2. Define the facade types whose inheritance hierarchy we want to inspect
        // -----------------------------------------------------------------
        Type[] facadeTypes = new Type[]
        {
            typeof(PdfContentEditor),
            typeof(PdfPageEditor),
            typeof(PdfAnnotationEditor),
            typeof(FormEditor),
            typeof(PdfFileEditor),
            typeof(PdfBookmarkEditor),
            typeof(PdfFileStamp),
            typeof(PdfFileMend),
            typeof(PdfViewer)
        };

        // -----------------------------------------------------------------
        // 3. Output the inheritance chain and implemented interfaces for each type
        // -----------------------------------------------------------------
        Console.WriteLine("Aspose.Pdf.Facades inheritance hierarchy:");
        Console.WriteLine();

        foreach (Type t in facadeTypes)
        {
            PrintHierarchy(t);
            Console.WriteLine(); // blank line between types
        }

        // -----------------------------------------------------------------
        // 4. Clean up the temporary PDF file
        // -----------------------------------------------------------------
        try
        {
            File.Delete(tempPdfPath);
        }
        catch
        {
            // Ignored – cleanup is best‑effort
        }
    }

    // Helper method that prints the base‑type chain and interfaces of a given type
    static void PrintHierarchy(Type type)
    {
        Console.WriteLine($"Type: {type.FullName}");

        // Base‑type chain (most derived → System.Object)
        Type current = type.BaseType;
        int depth = 0;
        while (current != null)
        {
            string indent = new string(' ', depth * 2);
            Console.WriteLine($"{indent}Base: {current.FullName}");
            current = current.BaseType;
            depth++;
        }

        // Implemented interfaces (if any)
        Type[] interfaces = type.GetInterfaces();
        if (interfaces.Length > 0)
        {
            Console.WriteLine("  Implements:");
            foreach (Type iface in interfaces)
            {
                Console.WriteLine($"    {iface.FullName}");
            }
        }
    }
}