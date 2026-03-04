using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputCgm = "input.cgm";
        const string outputPdf = "rotated_output.pdf";

        if (!File.Exists(inputCgm))
        {
            Console.Error.WriteLine($"File not found: {inputCgm}");
            return;
        }

        try
        {
            // Load the CGM file into a PDF document (CGM is input‑only, so we convert it to PDF first)
            using (Document doc = new Document(inputCgm, new CgmLoadOptions()))
            {
                // Bind the document to PdfPageEditor facade
                using (PdfPageEditor editor = new PdfPageEditor())
                {
                    editor.BindPdf(doc);

                    // Rotate all pages 90 degrees clockwise (allowed values: 0, 90, 180, 270)
                    editor.Rotation = 90;

                    // Apply the rotation changes to the document
                    editor.ApplyChanges();
                }

                // Save the rotated PDF
                doc.Save(outputPdf);
            }

            Console.WriteLine($"Rotated PDF saved to '{outputPdf}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}