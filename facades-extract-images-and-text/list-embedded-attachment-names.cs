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

        // Initialize PdfExtractor and bind the PDF document
        using (PdfExtractor extractor = new PdfExtractor())
        {
            extractor.BindPdf(inputPath);

            // Must extract attachments before retrieving their names
            extractor.ExtractAttachment();

            // Get the list of attachment names
            IList<string> attachmentNames = extractor.GetAttachNames();

            // Output each attachment name
            foreach (string name in attachmentNames)
            {
                Console.WriteLine(name);
            }
        }
    }
}