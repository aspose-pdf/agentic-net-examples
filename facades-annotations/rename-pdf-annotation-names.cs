using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class BatchAnnotationRenamer
{
    // Standardized prefix for all annotation names
    private const string Prefix = "StdPrefix_";

    static void Main()
    {
        // Folder containing PDFs to process
        const string inputFolder = @"C:\InputPdfs";
        // Folder where modified PDFs will be saved
        const string outputFolder = @"C:\OutputPdfs";

        if (!Directory.Exists(inputFolder))
        {
            Console.Error.WriteLine($"Input folder does not exist: {inputFolder}");
            return;
        }

        Directory.CreateDirectory(outputFolder);

        // Process each PDF file in the input folder
        foreach (string inputPath in Directory.GetFiles(inputFolder, "*.pdf"))
        {
            string fileName = Path.GetFileNameWithoutExtension(inputPath);
            string outputPath = Path.Combine(outputFolder, $"{fileName}_renamed.pdf");

            try
            {
                // Load the PDF document
                Document pdfDoc = new Document(inputPath);
                int pageCount = pdfDoc.Pages.Count;
                int globalCounter = 1; // ensures unique names across pages

                for (int pageIndex = 1; pageIndex <= pageCount; pageIndex++)
                {
                    Page page = pdfDoc.Pages[pageIndex];
                    // Retrieve all annotations on the current page
                    var annotations = page.Annotations;
                    if (annotations == null) continue;

                    foreach (Annotation annotation in annotations)
                    {
                        // Build new name: Prefix + original name (if any) + counter
                        string baseName = string.IsNullOrEmpty(annotation.Name) ? "Anno" : annotation.Name;
                        string newName = $"{Prefix}{baseName}_{globalCounter++}";

                        // Assign the new name
                        annotation.Name = newName;
                    }
                }

                // Save the modified PDF to the output location
                pdfDoc.Save(outputPath);
                Console.WriteLine($"Processed: {Path.GetFileName(inputPath)} → {Path.GetFileName(outputPath)}");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing '{inputPath}': {ex.Message}");
            }
        }

        Console.WriteLine("Batch annotation renaming completed.");
    }
}
