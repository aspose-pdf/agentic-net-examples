using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class RotatePdfPreserveForm
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputPdf = "rotated_output.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // 1. Export existing form fields to an in‑memory FDF stream.
        using (Form srcForm = new Form(inputPdf))
        using (MemoryStream fdfStream = new MemoryStream())
        {
            srcForm.ExportFdf(fdfStream);
            fdfStream.Position = 0; // reset for reading

            // 2. Rotate all pages 90° using PdfPageEditor (facade API).
            using (PdfPageEditor pageEditor = new PdfPageEditor())
            {
                pageEditor.BindPdf(inputPdf);
                // PdfPageEditor.Rotation expects an int (degrees). Cast the enum value.
                pageEditor.Rotation = (int)Aspose.Pdf.Rotation.on90;
                pageEditor.ApplyChanges();
                pageEditor.Save(outputPdf);
            }

            // 3. Import the previously exported form fields back into the rotated PDF.
            using (Form destForm = new Form(outputPdf))
            {
                destForm.ImportFdf(fdfStream);
                // Save overwrites the same file, preserving the rotated content plus fields.
                destForm.Save(outputPdf);
            }
        }

        Console.WriteLine($"Rotated PDF saved to '{outputPdf}' with form fields preserved.");
    }
}
