using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Paths for the existing PDF Portfolio and the PDF to embed
        const string portfolioPath = "portfolio.pdf";
        const string embedPdfPath  = "document_to_embed.pdf";
        const string outputPath    = "portfolio_with_embedded.pdf";

        // Verify source files exist
        if (!File.Exists(portfolioPath))
        {
            Console.Error.WriteLine($"Portfolio file not found: {portfolioPath}");
            return;
        }
        if (!File.Exists(embedPdfPath))
        {
            Console.Error.WriteLine($"File to embed not found: {embedPdfPath}");
            return;
        }

        try
        {
            // Open the existing PDF Portfolio
            using (Document doc = new Document(portfolioPath))
            {
                // Ensure the document has a collection (Portfolio) view
                if (doc.Collection == null)
                    doc.Collection = new Collection();

                // Create a file specification for the PDF to embed
                // Use a stream for the file contents – this is the recommended way
                var fileSpec = new FileSpecification(Path.GetFileName(embedPdfPath), "Embedded PDF document")
                {
                    Contents = new MemoryStream(File.ReadAllBytes(embedPdfPath))
                };

                // Add the file specification to the portfolio's collection
                doc.Collection.Add(fileSpec);

                // Save the updated PDF Portfolio
                doc.Save(outputPath);
            }

            Console.WriteLine($"Embedded PDF added successfully. Output saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
