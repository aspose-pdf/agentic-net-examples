using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input PDF files to process
        string[] inputFiles = new[]
        {
            "input1.pdf",
            "input2.pdf",
            "input3.pdf"
        };

        // Directory where processed PDFs will be saved
        string outputDirectory = "Processed";
        Directory.CreateDirectory(outputDirectory);

        // Process each PDF concurrently
        Parallel.ForEach(inputFiles, inputPath =>
        {
            try
            {
                if (!File.Exists(inputPath))
                {
                    Console.Error.WriteLine($"File not found: {inputPath}");
                    return;
                }

                string fileName = Path.GetFileNameWithoutExtension(inputPath);
                string outputPath = Path.Combine(outputDirectory, $"{fileName}_rotated.pdf");

                // Load, edit, and save the PDF using Aspose.Pdf.Facades
                using (Document doc = new Document(inputPath))
                using (PdfPageEditor editor = new PdfPageEditor())
                {
                    // Bind the document to the facade
                    editor.BindPdf(doc);

                    // Example edit: rotate all pages 90 degrees clockwise
                    editor.Rotation = 90;

                    // Apply the changes to the bound document
                    editor.ApplyChanges();

                    // Save the edited document
                    editor.Save(outputPath);
                }

                Console.WriteLine($"Processed: {inputPath} -> {outputPath}");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing '{inputPath}': {ex.Message}");
            }
        });

        Console.WriteLine("Batch processing completed.");
    }
}