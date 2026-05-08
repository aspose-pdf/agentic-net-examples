using System;
using System.IO;
using Aspose.Pdf; // core API for Document

class Program
{
    static void Main()
    {
        // Folder containing input PDFs
        const string inputFolder = @"C:\PdfInput";
        // Folder where modified PDFs will be saved
        const string outputFolder = @"C:\PdfOutput";

        if (!Directory.Exists(inputFolder))
        {
            Console.Error.WriteLine($"Input folder not found: {inputFolder}");
            return;
        }

        Directory.CreateDirectory(outputFolder);

        // Process each PDF file in the input folder
        foreach (string inputPath in Directory.GetFiles(inputFolder, "*.pdf"))
        {
            string fileName = Path.GetFileName(inputPath);
            string outputPath = Path.Combine(outputFolder, fileName);

            try
            {
                // Load the PDF document (core API)
                using (Document doc = new Document(inputPath))
                {
                    // Use FormEditor (fully qualified, no using directive for Aspose.Pdf.Facades)
                    using (Aspose.Pdf.Facades.FormEditor formEditor = new Aspose.Pdf.Facades.FormEditor())
                    {
                        // Bind the loaded document to the editor
                        formEditor.BindPdf(doc);

                        // Set a maximum of 100 characters for the field named "Comments"
                        formEditor.SetFieldLimit("Comments", 100);

                        // Save the modified document to the output location
                        formEditor.Save(outputPath);
                    }
                }

                Console.WriteLine($"Processed: {fileName}");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing '{fileName}': {ex.Message}");
            }
        }

        Console.WriteLine("Batch processing completed.");
    }
}