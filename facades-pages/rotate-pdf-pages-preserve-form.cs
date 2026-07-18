using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class RotatePdfPreserveForm
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "rotated_preserved.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the source PDF
        using (Document doc = new Document(inputPath))
        {
            // ------------------------------------------------------------
            // 1. Export existing form fields to an FDF stream (preserves data)
            // ------------------------------------------------------------
            MemoryStream fdfStream = new MemoryStream();
            Form formExporter = new Form(doc);
            formExporter.ExportFdf(fdfStream);
            fdfStream.Position = 0; // reset for later import

            // ------------------------------------------------------------
            // 2. Rotate all pages (90 degrees clockwise) using PdfPageEditor
            // ------------------------------------------------------------
            PdfPageEditor pageEditor = new PdfPageEditor();
            pageEditor.BindPdf(doc);
            pageEditor.Rotation = 90; // valid values: 0, 90, 180, 270
            pageEditor.ApplyChanges();

            // ------------------------------------------------------------
            // 3. Re‑import the previously exported form fields so they stay
            //    attached to the (now rotated) pages.
            // ------------------------------------------------------------
            Form formImporter = new Form(doc);
            formImporter.ImportFdf(fdfStream);

            // ------------------------------------------------------------
            // 4. Save the resulting PDF
            // ------------------------------------------------------------
            doc.Save(outputPath);
        }

        Console.WriteLine($"Rotated PDF saved to '{outputPath}' with form data preserved.");
    }
}