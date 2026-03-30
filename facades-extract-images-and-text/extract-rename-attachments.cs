using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "sample.pdf";
        const string outputDirectory = "attachments";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine("Input PDF not found: " + inputPdf);
            return;
        }

        if (!Directory.Exists(outputDirectory))
        {
            Directory.CreateDirectory(outputDirectory);
        }

        PdfExtractor extractor = new PdfExtractor();
        extractor.BindPdf(inputPdf);
        extractor.ExtractAttachment();

        IList<string> attachmentNames = extractor.GetAttachNames();
        MemoryStream[] attachmentStreams = extractor.GetAttachment();

        for (int i = 0; i < attachmentStreams.Length; i++)
        {
            string originalName = attachmentNames[i];
            string timestamp = DateTime.Now.ToString("yyyyMMddHHmmssfff");
            string newFileName = timestamp + "_" + originalName;
            string fullPath = Path.Combine(outputDirectory, newFileName);

            using (FileStream fileStream = new FileStream(fullPath, FileMode.Create, FileAccess.Write))
            {
                MemoryStream memStream = attachmentStreams[i];
                memStream.Position = 0;
                byte[] buffer = new byte[4096];
                int bytesRead;
                while ((bytesRead = memStream.Read(buffer, 0, buffer.Length)) > 0)
                {
                    fileStream.Write(buffer, 0, bytesRead);
                }
            }

            Console.WriteLine("Saved attachment as " + newFileName);
        }
    }
}
