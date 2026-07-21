using System;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputPdf = "output.pdf";
        const string annotationContent = "Custom annotation text";

        // Create a minimal PDF so the file exists for the editor.
        using (Document seed = new Document())
        {
            seed.Pages.Add();
            seed.Save(inputPdf);
        }

        // Add a text (sticky‑note) annotation on page 1.
        using (PdfContentEditor editor = new PdfContentEditor())
        {
            editor.BindPdf(inputPdf);

            // System.Drawing.Rectangle: x, y, width, height.
            System.Drawing.Rectangle rect = new System.Drawing.Rectangle(100, 200, 150, 100);

            // Parameters: rectangle, title, contents, open flag, icon name, page number (1‑based).
            editor.CreateText(rect, "Note", annotationContent, false, "Note", 1);

            editor.Save(outputPdf);
        }

        Console.WriteLine($"Annotation added and saved to '{outputPdf}'.");
    }
}
