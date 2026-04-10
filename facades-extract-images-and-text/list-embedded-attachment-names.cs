using System;
using System.Collections.Generic;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Path to the PDF file that may contain embedded attachments
        const string inputPdf = "input.pdf";

        // Ensure the file exists before processing
        if (!System.IO.File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // PdfExtractor is a Facade; use a using block for deterministic disposal
        using (PdfExtractor extractor = new PdfExtractor())
        {
            // Bind the PDF document to the extractor
            extractor.BindPdf(inputPdf);

            // Extract the attachment objects from the PDF (required before GetAttachNames)
            extractor.ExtractAttachment();

            // Retrieve the list of attachment names
            IList<string> attachmentNames = extractor.GetAttachNames();

            // Output each attachment name
            Console.WriteLine("Embedded attachments:");
            foreach (string name in attachmentNames)
            {
                Console.WriteLine(name);
            }
        }
    }
}