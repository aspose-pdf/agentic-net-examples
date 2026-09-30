using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputDir = "SplitPages";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputDir);

        // Load the source PDF
        Document pdfDoc = new Document(inputPath);
        int pageCount = pdfDoc.Pages.Count;

        // Iterate over each page and write it to a separate MemoryStream
        for (int page = 1; page <= pageCount; page++)
        {
            using (MemoryStream pageStream = new MemoryStream())
            {
                // Create a temporary document that contains only the current page
                using (Document singlePageDoc = new Document())
                {
                    // Add (clone) the required page – this does not modify the original document
                    singlePageDoc.Pages.Add(pdfDoc.Pages[page]);
                    // Save the single‑page document into the memory stream
                    singlePageDoc.Save(pageStream);
                }

                // Reset the stream position before reading its bytes
                pageStream.Position = 0;

                // Write the stream contents to a physical PDF file
                string outPath = Path.Combine(outputDir, $"Page_{page}.pdf");
                File.WriteAllBytes(outPath, pageStream.ToArray());

                Console.WriteLine($"Saved page {page} → {outPath}");
            }
        }
    }
}
