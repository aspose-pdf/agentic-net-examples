using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Paths for the original PDF, its backup, and the flattened result.
        const string sourcePath = "input.pdf";
        const string backupPath = "input_backup.pdf";
        const string outputPath = "flattened.pdf";

        BackupAndFlatten(sourcePath, backupPath, outputPath);
    }

    /// <summary>
    /// Creates a backup of the source PDF and then flattens all form fields using Aspose.Pdf.Facades.
    /// </summary>
    /// <param name="sourcePath">Path to the original PDF.</param>
    /// <param name="backupPath">Path where the backup will be stored.</param>
    /// <param name="outputPath">Path for the flattened PDF.</param>
    static void BackupAndFlatten(string sourcePath, string backupPath, string outputPath)
    {
        // Verify that the source file exists.
        if (!File.Exists(sourcePath))
        {
            Console.Error.WriteLine($"Source file not found: {sourcePath}");
            return;
        }

        // ----- Backup -----
        // Copy the original file to the backup location. Overwrite if a backup already exists.
        File.Copy(sourcePath, backupPath, overwrite: true);
        Console.WriteLine($"Backup created at: {backupPath}");

        // ----- Load and Flatten -----
        // Load the PDF document inside a using block to ensure proper disposal.
        using (Document doc = new Document(sourcePath))
        {
            // Use the Form facade (Aspose.Pdf.Facades) to flatten all form fields.
            // The Form object also implements IDisposable, so wrap it in a using block.
            using (Form form = new Form(doc))
            {
                form.FlattenAllFields(); // Removes all form fields and places their values directly on the page.
            }

            // Save the flattened document to the specified output path.
            doc.Save(outputPath);
            Console.WriteLine($"Flattened PDF saved to: {outputPath}");
        }
    }
}