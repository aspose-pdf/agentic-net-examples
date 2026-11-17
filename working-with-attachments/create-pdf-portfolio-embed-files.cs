using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "source.pdf";
        const string outputPdfPath = "portfolio.pdf";

        // Files to embed into the portfolio
        string[] filesToEmbed = { "doc1.txt", "image1.jpg", "report.pdf" };

        // Verify source PDF exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }

        // Verify each file to embed exists
        foreach (string f in filesToEmbed)
        {
            if (!File.Exists(f))
            {
                Console.Error.WriteLine($"File to embed not found: {f}");
                return;
            }
        }

        // Load the source PDF and convert it into a portfolio
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Ensure the document has a collection (portfolio) object
            if (pdfDoc.Collection == null)
                pdfDoc.Collection = new Collection();

            // Add each file as an embedded file (portfolio attachment)
            foreach (string filePath in filesToEmbed)
            {
                // Create a FileSpecification for the file
                var fileSpec = new FileSpecification(filePath, Path.GetFileName(filePath))
                {
                    // Optional: set the modification date of the attachment
                    Params = { ModDate = DateTime.UtcNow }
                };

                // Add the specification to the document's collection (portfolio)
                pdfDoc.Collection.Add(fileSpec);
            }

            // Save the resulting PDF portfolio
            pdfDoc.Save(outputPdfPath);
        }

        Console.WriteLine($"Portfolio PDF created: {outputPdfPath}");
    }
}
