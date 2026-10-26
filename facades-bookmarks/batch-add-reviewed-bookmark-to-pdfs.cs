using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations; // Added namespace for action types

class Program
{
    static void Main()
    {
        // Folder containing PDFs to process
        const string inputFolder = @"C:\PdfBatch";
        // Output folder for processed PDFs
        const string outputFolder = @"C:\PdfBatch\Reviewed";

        if (!Directory.Exists(inputFolder))
        {
            Console.Error.WriteLine($"Input folder not found: {inputFolder}");
            return;
        }

        Directory.CreateDirectory(outputFolder);

        // Process each PDF file in the input folder
        foreach (string inputPath in Directory.GetFiles(inputFolder, "*.pdf"))
        {
            try
            {
                // Load the document (using for deterministic disposal)
                using (Document doc = new Document(inputPath))
                {
                    int pageCount = doc.Pages.Count;

                    // Get the last page instance
                    Page lastPage = doc.Pages[pageCount];

                    // Add a top‑level bookmark titled "Reviewed" that points to the last page
                    OutlineItemCollection bookmark = new OutlineItemCollection(doc.Outlines)
                    {
                        Title = "Reviewed",
                        Action = new GoToAction(lastPage) // jump to the last page
                    };
                    doc.Outlines.Add(bookmark);

                    // Prepare output file path
                    string fileName = Path.GetFileNameWithoutExtension(inputPath);
                    string outputPath = Path.Combine(outputFolder, $"{fileName}_reviewed.pdf");

                    // Save the modified PDF
                    doc.Save(outputPath);

                    Console.WriteLine($"Processed: {Path.GetFileName(inputPath)} → {Path.GetFileName(outputPath)}");
                }
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing '{inputPath}': {ex.Message}");
            }
        }

        Console.WriteLine("Batch bookmark addition completed.");
    }
}
