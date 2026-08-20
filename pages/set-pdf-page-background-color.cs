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

        // Load the PDF document (document-disposal-with-using rule)
        using (Document doc = new Document(inputPath))
        {
            // Define the corporate brand color using RGB values
            // Example RGB: 30 (R), 144 (G), 255 (B)
            Aspose.Pdf.Color brandColor = Aspose.Pdf.Color.FromArgb(30, 144, 255);

            // Apply the background color to each page
            foreach (Page page in doc.Pages)
            {
                page.Background = brandColor;
            }

            // Save the updated PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with background color to '{outputPath}'.");
    }
}