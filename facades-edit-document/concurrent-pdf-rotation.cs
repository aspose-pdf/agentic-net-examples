using System;
using System.IO;
using System.Collections.Generic;
using System.Threading.Tasks;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class BatchPdfProcessor
{
    static void Main()
    {
        // Input PDF files to process
        string[] inputFiles = new string[]
        {
            "input1.pdf",
            "input2.pdf",
            "input3.pdf"
        };

        // Directory where edited PDFs will be saved
        string outputDirectory = "ProcessedPdfs";

        // Ensure the output directory exists
        Directory.CreateDirectory(outputDirectory);

        // Process each PDF file in parallel
        Parallel.ForEach(inputFiles, inputPath =>
        {
            // Validate input file existence
            if (!File.Exists(inputPath))
            {
                Console.Error.WriteLine($"File not found: {inputPath}");
                return;
            }

            // Determine output file path
            string outputPath = Path.Combine(
                outputDirectory,
                Path.GetFileNameWithoutExtension(inputPath) + "_edited.pdf");

            try
            {
                // Load the PDF document (lifecycle rule: use using for disposal)
                using (Document doc = new Document(inputPath))
                {
                    // Create a PdfPageEditor facade to edit the document
                    using (PdfPageEditor editor = new PdfPageEditor())
                    {
                        // Bind the loaded document to the editor
                        editor.BindPdf(doc);

                        // Example edit: rotate all pages 90 degrees clockwise
                        editor.Rotation = 90;

                        // Apply the changes to the document
                        editor.ApplyChanges();
                    }

                    // Save the edited document (PDF format, no SaveOptions needed)
                    doc.Save(outputPath);
                }

                Console.WriteLine($"Processed: {inputPath} -> {outputPath}");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing '{inputPath}': {ex.Message}");
            }
        });
    }
}