using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputPdf = "output_fade.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Load the PDF document inside a using block for proper disposal
        using (Document doc = new Document(inputPdf))
        {
            // Create a PdfPageEditor bound to the loaded document
            using (PdfPageEditor editor = new PdfPageEditor(doc))
            {
                // Set the same transition for all pages
                // DISSOLVE provides a fade‑out effect between pages
                editor.TransitionType = PdfPageEditor.DISSOLVE;
                // Duration is in seconds (integer value)
                editor.TransitionDuration = 2;

                // Apply the changes to the document
                editor.ApplyChanges();

                // Save the modified PDF
                editor.Save(outputPdf);
            }
        }

        Console.WriteLine($"PDF saved with Fade transition: '{outputPdf}'");
    }
}