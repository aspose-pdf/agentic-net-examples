using System;
using System.IO;
using Aspose.Pdf; // Core Aspose.Pdf namespace

class Program
{
    static void Main()
    {
        // Paths for the output portfolio PDF and a sample file to embed
        const string outputPdfPath = "portfolio.pdf";
        const string sampleFilePath = "sample.txt";

        // Ensure the sample file exists (create a simple placeholder if missing)
        if (!File.Exists(sampleFilePath))
        {
            File.WriteAllText(sampleFilePath, "This is a sample file embedded in the PDF Portfolio.");
        }

        // Create a new PDF document using the Document constructor.
        // Wrap the Document in a using block for deterministic disposal (lifecycle rule).
        using (Document doc = new Document())
        {
            // Initialise the collection that represents a PDF Portfolio.
            if (doc.Collection == null)
                doc.Collection = new Collection();

            // Create a FileSpecification for the file we want to embed.
            var fileSpec = new FileSpecification(sampleFilePath, Path.GetFileName(sampleFilePath))
            {
                // Load the file contents into a memory stream.
                Contents = new MemoryStream(File.ReadAllBytes(sampleFilePath))
            };

            // Add the file specification to the portfolio collection.
            doc.Collection.Add(fileSpec);

            // Optionally set a title for the portfolio (metadata).
            doc.Info.Title = "Sample PDF Portfolio";

            // Save the portfolio PDF.
            doc.Save(outputPdfPath);
        }

        Console.WriteLine($"PDF Portfolio created at '{outputPdfPath}'.");
    }
}
