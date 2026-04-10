using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";          // Existing PDF with form fields
        const string fdfPath = "checkboxes.fdf";     // FDF containing checkbox definitions/values
        const string outputPath = "merged_output.pdf";

        // Verify that both source files exist
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

        // Create a Form facade bound to the source PDF and specify the output file.
        // The Form class implements SaveableFacade, so we can use a using block for deterministic disposal.
        using (Form form = new Form(pdfPath, outputPath))
        {
            // Import the FDF data. This will add/overwrite field values.
            // If a field already exists in the PDF, its value will be updated;
            // if it does not exist, the field definition will be created.
            using (FileStream fdfStream = new FileStream(fdfPath, FileMode.Open, FileAccess.Read))
            {
                form.ImportFdf(fdfStream);
            }

            // Save the merged PDF. No explicit PreSave call is required.
            form.Save();
        }

        Console.WriteLine($"Merged PDF saved to '{outputPath}'.");
    }
}