using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        // Folder containing source PDFs
        string inputFolder = @"C:\InputPdfs";
        // Folder where processed PDFs will be saved
        string outputFolder = @"C:\OutputPdfs";

        if (!Directory.Exists(inputFolder))
        {
            Console.Error.WriteLine($"Input folder does not exist: {inputFolder}");
            return;
        }

        Directory.CreateDirectory(outputFolder);

        // Get all PDF files in the input folder (non‑recursive)
        string[] pdfFiles = Directory.GetFiles(inputFolder, "*.pdf", SearchOption.TopDirectoryOnly);

        foreach (string pdfPath in pdfFiles)
        {
            string fileName   = Path.GetFileName(pdfPath);
            string outputPath = Path.Combine(outputFolder, fileName);

            try
            {
                // Load the PDF. If it is encrypted, Document constructor throws PdfException.
                Document doc;
                try
                {
                    doc = new Document(pdfPath);
                }
                catch (PdfException)
                {
                    Console.Error.WriteLine($"Skipping encrypted PDF: {fileName}");
                    continue;
                }

                // Add a top‑level bookmark named "Table of Contents" that points to page 1.
                var tocBookmark = new OutlineItemCollection(doc.Outlines)
                {
                    Title = "Table of Contents",
                    Action = new GoToAction(doc.Pages[1], ExplicitDestinationType.Fit)
                };
                doc.Outlines.Add(tocBookmark);

                // Save the modified PDF to the output folder
                doc.Save(outputPath);

                Console.WriteLine($"Processed: {fileName}");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Failed to process {fileName}: {ex.Message}");
            }
        }

        Console.WriteLine("Batch processing completed.");
    }
}
