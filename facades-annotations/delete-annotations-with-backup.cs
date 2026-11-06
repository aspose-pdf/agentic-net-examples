using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";               // original PDF
        const string backupPath = "input_backup.pdf";        // backup copy
        const string outputPath = "output_no_annotations.pdf"; // result without annotations

        // Verify the source file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Source file not found: {inputPath}");
            return;
        }

        try
        {
            // ----- Step 1: Create a backup of the original PDF -----
            // Overwrite any existing backup to ensure the latest version is saved.
            File.Copy(inputPath, backupPath, overwrite: true);
            Console.WriteLine($"Backup created at '{backupPath}'.");

            // ----- Step 2: Load the PDF and delete all annotations -----
            // PdfFileEditor does not provide DeleteAnnotations; use Document instead.
            Document pdfDoc = new Document(inputPath);

            // Iterate through each page and clear its Annotations collection.
            foreach (Page page in pdfDoc.Pages)
            {
                page.Annotations.Clear();
            }

            // Save the modified PDF without annotations.
            pdfDoc.Save(outputPath);
            Console.WriteLine($"Annotations removed. Result saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
