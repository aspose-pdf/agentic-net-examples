using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string logPath   = "xmp_original.xml";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF document (lifecycle rule: use using)
            using (Document doc = new Document(inputPath))
            {
                // Create the XMP metadata facade and bind it to the loaded document
                using (PdfXmpMetadata xmp = new PdfXmpMetadata())
                {
                    xmp.BindPdf(doc);

                    // Retrieve the original XMP metadata as a byte array
                    byte[] data = xmp.GetXmpMetadata();

                    // Save the XML metadata for audit purposes
                    File.WriteAllBytes(logPath, data);
                }

                // Placeholder for any further modifications to the PDF
                // ...

                // Save the (potentially modified) PDF; here we simply copy the original
                doc.Save("output.pdf");
            }

            Console.WriteLine($"Original XMP metadata saved to '{logPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}