using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputCgm = "input.cgm";
        const string outputPdf = "output.pdf";

        // Verify the CGM input file exists
        if (!File.Exists(inputCgm))
        {
            Console.Error.WriteLine($"File not found: {inputCgm}");
            return;
        }

        // Load the CGM file (input‑only format) as a PDF Document
        using (Document doc = new Document(inputCgm, new CgmLoadOptions()))
        {
            // Use the PdfPageEditor facade to modify page properties
            using (PdfPageEditor editor = new PdfPageEditor(doc))
            {
                // Rotate all pages 90 degrees clockwise
                editor.Rotation = 90;

                // Set zoom factor (1.0 = 100%)
                editor.Zoom = 0.8f; // 80% zoom

                // Change the output page size to A4
                editor.PageSize = PageSize.A4;

                // Apply the changes to the underlying document
                editor.ApplyChanges();
            }

            // Save the modified document as a PDF file
            doc.Save(outputPdf);
        }

        Console.WriteLine($"CGM processed and saved to '{outputPdf}'.");
    }
}