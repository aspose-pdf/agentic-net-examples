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
        const int rotationDegree = 90; // rotate all pages 90 degrees clockwise

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // ---------- Preserve form fields ----------
            // Export current form field data to a memory stream (FDF format)
            using (MemoryStream fdfStream = new MemoryStream())
            {
                Form form = new Form(doc);
                form.ExportFdf(fdfStream);          // capture field values
                fdfStream.Position = 0;             // reset for reading

                // ---------- Rotate pages ----------
                // Use PdfPageEditor (Facade) to rotate pages
                PdfPageEditor editor = new PdfPageEditor(doc);
                editor.Rotation = rotationDegree;    // apply same rotation to every page
                editor.ApplyChanges();               // commit rotation changes

                // ---------- Restore form fields ----------
                // Import the previously saved field data back into the rotated document
                form.ImportFdf(fdfStream);
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Rotated PDF saved to '{outputPath}'. Form fields remain intact.");
    }
}