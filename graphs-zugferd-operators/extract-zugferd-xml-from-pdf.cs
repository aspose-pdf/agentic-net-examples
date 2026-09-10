using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf = "invoice.pdf";          // PDF containing ZUGFeRD data
        const string outputXml = "ZUGFeRD.xml";         // Desired output file name

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        try
        {
            // Load the PDF document (lifecycle rule: use using for deterministic disposal)
            using (Document pdfDoc = new Document(inputPdf))
            {
                bool extracted = false;

                // Iterate over all embedded files in the PDF
                foreach (FileSpecification fileSpec in pdfDoc.EmbeddedFiles)
                {
                    // ZUGFeRD payload is an XML file; look for .xml extension (case‑insensitive)
                    if (!string.IsNullOrEmpty(fileSpec.Name) &&
                        fileSpec.Name.EndsWith(".xml", StringComparison.OrdinalIgnoreCase))
                    {
                        // Ensure the content stream is positioned at the beginning
                        if (fileSpec.Contents.CanSeek)
                            fileSpec.Contents.Position = 0;

                        // Write the embedded XML to the local file system
                        using (FileStream outStream = new FileStream(outputXml, FileMode.Create, FileAccess.Write))
                        {
                            fileSpec.Contents.CopyTo(outStream);
                        }

                        Console.WriteLine($"Embedded ZUGFeRD XML saved to '{outputXml}'.");
                        extracted = true;
                        break; // stop after the first matching file
                    }
                }

                if (!extracted)
                {
                    Console.WriteLine("No embedded ZUGFeRD XML file found in the PDF.");
                }
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
