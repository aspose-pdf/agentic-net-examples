using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

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

        // Use PdfContentEditor to add a web link on page 2
        using (PdfContentEditor editor = new PdfContentEditor())
        {
            // Bind the existing PDF document
            editor.BindPdf(inputPath);

            // Define the clickable rectangle (x, y, width, height)
            System.Drawing.Rectangle rect = new System.Drawing.Rectangle(100, 500, 200, 50);

            // Create a web link that opens the specified URL when clicked on page 2
            editor.CreateWebLink(rect, "https://example.com", 2);

            // Save the modified PDF
            editor.Save(outputPath);
        }

        Console.WriteLine($"Hyperlink added and saved to '{outputPath}'.");
    }
}