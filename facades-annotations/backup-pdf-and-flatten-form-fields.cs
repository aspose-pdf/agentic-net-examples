using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Paths for the original PDF, its backup, and the flattened result
        const string inputPdf  = "input.pdf";
        const string backupPdf = "input_backup.pdf";
        const string outputPdf = "flattened.pdf";

        // Verify that the source PDF exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        try
        {
            // -----------------------------------------------------------------
            // Step 1: Create a backup copy of the original PDF to prevent data loss
            // -----------------------------------------------------------------
            // Overwrite any existing backup file
            File.Copy(inputPdf, backupPdf, overwrite: true);
            Console.WriteLine($"Backup created at '{backupPdf}'.");

            // -----------------------------------------------------------------
            // Step 2: Load the PDF using the Form facade (Aspose.Pdf.Facades)
            // -----------------------------------------------------------------
            // The Form constructor accepts a file path and loads the document
            using (Form form = new Form(inputPdf))
            {
                // -----------------------------------------------------------------
                // Step 3: Perform flattening – removes all form fields and makes
                //         their values part of the page content
                // -----------------------------------------------------------------
                form.FlattenAllFields();

                // -----------------------------------------------------------------
                // Step 4: Save the flattened PDF to a new file
                // -----------------------------------------------------------------
                form.Save(outputPdf);
            }

            Console.WriteLine($"Flattened PDF saved to '{outputPdf}'.");
        }
        catch (Exception ex)
        {
            // Handle any unexpected errors
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}