using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string pdfPath   = "input.pdf";   // existing PDF with form fields
        const string outputPath = "merged_output.pdf"; // result PDF
        const string fdfPath   = "checkboxes.fdf"; // FDF containing checkbox definitions

        // Ensure source files exist
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF not found: {pdfPath}");
            return;
        }
        if (!File.Exists(fdfPath))
        {
            Console.Error.WriteLine($"FDF not found: {fdfPath}");
            return;
        }

        // Form facade handles both loading the PDF and saving the result.
        // It will import field values from the FDF into the existing PDF.
        // Matching fields are updated; fields absent in the PDF are ignored,
        // thus avoiding duplication of existing form fields.
        using (Form form = new Form(pdfPath, outputPath))
        {
            // Open the FDF stream and import its field data.
            using (FileStream fdfStream = new FileStream(fdfPath, FileMode.Open, FileAccess.Read))
            {
                form.ImportFdf(fdfStream);
            }

            // Persist the merged PDF.
            form.Save();
        }

        Console.WriteLine($"Merged PDF saved to '{outputPath}'.");
    }
}