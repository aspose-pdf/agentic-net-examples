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

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Define corporate brand color (example RGB 34,120,200)
            // Aspose.Pdf.Color can be created directly from integer RGB values
            Color brandColor = Color.FromRgb(34, 120, 200);

            // Apply the background color to every page via the Page.Background property
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                doc.Pages[i].Background = brandColor;
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with corporate background color to '{outputPath}'.");
    }
}
