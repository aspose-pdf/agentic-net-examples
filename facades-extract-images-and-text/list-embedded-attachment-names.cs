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

        // Create PdfExtractor, bind the PDF, extract attachments, then list their names.
        using (PdfExtractor extractor = new PdfExtractor())
        {
            extractor.BindPdf(inputPath);
            extractor.ExtractAttachment(); // Must be called before GetAttachNames()
            IList<string> attachmentNames = extractor.GetAttachNames();

            foreach (string name in attachmentNames)
            {
                Console.WriteLine(name);
            }
        }
    }
}