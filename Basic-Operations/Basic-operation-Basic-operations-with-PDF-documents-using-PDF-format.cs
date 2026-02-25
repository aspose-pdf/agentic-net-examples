using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Input and output PDF file paths
        const string inputPdf  = "input.pdf";
        const string outputPdf = "output.pdf";

        // Verify that the input file exists
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

                // Extract all text from the document using TextAbsorber
                TextAbsorber absorber = new TextAbsorber();
                doc.Pages.Accept(absorber);
                Console.WriteLine($"Text characters: {absorber.Text.Length}");

                // Save the (potentially modified) document to the output path
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