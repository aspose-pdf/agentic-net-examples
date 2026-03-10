using System;
using System.IO;
using Aspose.Pdf.Facades; // Facade API for extracting attachments

class Program
{
    static void Main()
    {
        // Path to the source PDF that contains an embedded PDF attachment
        const string inputPdf = "source.pdf";

        // Folder where the extractor will write all attachments
        const string outputFolder = "ExtractedAttachments";

        // Desired final path for the extracted PDF attachment
        const string outputPdf = "extracted_attachment.pdf";

        // Verify the source file exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Ensure the output folder exists
        Directory.CreateDirectory(outputFolder);

        try
        {
            // Use PdfExtractor (a Facade) to work with the PDF
            using (PdfExtractor extractor = new PdfExtractor())
            {
                // Load the source PDF
                extractor.BindPdf(inputPdf);

                // Extract all embedded file attachments
                extractor.ExtractAttachment();

                // Write the extracted files to the output folder
                extractor.GetAttachment(outputFolder);
            }

            // After extraction, locate the first PDF file among the extracted attachments
            string[] pdfAttachments = Directory.GetFiles(outputFolder, "*.pdf", SearchOption.TopDirectoryOnly);

            if (pdfAttachments.Length > 0)
            {
                // Move (or copy) the attachment to the desired output location
                File.Copy(pdfAttachments[0], outputPdf, overwrite: true);
                Console.WriteLine($"Attachment extracted to '{outputPdf}'.");
            }
            else
            {
                Console.WriteLine("No PDF attachment found in the source document.");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}