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

        // Use PdfExtractor facade to work with attachments
        using (PdfExtractor extractor = new PdfExtractor())
        {
            // Bind the PDF document
            extractor.BindPdf(inputPdf);

            // Extract attachments (required before retrieving names)
            extractor.ExtractAttachment();

            // Get the list of attachment names
            IList<string> attachmentNames = extractor.GetAttachNames();

            // List the attachment names
            Console.WriteLine("Embedded attachments:");
            foreach (string name in attachmentNames)
            {
                Console.WriteLine(name);
            }
        }
    }
}