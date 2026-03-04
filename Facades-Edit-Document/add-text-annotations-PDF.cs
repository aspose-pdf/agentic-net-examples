using System;
using System.Drawing;               // System.Drawing.Rectangle is required by PdfContentEditor
using Aspose.Pdf.Facades;          // Facade classes for annotation handling

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";          // source PDF
        const string outputPdf = "annotated_output.pdf"; // result PDF

        // Verify input file exists
        if (!System.IO.File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        try
        {
            // Initialize the facade and bind the PDF document
            using (PdfContentEditor editor = new PdfContentEditor())
            {
                editor.BindPdf(inputPdf);

                // Define annotation rectangle (x, y, width, height) in points
                Rectangle rect = new Rectangle(100, 500, 200, 100);

                // Create a text (sticky‑note) annotation on page 1
                // title   – annotation title shown in the popup
                // contents– main text of the annotation
                // open    – true to display the popup open by default
                // icon    – one of the supported icons ("Comment", "Key", "Note", etc.)
                // page    – 1‑based page number
                editor.CreateText(
                    rect,
                    "Review Note",               // title
                    "Please verify the figures on this page.", // contents
                    true,                        // open
                    "Note",                      // icon
                    1);                          // page number (1‑based)

                // Save the modified PDF
                editor.Save(outputPdf);
            }

            Console.WriteLine($"Text annotation added successfully. Output saved to '{outputPdf}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}