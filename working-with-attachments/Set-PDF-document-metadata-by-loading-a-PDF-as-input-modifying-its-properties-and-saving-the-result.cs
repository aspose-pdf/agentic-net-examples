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
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF document inside a using block for proper disposal
            using (Document doc = new Document(inputPath))
            {
                // Modify standard metadata properties
                doc.Info.Title = "Sample PDF Title";
                doc.Info.Author = "John Doe";
                doc.Info.Subject = "Metadata Example";
                doc.Info.Keywords = "Aspose, PDF, metadata";
                doc.Info.Creator = "My Application";
                doc.Info.Producer = "Aspose.Pdf for .NET";
                doc.Info.CreationDate = DateTime.Now;
                doc.Info.ModDate = DateTime.Now;

                // Save the modified document as PDF
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