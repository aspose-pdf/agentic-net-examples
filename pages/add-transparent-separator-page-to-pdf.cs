using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_with_separator.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the existing PDF inside a using block (document disposal rule)
        using (Document doc = new Document(inputPath))
        {
            // Insert an empty page at the desired position.
            // Here we add it at the end of the document; change the index if needed.
            Page separator = doc.Pages.Add();

            // Set the page background to transparent.
            // Aspose.Pdf.Color.Transparent provides a fully transparent color.
            separator.Background = Aspose.Pdf.Color.Transparent;

            // Save the modified PDF (save rule)
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with transparent separator page: '{outputPath}'");
    }
}