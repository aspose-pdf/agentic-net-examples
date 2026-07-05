using System;
using System.IO;
using Aspose.Pdf;                     // Core PDF API
using Aspose.Pdf.Facades;            // Facades API (if needed for other operations)

class ExportAnnotationsExample
{
    static void Main()
    {
        // Path to the source PDF file
        const string inputPdfPath = "input.pdf";

        // Ensure the source file exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"File not found: {inputPdfPath}");
            return;
        }

        // MemoryStream that will hold the XFDF data
        using (MemoryStream xfdfStream = new MemoryStream())
        {
            // Load the original PDF
            using (Document sourceDoc = new Document(inputPdfPath))
            {
                // Create a temporary PDF that contains only pages 1 and 2
                using (Document tempDoc = new Document())
                {
                    // Aspose.Pdf uses 1‑based page indexing
                    tempDoc.Pages.Add(sourceDoc.Pages[1]);
                    tempDoc.Pages.Add(sourceDoc.Pages[2]);

                    // Export all annotations from the temporary document to the XFDF stream
                    // This method writes the XFDF XML into the provided stream
                    tempDoc.ExportAnnotationsToXfdf(xfdfStream);
                }
            }

            // Reset the stream position so it can be read from the beginning
            xfdfStream.Position = 0;

            // Example: read the XFDF content as a string (for further processing)
            using (StreamReader reader = new StreamReader(xfdfStream))
            {
                string xfdfXml = reader.ReadToEnd();
                Console.WriteLine("Exported XFDF content:");
                Console.WriteLine(xfdfXml);
            }

            // At this point, xfdfStream contains the XFDF data for pages 1‑2
            // It can be passed to other APIs, saved to a file, etc.
        }
    }
}