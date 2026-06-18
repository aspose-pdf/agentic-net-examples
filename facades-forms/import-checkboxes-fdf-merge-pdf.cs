using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string pdfPath   = "input.pdf";   // Existing PDF with form fields
        const string fdfPath   = "checkboxes.fdf"; // FDF containing checkbox definitions
        const string outputPath = "merged_output.pdf";

        // Verify that source files exist
        if (!File.Exists(pdfPath) || !File.Exists(fdfPath))
        {
            Console.Error.WriteLine("Required input files not found.");
            return;
        }

        // Form facade works on a source PDF and writes to a target PDF
        using (Form form = new Form(pdfPath, outputPath))
        {
            // Import field data from the FDF stream.
            // Existing fields with the same fully‑qualified name are updated;
            // new fields are added – no duplicate field objects are created.
            using (FileStream fdfStream = new FileStream(fdfPath, FileMode.Open, FileAccess.Read))
            {
                form.ImportFdf(fdfStream);
            }

            // Persist the changes to the output PDF.
            form.Save();
        }

        Console.WriteLine($"Merged PDF saved to '{outputPath}'.");
    }
}