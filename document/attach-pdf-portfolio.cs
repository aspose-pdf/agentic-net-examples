using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string targetPdfPath = "target.pdf";
        const string portfolioFilePath = "attachment.pdf";
        const string outputPdfPath = "output.pdf";

        if (!File.Exists(targetPdfPath))
        {
            Console.Error.WriteLine($"Target PDF not found: {targetPdfPath}");
            return;
        }

        if (!File.Exists(portfolioFilePath))
        {
            Console.Error.WriteLine($"Portfolio file not found: {portfolioFilePath}");
            return;
        }

        using (Document document = new Document(targetPdfPath))
        {
            // Ensure a collection (portfolio) exists.
            if (document.Collection == null)
                document.Collection = new Collection();

            // Create a FileSpecification for the attachment with a custom description.
            var fileSpec = new FileSpecification(portfolioFilePath, "Custom description for the attached PDF file")
            {
                // Load the file bytes into the Contents stream.
                Contents = new MemoryStream(File.ReadAllBytes(portfolioFilePath))
            };

            // Add the file specification to the document's collection (portfolio).
            document.Collection.Add(fileSpec);

            // Set custom metadata (example: Title).
            document.Info.Title = "Custom document description metadata";

            // Save the resulting PDF.
            document.Save(outputPdfPath);
        }

        Console.WriteLine($"Portfolio attached and saved to '{outputPdfPath}'.");
    }
}