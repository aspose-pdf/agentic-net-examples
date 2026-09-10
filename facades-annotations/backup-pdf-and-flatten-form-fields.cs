using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Paths for the original, backup, and flattened PDF files
        const string inputPath  = "input.pdf";
        const string backupPath = "input_backup.pdf";
        const string outputPath = "flattened.pdf";

        // Verify that the source PDF exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the original PDF document (lifecycle rule: use using)
            using (Document doc = new Document(inputPath))
            {
                // ---------- Backup ----------
                // Save a copy of the original PDF before any modifications
                doc.Save(backupPath);
                Console.WriteLine($"Backup created at '{backupPath}'.");

                // ---------- Flatten ----------
                // Use Aspose.Pdf.Facades.Form to flatten all form fields.
                // The Form facade works on the same Document instance.
                using (Form form = new Form(doc))
                {
                    form.FlattenAllFields(); // Removes all interactive fields
                }

                // ---------- Save flattened PDF ----------
                // Save the modified document to the desired output location
                doc.Save(outputPath);
                Console.WriteLine($"Flattened PDF saved at '{outputPath}'.");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}