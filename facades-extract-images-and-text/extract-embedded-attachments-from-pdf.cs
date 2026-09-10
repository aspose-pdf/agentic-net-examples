using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf.Facades;

class Program
{
    static void Main(string[] args)
    {
        // Expect two arguments: input PDF path and output directory.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: ExtractAttachments <input-pdf> <output-directory>");
            return;
        }

        string inputPdfPath = args[0];
        string outputDirectory = args[1];

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }

        // Ensure the output directory exists.
        Directory.CreateDirectory(outputDirectory);

        try
        {
            // PdfExtractor implements IDisposable, so use a using block.
            using (PdfExtractor extractor = new PdfExtractor())
            {
                // Bind the source PDF file.
                extractor.BindPdf(inputPdfPath);

                // Extract all attachments from the PDF.
                extractor.ExtractAttachment();

                // Get the list of attachment names.
                IList<string> attachmentNames = extractor.GetAttachNames();

                // Get the attachment contents as memory streams.
                MemoryStream[] attachmentStreams = extractor.GetAttachment();

                // Write each attachment to the output directory.
                for (int i = 0; i < attachmentStreams.Length; i++)
                {
                    string attachmentName = attachmentNames[i];
                    string outputPath = Path.Combine(outputDirectory, attachmentName);

                    // Reset stream position before reading.
                    attachmentStreams[i].Position = 0;

                    using (FileStream fileStream = new FileStream(outputPath, FileMode.Create, FileAccess.Write))
                    {
                        attachmentStreams[i].CopyTo(fileStream);
                    }

                    Console.WriteLine($"Extracted: {attachmentName} -> {outputPath}");
                }
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error extracting attachments: {ex.Message}");
        }
    }
}