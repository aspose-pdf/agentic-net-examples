using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Initialize the PdfExtractor facade
        using (PdfExtractor extractor = new PdfExtractor())
        {
            // Bind the PDF document
            extractor.BindPdf(inputPdf);

            // Extract attachment information (required before GetAttachNames)
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