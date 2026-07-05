using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";          // Original PDF
        const string outputPdf = "flattened_output.pdf"; // Result after flattening

        // Verify input file exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // ---------- Backup the original PDF ----------
        // Create a backup file in the same folder with a "_backup" suffix.
        string backupPath = Path.Combine(
            Path.GetDirectoryName(inputPdf) ?? string.Empty,
            Path.GetFileNameWithoutExtension(inputPdf) + "_backup.pdf");

        try
        {
            // Overwrite any existing backup.
            File.Copy(inputPdf, backupPath, true);
            Console.WriteLine($"Backup created at: {backupPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Failed to create backup: {ex.Message}");
            return;
        }

        // ---------- Flatten the PDF ----------
        // Use the Form facade from Aspose.Pdf.Facades to flatten all form fields.
        try
        {
            using (Form form = new Form(inputPdf))
            {
                // Flatten all fields; values become part of the page content.
                form.FlattenAllFields();

                // Save the flattened document to the desired output path.
                form.Save(outputPdf);
            }

            Console.WriteLine($"Flattened PDF saved to: {outputPdf}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Flattening failed: {ex.Message}");
        }
    }
}