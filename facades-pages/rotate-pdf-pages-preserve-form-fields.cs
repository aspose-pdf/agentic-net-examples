using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class RotatePdfPreserveForm
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "rotated_output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the source PDF
        using (Document doc = new Document(inputPath))
        {
            // ---------- Preserve form fields ----------
            // Export all form fields to a temporary stream (FDF format)
            using (MemoryStream fdfStream = new MemoryStream())
            {
                // Bind the Form facade to the document and export fields
                using (Form formExport = new Form(doc))
                {
                    formExport.ExportFdf(fdfStream);
                }

                // ---------- Rotate pages ----------
                // Use PdfPageEditor (Facade) to rotate all pages 90 degrees clockwise
                using (PdfPageEditor pageEditor = new PdfPageEditor())
                {
                    pageEditor.BindPdf(doc);
                    pageEditor.Rotation = 90; // valid values: 0, 90, 180, 270
                    pageEditor.ApplyChanges(); // apply rotation to the document
                }

                // ---------- Re‑import form fields ----------
                // Reset stream position before reading
                fdfStream.Position = 0;
                using (Form formImport = new Form(doc))
                {
                    formImport.ImportFdf(fdfStream);
                }

                // ---------- Save the rotated PDF ----------
                doc.Save(outputPath);
            }
        }

        Console.WriteLine($"Rotated PDF saved to '{outputPath}'. Form fields were preserved.");
    }
}