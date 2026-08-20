using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string outputPath = "portfolio.pdf";

        // Create a new PDF document. The Portfolio collection is created automatically.
        using (Document doc = new Document())
        {
            // Ensure the Collection object exists – it represents the PDF portfolio.
            if (doc.Collection == null)
                doc.Collection = new Collection();

            // Files that will be embedded in the portfolio
            string[] filesToAttach = { "file1.txt", "image.png" };

            foreach (string filePath in filesToAttach)
            {
                if (!File.Exists(filePath))
                    continue; // Skip missing files

                // Create a FileSpecification for the attachment.
                // The first argument is the file name that will appear in the portfolio,
                // the second argument is a description (using the same name here).
                var fileSpec = new FileSpecification(Path.GetFileName(filePath), Path.GetFileName(filePath));
                // Set the file contents via a memory stream.
                fileSpec.Contents = new MemoryStream(File.ReadAllBytes(filePath));

                // Add the file specification to the document's collection (portfolio).
                doc.Collection.Add(fileSpec);
            }

            // Save the PDF portfolio
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF Portfolio created at '{outputPath}'.");
    }
}
