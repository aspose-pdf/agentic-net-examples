using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputDir = "Attachments";

        // Verify input PDF exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputDir);

        try
        {
            // Create a PdfExtractor and bind the source PDF
            using (PdfExtractor extractor = new PdfExtractor())
            {
                extractor.BindPdf(inputPdf);

                // Extract all attachments from the PDF
                extractor.ExtractAttachment();

                // Retrieve attachment names and their corresponding streams
                IList<string> names = extractor.GetAttachNames();
                MemoryStream[] streams = extractor.GetAttachment();

                // Write each attachment to the output directory
                for (int i = 0; i < streams.Length; i++)
                {
                    string fileName = names[i];
                    string outPath = Path.Combine(outputDir, fileName);

                    using (FileStream fs = new FileStream(outPath, FileMode.Create, FileAccess.Write))
                    {
                        MemoryStream ms = streams[i];
                        ms.Position = 0;               // Reset stream position
                        ms.CopyTo(fs);                 // Write stream content to file
                    }
                }
            }

            Console.WriteLine("All attachments extracted successfully.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}