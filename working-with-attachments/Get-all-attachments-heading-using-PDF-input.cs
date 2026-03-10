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

        try
        {
            // Initialize the extractor and bind the PDF file
            using (PdfExtractor extractor = new PdfExtractor())
            {
                extractor.BindPdf(inputPdf);

                // Extract attachment information from the document
                extractor.ExtractAttachment();

                // Retrieve the list of attachment names
                IList<string> attachmentNames = extractor.GetAttachNames();

                Console.WriteLine("Attachments found in the PDF:");
                foreach (string name in attachmentNames)
                {
                    Console.WriteLine(name);
                }
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}