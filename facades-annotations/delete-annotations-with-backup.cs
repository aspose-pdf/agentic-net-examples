using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Paths for the original PDF, the backup copy, and the output PDF after annotation deletion
        const string inputPdfPath   = "input.pdf";
        const string backupPdfPath  = "input_backup.pdf";
        const string outputPdfPath  = "output.pdf";

        // Set to true to keep a backup of the original PDF before modifications
        bool retainBackup = true;

        // Verify that the source PDF exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Source file not found: {inputPdfPath}");
            return;
        }

        // Optionally create a backup copy of the original PDF
        if (retainBackup)
        {
            // Overwrite any existing backup file
            File.Copy(inputPdfPath, backupPdfPath, overwrite: true);
            Console.WriteLine($"Backup created at: {backupPdfPath}");
        }

        // Load the PDF document within a using block for deterministic disposal
        using (Document doc = new Document(inputPdfPath))
        {
            // Initialize the annotation editor with the loaded document
            PdfAnnotationEditor editor = new PdfAnnotationEditor(doc);

            // Delete all annotations in the document
            editor.DeleteAnnotations();

            // Save the modified document to the desired output path
            editor.Save(outputPdfPath);

            // Close the editor (releases the bound document resources)
            editor.Close();
        }

        Console.WriteLine($"Annotations removed. Result saved to: {outputPdfPath}");
    }
}