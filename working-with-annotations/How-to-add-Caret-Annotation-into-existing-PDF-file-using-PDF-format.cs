using System;
using System.Drawing; // System.Drawing types are required by PdfContentEditor methods
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputPdf = "output_caret.pdf";

        // Ensure the source file exists
        if (!System.IO.File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        try
        {
            // Initialize the content editor facade
            using (PdfContentEditor editor = new PdfContentEditor())
            {
                // Bind the existing PDF document
                editor.BindPdf(inputPdf);

                // Define the annotation rectangle (position and size on the page)
                // Fully qualified System.Drawing.Rectangle to avoid ambiguity
                System.Drawing.Rectangle annotRect = new System.Drawing.Rectangle(100, 500, 150, 100);

                // Define the caret rectangle (the actual caret shape)
                System.Drawing.Rectangle caretRect = new System.Drawing.Rectangle(110, 510, 130, 80);

                // Create the caret annotation on page 1
                // Parameters: page number, annotation rect, caret rect, symbol, contents, color
                editor.CreateCaret(
                    page: 1,
                    annotRect: annotRect,
                    caretRect: caretRect,
                    symbol: "None",               // "P" for paragraph, "None" for no symbol
                    annotContents: "Sample caret annotation",
                    color: System.Drawing.Color.Blue);

                // Save the modified PDF
                editor.Save(outputPdf);
            }

            Console.WriteLine($"Caret annotation added successfully. Output saved to '{outputPdf}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}