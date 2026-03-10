using System;
using System.IO;
using Aspose.Pdf.Facades;
using System.Drawing; // System.Drawing.Rectangle and System.Drawing.Color are required by PdfContentEditor

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputPdf = "output.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Use PdfContentEditor to add a text markup annotation
        using (PdfContentEditor editor = new PdfContentEditor())
        {
            // Load the existing PDF
            editor.BindPdf(inputPdf);

            // Define the annotation rectangle (left, top, width, height)
            // Fully qualified to avoid ambiguity with Aspose.Pdf.Rectangle
            System.Drawing.Rectangle rect = new System.Drawing.Rectangle(100, 500, 200, 20);

            // Add a highlight annotation (type 0) on page 1 with yellow color
            // Parameters: rect, contents, type (0=Highlight), page number (1‑based), color
            editor.CreateMarkup(rect, "Sample highlight", 0, 1, System.Drawing.Color.Yellow);

            // Save the modified PDF
            editor.Save(outputPdf);
        }

        Console.WriteLine($"Markup annotation added and saved to '{outputPdf}'.");
    }
}