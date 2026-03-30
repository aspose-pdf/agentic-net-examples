using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "sample.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine("File not found: " + inputPath);
            return;
        }

        using (PdfExtractor extractor = new PdfExtractor())
        {
            extractor.BindPdf(inputPath);
            extractor.ExtractAttachment();
            IList<string> attachmentNames = extractor.GetAttachNames();

            Console.WriteLine("Attachments found: " + attachmentNames.Count);
            foreach (string name in attachmentNames)
            {
                Console.WriteLine(name);
            }
        }
    }
}
