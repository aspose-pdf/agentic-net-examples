using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath  = "portfolio.pdf";
        const string outputPath = "flattened.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF portfolio
            using (Document doc = new Document(inputPath))
            {
                // Remove the collection (portfolio) if it exists
                if (doc.Collection != null)
                {
                    doc.Collection = null;
                }

                // Flatten interactive elements (forms, annotations, etc.)
                doc.Flatten();

                // Save as a regular PDF (no portfolio)
                doc.Save(outputPath);
            }

            Console.WriteLine($"Portfolio flattened and saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}