using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputFolder = "Attachments";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        Directory.CreateDirectory(outputFolder);

        using (PdfExtractor extractor = new PdfExtractor())
        {
            // Load the PDF document
            extractor.BindPdf(inputPdf);

            // Extract all attachments
            extractor.ExtractAttachment();

            // Get attachment names and streams (generic IList<string>)
            IList<string> attachmentNames = extractor.GetAttachNames();
            MemoryStream[] streams = extractor.GetAttachment();

            for (int i = 0; i < streams.Length; i++)
            {
                string originalName = attachmentNames[i];
                string timestamp = DateTime.Now.ToString("yyyyMMddHHmmssfff");
                string newFileName = $"{timestamp}_{originalName}";
                string outputPath = Path.Combine(outputFolder, newFileName);

                using (FileStream fileStream = new FileStream(outputPath, FileMode.Create, FileAccess.Write))
                {
                    // Ensure the memory stream is positioned at the beginning
                    streams[i].Position = 0;
                    streams[i].CopyTo(fileStream);
                }
            }
        }

        Console.WriteLine("Attachments extracted and renamed successfully.");
    }
}
