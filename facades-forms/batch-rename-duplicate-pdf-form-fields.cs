using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf.Facades;   // Form, FormEditor
using Aspose.Pdf;           // Document (if needed)

class BatchFieldRenamer
{
    static void Main()
    {
        // Input folder containing PDFs to process
        const string inputFolder = @"C:\PdfBatch\Input";
        // Output folder for PDFs with renamed fields
        const string outputFolder = @"C:\PdfBatch\Output";

        if (!Directory.Exists(inputFolder))
        {
            Console.Error.WriteLine($"Input folder not found: {inputFolder}");
            return;
        }

        Directory.CreateDirectory(outputFolder);

        // Keep track of how many times each field name has been seen across all documents
        var fieldUsage = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);

        // Process each PDF file in the input folder
        foreach (string inputPath in Directory.GetFiles(inputFolder, "*.pdf"))
        {
            string fileName = Path.GetFileNameWithoutExtension(inputPath);
            string outputPath = Path.Combine(outputFolder, $"{fileName}_renamed.pdf");

            // First, read existing field names using the Form facade
            string[] existingFields;
            using (Form form = new Form(inputPath))
            {
                existingFields = form.FieldNames;
            }

            // Open the same file with FormEditor to perform renaming
            using (FormEditor editor = new FormEditor(inputPath, outputPath))
            {
                foreach (string fieldName in existingFields)
                {
                    if (fieldUsage.TryGetValue(fieldName, out int count))
                    {
                        // Duplicate detected – increment counter and create a new unique name
                        count++;
                        fieldUsage[fieldName] = count;
                        string newFieldName = $"{fieldName}_{count}";
                        editor.RenameField(fieldName, newFieldName);
                    }
                    else
                    {
                        // First occurrence – keep the original name
                        fieldUsage[fieldName] = 1;
                    }
                }

                // Persist changes to the output file
                editor.Save();
            }

            Console.WriteLine($"Processed: {Path.GetFileName(inputPath)} → {Path.GetFileName(outputPath)}");
        }

        Console.WriteLine("Batch renaming completed.");
    }
}