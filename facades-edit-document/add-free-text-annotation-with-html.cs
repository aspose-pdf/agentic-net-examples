using System;
using System.IO;
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

        // Bind the existing PDF using the Facades API
        using (PdfContentEditor editor = new PdfContentEditor())
        {
            editor.BindPdf(inputPath);

            // Define the annotation rectangle (System.Drawing.Rectangle is required by the API)
            System.Drawing.Rectangle rect = new System.Drawing.Rectangle(100, 500, 300, 200);

            // HTML content to be displayed inside the free‑text annotation
            string htmlContent = "<b>Bold Text</b><br/><i>Italic Text</i><br/><u>Underlined Text</u>";

            // Create a free‑text annotation on page 1 with the HTML string as its contents
            editor.CreateFreeText(rect, htmlContent, 1);

            // Save the modified PDF
            editor.Save(outputPath);
        }

        Console.WriteLine($"Free‑text annotation with HTML saved to '{outputPath}'.");
    }
}