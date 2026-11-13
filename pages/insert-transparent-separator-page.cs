using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_with_separator.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the existing PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Add a new empty page at the end of the document
            Page separatorPage = doc.Pages.Add();

            // Set the page background to transparent (correct property is Page.Background)
            separatorPage.Background = Color.Transparent;

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Separator page added. Saved to '{outputPath}'.");
    }
}
