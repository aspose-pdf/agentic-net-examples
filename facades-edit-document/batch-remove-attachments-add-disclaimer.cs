using System;
using System.IO;
using System.Drawing; // Needed for System.Drawing.Rectangle
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class BatchPdfProcessor
{
    // Standard disclaimer text to be added to each PDF
    private const string DisclaimerText = "DISCLAIMER: This document is confidential and intended solely for the designated recipient.";

    static void Main()
    {
        // Input folder containing PDFs to process
        const string inputFolder = @"C:\InputPdfs";
        // Output folder where processed PDFs will be saved
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
            string fileName = Path.GetFileName(inputPath);
            string outputPath = Path.Combine(outputFolder, fileName);

            try
            {
                // Use PdfContentEditor (a Facade) to delete attachments and add a disclaimer annotation
                using (PdfContentEditor editor = new PdfContentEditor())
                {
                    // Bind the source PDF file
                    editor.BindPdf(inputPath);

                    // Remove all embedded attachments
                    editor.DeleteAttachments();

                    // Define the rectangle where the disclaimer will appear (coordinates are in points)
                    // System.Drawing.Rectangle expects (x, y, width, height)
                    // Original Aspose rectangle: (llx, lly, urx, ury) = (50, 750, 550, 800)
                    System.Drawing.Rectangle disclaimerRect = new System.Drawing.Rectangle(
                        50,               // X (lower‑left)
                        750,              // Y (lower‑left)
                        550 - 50,         // Width
                        800 - 750);       // Height

                    // Add the disclaimer text on the first page (page number = 1)
                    // Signature: CreateText(Rectangle rect, string text, string author, bool isOpen, string name, int page)
                    editor.CreateText(disclaimerRect, DisclaimerText, "System", true, "Disclaimer", 1);

                    // Save the modified PDF to the output location
                    editor.Save(outputPath);
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
