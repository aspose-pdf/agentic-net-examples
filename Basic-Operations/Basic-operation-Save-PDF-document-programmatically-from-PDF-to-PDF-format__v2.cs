using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text; // needed for TextAbsorber

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputPdf = "output.pdf";

        // Verify input file exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Not found: {inputPdf}");
            return;
        }

        try
        {
            // Load the PDF document inside a using block for deterministic disposal
            using (Document doc = new Document(inputPdf))
            {
                // Basic metadata output
                Console.WriteLine($"Pages: {doc.Pages.Count}");
                Console.WriteLine($"Author: {doc.Info.Author}");
                Console.WriteLine($"Title:  {doc.Info.Title}");

                // Extract text to demonstrate reading capabilities
                TextAbsorber absorber = new TextAbsorber();
                doc.Pages.Accept(absorber);
                Console.WriteLine($"Text characters: {absorber.Text.Length}");

                // Save the document back to PDF format
                doc.Save(outputPdf);
            }

            Console.WriteLine("Basic operations completed.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}