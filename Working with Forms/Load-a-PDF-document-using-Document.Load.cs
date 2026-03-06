using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "sample.pdf";
        const string outputPath = "loaded_output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document using Document.LoadFrom (the standard load method)
        using (Document doc = new Document())
        {
            // LoadFrom requires a filename and LoadOptions; passing null uses default options
            doc.LoadFrom(inputPath, null);

            // Example operation: display the number of pages
            Console.WriteLine($"Document loaded. Page count: {doc.Pages.Count}");

            // Save the document to verify that loading succeeded
            doc.Save(outputPath);
        }

        Console.WriteLine($"Document saved to '{outputPath}'.");
    }
}