using System;
using System.Collections.Generic;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";

        if (!System.IO.File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // PdfExtractor implements IDisposable via Facade, so use using for deterministic disposal
        using (PdfExtractor extractor = new PdfExtractor())
        {
            // Bind the PDF file to the extractor
            extractor.BindPdf(inputPath);

            // Extract attachments (required before calling GetAttachNames)
            extractor.ExtractAttachment();

            // Retrieve the list of attachment names
            IList<string> attachmentNames = extractor.GetAttachNames();

            // Output each attachment name
            foreach (string name in attachmentNames)
            {
                Console.WriteLine(name);
            }
        }
    }
}