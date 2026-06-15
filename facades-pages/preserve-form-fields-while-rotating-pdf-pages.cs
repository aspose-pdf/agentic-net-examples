using System;
using System.IO;
using Aspose.Pdf;                     // Document, Rotation enum
using Aspose.Pdf.Facades;            // Form, PdfPageEditor

class PreserveFormFieldsWhileRotating
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

        // Load the source PDF inside a using block (lifecycle rule)
        using (Document pdfDoc = new Document(inputPath))
        {
            // -----------------------------------------------------------------
            // STEP 1 – Export existing form fields to a temporary stream.
            // -----------------------------------------------------------------
            MemoryStream fieldData = new MemoryStream();
            using (Form formExporter = new Form(pdfDoc))
            {
                // Export all AcroForm fields to FDF (could also use ExportJson)
                formExporter.ExportFdf(fieldData);
                fieldData.Position = 0; // reset for later import
            }

            // -----------------------------------------------------------------
            // STEP 2 – Rotate pages.  PdfPageEditor rotates the visual content.
            // -----------------------------------------------------------------
            using (PdfPageEditor pageEditor = new PdfPageEditor())
            {
                pageEditor.BindPdf(pdfDoc);          // bind the same Document instance
                pageEditor.Rotation = 90;            // rotate all pages 90 degrees clockwise
                pageEditor.ApplyChanges();           // commit the rotation
            }

            // -----------------------------------------------------------------
            // STEP 3 – Re‑import the previously exported form fields.
            // -----------------------------------------------------------------
            using (Form formImporter = new Form(pdfDoc))
            {
                // Import the field definitions back onto the rotated pages.
                formImporter.ImportFdf(fieldData);
            }

            // -----------------------------------------------------------------
            // STEP 4 – Save the resulting PDF.
            // -----------------------------------------------------------------
            pdfDoc.Save(outputPath);
        }

        Console.WriteLine($"Rotated PDF saved to '{outputPath}'. Form fields remain functional.");
    }
}