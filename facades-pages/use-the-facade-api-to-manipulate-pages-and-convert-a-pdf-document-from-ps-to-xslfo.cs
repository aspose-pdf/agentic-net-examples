using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string psInputPath = "input.ps";
        const string pdfOutputPath = "output.pdf";

        if (!File.Exists(psInputPath))
        {
            Console.Error.WriteLine($"File not found: {psInputPath}");
            return;
        }

        // Load the PostScript file (PS is input‑only; use PsLoadOptions)
        PsLoadOptions loadOptions = new PsLoadOptions();

        using (Document pdfDoc = new Document(psInputPath, loadOptions))
        {
            // Manipulate pages with the PdfPageEditor facade
            using (PdfPageEditor pageEditor = new PdfPageEditor())
            {
                pageEditor.BindPdf(pdfDoc);

                // Example modifications:
                // Rotate all pages 90 degrees
                pageEditor.Rotation = 90;
                // Zoom pages to 120%
                pageEditor.Zoom = 1.2f;

                // Apply the changes to the document
                pageEditor.ApplyChanges();

                // Save the edited document (PDF format)
                pageEditor.Save(pdfOutputPath);
            }

            // If additional processing is needed, the document can be saved again:
            // pdfDoc.Save(pdfOutputPath);
        }

        Console.WriteLine($"Processed PDF saved to '{pdfOutputPath}'.");
        // Note: Aspose.Pdf does not provide a way to save/export to XSL‑FO.
        // XSL‑FO is supported only as an input format (via XslFoLoadOptions).
    }
}