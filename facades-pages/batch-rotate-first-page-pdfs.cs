using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf.Facades;

class BatchRotateFirstPage
{
    static void Main()
    {
        // Folder containing source PDFs
        const string inputFolder = @"C:\PdfInput";
        // Folder where rotated PDFs will be saved
        const string outputFolder = @"C:\PdfOutput";

        // Ensure output folder exists
        Directory.CreateDirectory(outputFolder);

        // Process each PDF file in the input folder
        foreach (string inputPath in Directory.GetFiles(inputFolder, "*.pdf"))
        {
            string fileName = Path.GetFileName(inputPath);
            string outputPath = Path.Combine(outputFolder, fileName);

            // Use PdfPageEditor facade to rotate pages
            using (PdfPageEditor editor = new PdfPageEditor())
            {
                // Bind the source PDF file
                editor.BindPdf(inputPath);

                // Prepare a dictionary for per‑page rotations (page number is 1‑based)
                editor.PageRotations = new Dictionary<int, int>
                {
                    { 1, 90 } // Rotate the first page by 90 degrees (valid values: 0,90,180,270)
                };

                // Apply the rotation changes
                editor.ApplyChanges();

                // Save the modified PDF to the output location
                editor.Save(outputPath);
                // No need to call Close(); the using statement disposes the editor.
            }

            Console.WriteLine($"Rotated first page of '{fileName}' and saved to '{outputPath}'.");
        }
    }
}
