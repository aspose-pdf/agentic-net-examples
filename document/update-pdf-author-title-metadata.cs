using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF document inside a using block for deterministic disposal
            using (Document doc = new Document(inputPath))
            {
                // Read existing metadata
                string currentAuthor = doc.Info.Author;
                string currentTitle  = doc.Info.Title;
                Console.WriteLine($"Current Author: {currentAuthor}");
                Console.WriteLine($"Current Title : {currentTitle}");

                // Modify metadata fields
                doc.Info.Author = "New Author Name";
                doc.Info.Title  = "New Document Title";

                // Save the updated PDF (PDF format, no SaveOptions needed)
                doc.Save(outputPath);
            }

            Console.WriteLine($"Metadata updated and saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}