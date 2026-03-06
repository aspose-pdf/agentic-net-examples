using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Path to the source PDF that contains the attachment(s)
        const string inputPdfPath = "input.pdf";

        // Directory where extracted attachments will be saved
        const string outputFolder = "ExtractedAttachments";

        // Ensure the output directory exists
        Directory.CreateDirectory(outputFolder);

        // Use PdfExtractor (a Facade) to extract embedded files
        using (PdfExtractor extractor = new PdfExtractor())
        {
            // Bind the PDF document to the extractor
            extractor.BindPdf(inputPdfPath);

            // Prepare the extractor to work with attachments
            extractor.ExtractAttachment();

            // Save all attachments to the specified folder
            // Each attachment will be written with its original file name
            extractor.GetAttachment(outputFolder);
        }

        Console.WriteLine($"All attachments have been extracted to '{outputFolder}'.");
    }
}