using System;
using System.IO;
using Aspose.Pdf;

class PortfolioCreator
{
    static void Main()
    {
        // Paths
        const string inputPdfPath = "input.pdf";          // Existing PDF to convert
        const string outputPdfPath = "portfolio.pdf";     // Resulting PDF Portfolio
        string[] filesToEmbed = { "file1.docx", "image.png", "notes.txt" }; // Files to embed

        // Verify input PDF exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }

        // Verify files to embed exist
        foreach (string f in filesToEmbed)
        {
            if (!File.Exists(f))
            {
                Console.Error.WriteLine($"Embedded file not found: {f}");
                return;
            }
        }

        // Load the source PDF, add embedded files, and save as a Portfolio
        using (Document doc = new Document(inputPdfPath))
        {
            // Ensure the document has a Collection (portfolio) object
            if (doc.Collection == null)
                doc.Collection = new Collection();

            // Add each file to the PDF Portfolio
            foreach (string filePath in filesToEmbed)
            {
                var fileSpec = new FileSpecification(Path.GetFileName(filePath), "Embedded file")
                {
                    Contents = new MemoryStream(File.ReadAllBytes(filePath))
                };
                doc.Collection.Add(fileSpec);
            }

            // Optionally set a title/description for the portfolio
            doc.Info.Title = "PDF Portfolio with embedded files";

            // Save the resulting PDF Portfolio
            doc.Save(outputPdfPath);
        }

        Console.WriteLine($"Portfolio created: {outputPdfPath}");
    }
}
