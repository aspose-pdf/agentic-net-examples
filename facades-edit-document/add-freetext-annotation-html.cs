using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

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

        // Load the PDF document inside a using block for proper disposal
        using (Document doc = new Document(inputPath))
        {
            // Initialize the content editor facade and bind the loaded document
            PdfContentEditor editor = new PdfContentEditor();
            editor.BindPdf(doc);

            // Define the annotation rectangle (x, y, width, height) using System.Drawing.Rectangle
            System.Drawing.Rectangle rect = new System.Drawing.Rectangle(100, 500, 300, 200);

            // HTML content to be displayed inside the free‑text annotation
            string htmlContent = "<b>Bold Text</b><br/><i>Italic Text</i><br/><u>Underlined Text</u>";

            // Create the free‑text annotation on page 1 (Aspose.Pdf uses 1‑based page indexing)
            editor.CreateFreeText(rect, htmlContent, 1);

            // Save the modified PDF
            editor.Save(outputPath);
        }

        Console.WriteLine($"Free‑text annotation with HTML added and saved to '{outputPath}'.");
    }
}