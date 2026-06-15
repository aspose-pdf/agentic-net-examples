using System;
using System.IO;
using Aspose.Pdf;                     // Core API
using Aspose.Pdf.Text;                // For any text handling if needed

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";          // Existing PDF
        const string outputPath = "output_with_separator.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the existing PDF
        using (Document doc = new Document(inputPath))
        {
            // Add an empty page at the end of the document
            Page separator = doc.Pages.Add();

            // Set the page background to fully transparent
            separator.Background = Aspose.Pdf.Color.Transparent;

            // Save the modified document
            doc.Save(outputPath);
        }

        Console.WriteLine($"Separator page added. Saved to '{outputPath}'.");
    }
}