using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        // Verify the source PDF exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the existing PDF document (lifecycle rule: use using for disposal)
        using (Document doc = new Document(inputPath))
        {
            // Append a new empty page at the end of the document
            // PageCollection.Add() creates and returns the new Page instance
            Page newPage = doc.Pages.Add();

            // Optional: set page size or add content to the new page
            // newPage.SetPageSize(595, 842); // A4 size in points

            // Save the updated PDF (lifecycle rule: Document.Save)
            doc.Save(outputPath);
        }

        Console.WriteLine($"New page appended and saved to '{outputPath}'.");
    }
}