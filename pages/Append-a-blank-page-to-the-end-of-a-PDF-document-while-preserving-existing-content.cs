using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the existing PDF document
        using (Document doc = new Document(inputPath))
        {
            // Append a blank page at the end of the document.
            // The Add() method creates an empty page using the most common page size.
            doc.Pages.Add();

            // Save the updated PDF preserving all original content.
            doc.Save(outputPath);
        }

        Console.WriteLine($"Blank page appended successfully. Saved to '{outputPath}'.");
    }
}