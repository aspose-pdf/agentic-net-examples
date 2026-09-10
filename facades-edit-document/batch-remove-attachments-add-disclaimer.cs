using System;
using System.IO;
using System.Drawing; // Needed for System.Drawing.Rectangle
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class BatchProcess
{
    static void Main()
    {
        // Folder containing input PDF files
        const string inputFolder = @"C:\InputPdfs";
        // Folder where processed PDFs will be saved
        const string outputFolder = @"C:\OutputPdfs";

        // Ensure output directory exists
        Directory.CreateDirectory(outputFolder);

        // Standardized disclaimer text
        const string disclaimer = "Disclaimer: This document is confidential and intended for the designated recipient only.";

        // Process each PDF file in the input folder
        foreach (string inputPath in Directory.GetFiles(inputFolder, "*.pdf"))
        {
            // Determine output file path
            string fileName = Path.GetFileNameWithoutExtension(inputPath);
            string outputPath = Path.Combine(outputFolder, fileName + "_processed.pdf");

            // Use PdfContentEditor facade to edit the PDF
            using (PdfContentEditor editor = new PdfContentEditor())
            {
                // Load the PDF document
                editor.BindPdf(inputPath);

                // Remove all existing attachments
                editor.DeleteAttachments();

                // Add disclaimer annotation to each page
                // Page indexing in Aspose.Pdf is 1‑based
                for (int pageNumber = 1; pageNumber <= editor.Document.Pages.Count; pageNumber++)
                {
                    // Get page dimensions
                    Page page = editor.Document.Pages[pageNumber];
                    double pageWidth = page.PageInfo.Width;
                    double pageHeight = page.PageInfo.Height;

                    // Define a rectangle for the annotation (bottom‑left corner)
                    // System.Drawing.Rectangle(x, y, width, height)
                    // y is measured from the top, so we offset from the bottom.
                    System.Drawing.Rectangle rect = new System.Drawing.Rectangle(
                        0,                                          // left (x)
                        (int)(pageHeight - 50),                    // top (y) – 50 points from bottom
                        (int)(pageWidth * 0.8),                    // width (80% of page width)
                        50);                                        // height

                    // Create a text annotation with the disclaimer
                    // Parameters: rectangle, text, author, isOpen, title, pageNumber
                    editor.CreateText(rect, disclaimer, "System", false, "Disclaimer", pageNumber);
                }

                // Save the modified PDF to the output path
                editor.Save(outputPath);
            }

            Console.WriteLine($"Processed: {outputPath}");
        }
    }
}
