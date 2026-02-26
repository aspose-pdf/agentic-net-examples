using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Paths to the source PDF and the output PDF
        const string inputPdf  = "input.pdf";
        const string outputPdf = "output.pdf";

        // TeX formatted string that will appear in the figure annotation
        const string texContent = @"\frac{a}{b} = c";

        // Verify that the input file exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Create a PdfContentEditor instance and bind the source PDF
        PdfContentEditor editor = new PdfContentEditor();
        editor.BindPdf(inputPdf);

        // Define the annotation rectangle.
        // Use the fully‑qualified System.Drawing.Rectangle to avoid ambiguity with Aspose.Pdf.Rectangle.
        System.Drawing.Rectangle annotRect = new System.Drawing.Rectangle(100, 500, 200, 100);

        // Add a free‑text annotation containing the TeX string on page 1.
        // PdfContentEditor.CreateFreeText expects (Rectangle rect, string contents, int page).
        editor.CreateFreeText(annotRect, texContent, 1);

        // Save the modified document.
        editor.Save(outputPdf);

        Console.WriteLine($"Figure annotation with TeX content added and saved to '{outputPdf}'.");
    }
}