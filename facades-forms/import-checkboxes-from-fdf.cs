using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string pdfPath   = "input.pdf";          // Existing PDF with form fields
        const string fdfPath   = "checkboxes.fdf";    // FDF containing checkbox definitions/values
        const string outputPath = "merged_output.pdf"; // Resulting PDF

        // Verify source files exist
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

        // Initialize the Form facade with source PDF and target PDF
        Form form = new Form(pdfPath, outputPath);

        // Import checkbox definitions (and their values) from the FDF stream.
        // ImportFdf merges the incoming fields with existing ones without creating duplicates.
        using (FileStream fdfStream = new FileStream(fdfPath, FileMode.Open, FileAccess.Read))
        {
            form.ImportFdf(fdfStream);
        }

        // Persist the merged document.
        form.Save();

        // Release resources held by the facade.
        form.Close();

        Console.WriteLine($"Merged PDF saved to '{outputPath}'.");
    }
}