using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";          // Existing PDF with form fields
        const string fdfPath = "checkboxes.fdf";    // FDF containing checkbox definitions
        const string outputPath = "merged_output.pdf";

        // Verify input files exist
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF file not found: {pdfPath}");
            return;
        }
        if (!File.Exists(fdfPath))
        {
            Console.Error.WriteLine($"FDF file not found: {fdfPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(pdfPath))
        {
            // Use the Facades.Form class (fully qualified via the using directive) to import FDF data.
            // This avoids the ambiguous reference with Aspose.Pdf.Forms.Form.
            using (Form form = new Form())
            {
                // Bind the Form facade to the loaded document.
                form.BindPdf(doc);

                // Import checkbox definitions and values from the FDF file.
                // ImportFdf updates existing fields and adds new ones.
                // Duplicate field names are automatically merged (existing fields are updated,
                // new fields are created), so no manual duplication handling is required.
                using (FileStream fdfStream = new FileStream(fdfPath, FileMode.Open, FileAccess.Read))
                {
                    form.ImportFdf(fdfStream);
                }
            }

            // Save the merged PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Merged PDF saved to '{outputPath}'.");
    }
}
