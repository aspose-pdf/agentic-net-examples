using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf.Facades;

public class Program
{
    public static void Main()
    {
        string inputPdf = "input.pdf";
        string outputRoot = "Attachments";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine("Input PDF not found: " + inputPdf);
            return;
        }

        Directory.CreateDirectory(outputRoot);

        using (PdfExtractor extractor = new PdfExtractor())
        {
            extractor.BindPdf(inputPdf);
            extractor.ExtractAttachment();

            IList<string> attachmentNames = extractor.GetAttachNames();
            MemoryStream[] attachmentStreams = extractor.GetAttachment();

            for (int i = 0; i < attachmentStreams.Length; i++)
            {
                string name = attachmentNames[i];
                string extension = Path.GetExtension(name);
                string subFolder;

                if (string.IsNullOrEmpty(extension))
                {
                    subFolder = Path.Combine(outputRoot, "no-extension");
                }
                else
                {
                    subFolder = Path.Combine(outputRoot, extension.TrimStart('.').ToLowerInvariant());
                }

                Directory.CreateDirectory(subFolder);
                string outputPath = Path.Combine(subFolder, name);

                using (FileStream fileStream = new FileStream(outputPath, FileMode.Create, FileAccess.Write))
                {
                    MemoryStream memStream = attachmentStreams[i];
                    memStream.Position = 0;
                    memStream.CopyTo(fileStream);
                }
            }
        }

        Console.WriteLine("Attachments extracted to folder: " + outputRoot);
    }
}
