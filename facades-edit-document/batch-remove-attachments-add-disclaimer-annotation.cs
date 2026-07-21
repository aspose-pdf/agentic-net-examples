using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Annotations;

class BatchPdfProcessor
{
    static void Main()
    {
        // Folder containing source PDFs
        const string inputFolder = @"C:\InputPdfs";
        // Folder where processed PDFs will be written
        const string outputFolder = @"C:\ProcessedPdfs";

        // Ensure output directory exists
        Directory.CreateDirectory(outputFolder);

        // Standard disclaimer text
        const string disclaimerTitle   = "Disclaimer";
        const string disclaimerContent = "This document is confidential and intended solely for the recipient.";

        // Process each PDF file in the input folder
        foreach (string inputPath in Directory.GetFiles(inputFolder, "*.pdf"))
        {
            string fileName   = Path.GetFileNameWithoutExtension(inputPath);
            string outputPath = Path.Combine(outputFolder, fileName + "_processed.pdf");

            // Use PdfContentEditor (Facade) to delete all attachments and then add the disclaimer annotation
            using (PdfContentEditor editor = new PdfContentEditor())
            {
                // Load the PDF
                editor.BindPdf(inputPath);

                // Remove every attachment from the document
                editor.DeleteAttachments();

                // Add a text annotation (disclaimer) on the first page
                // Aspose.Pdf uses 1‑based page indexing
                Page firstPage = editor.Document.Pages[1];

                // Define the rectangle where the annotation will appear
                // (left, bottom, right, top) in points
                Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(50, 50, 550, 120);

                // Create the annotation
                TextAnnotation disclaimer = new TextAnnotation(firstPage, rect)
                {
                    Title    = disclaimerTitle,
                    Contents = disclaimerContent,
                    // Use Aspose.Pdf.Color to stay cross‑platform
                    Color    = Aspose.Pdf.Color.Yellow,
                    // Open the annotation by default
                    Open     = true,
                    // Use the standard "Note" icon
                    Icon     = TextIcon.Note
                };

                // Attach the annotation to the page
                firstPage.Annotations.Add(disclaimer);

                // Save the modified PDF to the output location
                editor.Save(outputPath);
            }

            Console.WriteLine($"Processed: {Path.GetFileName(inputPath)} → {Path.GetFileName(outputPath)}");
        }

        Console.WriteLine("Batch processing completed.");
    }
}