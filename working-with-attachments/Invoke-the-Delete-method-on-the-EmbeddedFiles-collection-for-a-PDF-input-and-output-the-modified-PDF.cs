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

        try
        {
            // Load the PDF document inside a using block for proper disposal
            using (Document doc = new Document(inputPath))
            {
                // Delete all embedded files from the document
                doc.EmbeddedFiles.Delete();

                // If you need to delete a specific file by name, use:
                // doc.EmbeddedFiles.Delete("filename.ext");

                // Save the modified PDF
                doc.Save(outputPath);
            }

            Console.WriteLine($"Embedded files removed. Saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}