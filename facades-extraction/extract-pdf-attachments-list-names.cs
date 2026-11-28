using System;
using System.IO;
using System.Linq; // Needed for ToArray()
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";
        const string outputListPath = "AttachmentsList.txt";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF file not found: {pdfPath}");
            return;
        }

        // PdfExtractor is a Facades class used for extracting content such as attachments.
        using (PdfExtractor extractor = new PdfExtractor())
        {
            // Bind the source PDF document.
            extractor.BindPdf(pdfPath);

            // Extract all attachments from the bound PDF.
            extractor.ExtractAttachment();

            // Retrieve the names of the extracted attachments.
            // GetAttachNames returns IList<string>; convert it to a string array for WriteAllLines.
            string[] attachmentNames = extractor.GetAttachNames().ToArray();

            // Write the attachment names to a text file (one name per line).
            File.WriteAllLines(outputListPath, attachmentNames);
        }

        Console.WriteLine($"Attachment list written to '{outputListPath}'.");
    }
}
