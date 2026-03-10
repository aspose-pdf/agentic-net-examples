using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";   // source PDF
        const string outputPdf = "output.pdf";  // destination PDF

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // PdfContentEditor is a saveable facade – it implements IDisposable.
        using (PdfContentEditor editor = new PdfContentEditor())
        {
            // Bind the existing PDF file to the facade.
            editor.BindPdf(inputPdf);

            // Define the rectangle where the custom text will appear.
            // PdfContentEditor.CreateText expects a System.Drawing.Rectangle (x, y, width, height).
            // The original Aspose.Pdf.Rectangle uses (llx, lly, urx, ury).
            // Convert (100, 500, 300, 550) -> x=100, y=500, width=200, height=50.
            System.Drawing.Rectangle rect = new System.Drawing.Rectangle(100, 500, 200, 50);

            // Create a text annotation inside the rectangle.
            // Parameters: rectangle, text, font name, isBold, title, flags.
            editor.CreateText(rect, "Custom inserted text", "Helvetica", false, "MyText", 0);

            // Save the modified document via the facade.
            editor.Save(outputPdf);
        }

        Console.WriteLine($"Custom text added and saved to '{outputPdf}'.");
    }
}
