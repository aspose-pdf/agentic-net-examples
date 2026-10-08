using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputPdf = "portfolio.pdf";

        // Files that will be embedded into the PDF portfolio
        string[] filesToEmbed = { "file1.txt", "image1.jpg", "data.xlsx" };

        // Verify the source PDF exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }

        // Verify each file to embed exists
        foreach (var f in filesToEmbed)
        {
            if (!File.Exists(f))
            {
                Console.Error.WriteLine($"Embedded file not found: {f}");
                return;
            }
        }

        try
        {
            // Load the original PDF inside a using block for deterministic disposal
            using (Document doc = new Document(inputPdf))
            {
                // Ensure the document has a Collection (required for PDF Portfolio)
                if (doc.Collection == null)
                    doc.Collection = new Collection();

                // Add each file as an embedded file (FileSpecification) to the collection
                foreach (string filePath in filesToEmbed)
                {
                    // Create a FileSpecification with a display name and description
                    var fileSpec = new FileSpecification(Path.GetFileName(filePath), $"Embedded file: {Path.GetFileName(filePath)}")
                    {
                        // Load the file contents into a memory stream
                        Contents = new MemoryStream(File.ReadAllBytes(filePath))
                    };

                    // Add the specification to the portfolio collection
                    doc.Collection.Add(fileSpec);
                }

                // Optionally set some document metadata
                doc.Info.Title = "PDF Portfolio with embedded files";

                // Save the resulting PDF portfolio
                doc.Save(outputPdf);
            }

            Console.WriteLine($"Portfolio PDF saved to '{outputPdf}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
