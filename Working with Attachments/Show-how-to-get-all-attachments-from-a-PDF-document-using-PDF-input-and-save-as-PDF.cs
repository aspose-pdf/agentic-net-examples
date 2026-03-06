using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input PDF containing attachments
        const string inputPdfPath = "input.pdf";

        // Output PDF (a copy of the input, saved after extraction)
        const string outputPdfPath = "output.pdf";

        // Folder where extracted attachments will be saved
        const string attachmentsFolder = "ExtractedAttachments";

        // Ensure the attachments folder exists
        Directory.CreateDirectory(attachmentsFolder);

        // ---------- Extract all attachments ----------
        // PdfExtractor is a Facade class that provides extraction capabilities.
        using (PdfExtractor extractor = new PdfExtractor())
        {
            // Bind the source PDF file
            extractor.BindPdf(inputPdfPath);

            // Prepare the extractor to work with attachments
            extractor.ExtractAttachment();

            // Save all extracted attachments to the specified folder
            // GetAttachment(string) writes each attachment file into the folder.
            extractor.GetAttachment(attachmentsFolder);
        }

        // ---------- Save (or copy) the PDF ----------
        // Wrap the Document in a using block for deterministic disposal.
        using (Document doc = new Document(inputPdfPath))
        {
            // Save the PDF. No modifications are made; this simply writes a copy.
            doc.Save(outputPdfPath);
        }

        Console.WriteLine($"Attachments extracted to '{attachmentsFolder}'.");
        Console.WriteLine($"PDF saved as '{outputPdfPath}'.");
    }
}