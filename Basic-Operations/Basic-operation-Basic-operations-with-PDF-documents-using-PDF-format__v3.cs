using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        // Verify the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF document (lifecycle: load)
            using (Document doc = new Document(inputPath))
            {
                // Basic information
                Console.WriteLine($"Pages: {doc.Pages.Count}");
                Console.WriteLine($"Author: {doc.Info.Author}");
                Console.WriteLine($"Title: {doc.Info.Title}");

                // Extract all text using TextAbsorber (correct API)
                TextAbsorber absorber = new TextAbsorber();
                doc.Pages.Accept(absorber);
                Console.WriteLine($"Extracted text length: {absorber.Text?.Length ?? 0}");

                // Save the document (lifecycle: save)
                doc.Save(outputPath);
            }

            Console.WriteLine($"Saved output PDF to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}