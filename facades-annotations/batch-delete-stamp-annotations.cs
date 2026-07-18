using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Folder containing PDFs to process
        const string inputFolder  = @"C:\Pdf\Input";
        // Folder where cleaned PDFs will be saved
        const string outputFolder = @"C:\Pdf\Output";

        // Ensure output directory exists
        Directory.CreateDirectory(outputFolder);

        // Process each PDF file in the input folder
        foreach (string pdfPath in Directory.GetFiles(inputFolder, "*.pdf"))
        {
            try
            {
                // Initialize the annotation editor facade
                using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
                {
                    // Load the PDF document
                    editor.BindPdf(pdfPath);

                    // Delete all annotations of type "Stamp"
                    editor.DeleteAnnotations("Stamp");

                    // Build output file path (overwrite original name in output folder)
                    string outPath = Path.Combine(outputFolder, Path.GetFileName(pdfPath));

                    // Save the modified PDF
                    editor.Save(outPath);

                    // Close the facade (releases any resources)
                    editor.Close();
                }

                Console.WriteLine($"Processed: {Path.GetFileName(pdfPath)}");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing '{pdfPath}': {ex.Message}");
            }
        }

        Console.WriteLine("Batch stamp deletion completed.");
    }
}