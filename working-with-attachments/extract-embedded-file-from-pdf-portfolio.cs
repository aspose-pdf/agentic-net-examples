using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Path to the PDF portfolio containing embedded files
        const string inputPdf = "portfolio.pdf";

        // Index of the portfolio item to extract (1‑based, as Aspose collections are 1‑based)
        const int itemIndex = 1;

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        try
        {
            // Open the PDF document
            using (Document doc = new Document(inputPdf))
            {
                // Verify that the requested index exists
                if (doc.EmbeddedFiles == null || doc.EmbeddedFiles.Count < itemIndex)
                {
                    Console.Error.WriteLine("Embedded file index out of range.");
                    return;
                }

                // Retrieve the embedded file (Aspose collections are 1‑based)
                FileSpecification spec = doc.EmbeddedFiles[itemIndex];

                // The original file name (including extension) is stored in the Name property
                string originalFileName = spec.Name;

                // Ensure the stream is positioned at the beginning before copying
                if (spec.Contents.CanSeek)
                {
                    spec.Contents.Position = 0;
                }

                // Save the embedded file's data to disk using its original name
                using (FileStream outStream = File.Create(originalFileName))
                {
                    spec.Contents.CopyTo(outStream);
                }

                Console.WriteLine($"Extracted '{originalFileName}' from the portfolio.");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
