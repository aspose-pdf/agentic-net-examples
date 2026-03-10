using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";          // Source PDF containing attachments
        const string outputFolder = "Attachments";    // Folder where attachments will be saved
        const string outputPdf = "output.pdf";        // Optional copy of the original PDF

        // Verify the input file exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Ensure the output directory for attachments exists
        Directory.CreateDirectory(outputFolder);

        // -------- Extract all attachments --------
        using (PdfExtractor extractor = new PdfExtractor())
        {
            // Bind the PDF file to the extractor
            extractor.BindPdf(inputPdf);

            // Prepare extraction of attachments
            extractor.ExtractAttachment();

            // Write each attachment to the specified folder
            extractor.GetAttachment(outputFolder);
        }

        // -------- Save (or copy) the PDF as a PDF file --------
        using (Document doc = new Document(inputPdf))
        {
            // Save the document; this creates a new PDF file identical to the source
            doc.Save(outputPdf);
        }

        Console.WriteLine($"All attachments have been extracted to '{outputFolder}'.");
        Console.WriteLine($"PDF has been saved as '{outputPdf}'.");
    }
}