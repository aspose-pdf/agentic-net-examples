using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string sourcePath      = "source.pdf";      // PDF containing the original form fields
        const string destinationPath = "destination.pdf"; // PDF that will receive the copied fields
        const string outputPath      = "merged.pdf";      // Resulting PDF with fields copied

        if (!File.Exists(sourcePath))
        {
            Console.Error.WriteLine($"Source file not found: {sourcePath}");
            return;
        }
        if (!File.Exists(destinationPath))
        {
            Console.Error.WriteLine($"Destination file not found: {destinationPath}");
            return;
        }

        // Load both PDFs inside using blocks for deterministic disposal
        using (Document srcDoc = new Document(sourcePath))
        using (Document destDoc = new Document(destinationPath))
        {
            // Bind the destination document to a FormEditor instance
            using (FormEditor formEditor = new FormEditor(destDoc))
            {
                // Iterate over every form field in the source document
                foreach (var field in srcDoc.Form.Fields)
                {
                    // Copy the field from the source PDF into the bound destination PDF
                    // CopyOuterField(sourceFileName, fullyQualifiedFieldName)
                    formEditor.CopyOuterField(sourcePath, field.FullName);
                }

                // Persist the changes to a new file
                formEditor.Save(outputPath);
            }
        }

        Console.WriteLine($"Form fields successfully copied to '{outputPath}'.");
    }
}