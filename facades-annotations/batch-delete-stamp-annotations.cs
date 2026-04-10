using System;
using System.IO;
using Aspose.Pdf.Facades;

class BatchStampRemover
{
    static void Main()
    {
        // Folder containing PDFs to process
        const string inputFolder = @"C:\PdfInput";
        // Folder where cleaned PDFs will be saved
        const string outputFolder = @"C:\PdfOutput";

        // Ensure the output directory exists
        Directory.CreateDirectory(outputFolder);

        // Process each PDF file in the input folder
        foreach (string inputPath in Directory.GetFiles(inputFolder, "*.pdf"))
        {
            // Determine the output file path (same name, different folder)
            string outputPath = Path.Combine(outputFolder, Path.GetFileName(inputPath));

            // Use PdfAnnotationEditor to delete stamp annotations
            using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
            {
                // Load the PDF document
                editor.BindPdf(inputPath);

                // Delete all annotations of type "Stamp"
                editor.DeleteAnnotations("Stamp");

                // Save the modified PDF
                editor.Save(outputPath);
            }

            Console.WriteLine($"Processed: {Path.GetFileName(inputPath)} → {outputPath}");
        }

        Console.WriteLine("Batch stamp removal completed.");
    }
}