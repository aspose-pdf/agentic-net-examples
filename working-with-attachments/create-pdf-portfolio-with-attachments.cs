using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string outputPath = "portfolio.pdf";

        // Create an empty PDF document
        using (Document doc = new Document())
        {
            // Initialize the collection that holds portfolio files
            if (doc.Collection == null)
                doc.Collection = new Collection();

            // Files to add to the portfolio (ensure the files exist)
            string[] filesToAdd = { "file1.txt", "image.png" };
            foreach (string filePath in filesToAdd)
            {
                if (File.Exists(filePath))
                {
                    // Create a file specification for the attachment
                    var fileSpec = new FileSpecification(filePath, Path.GetFileName(filePath));
                    fileSpec.Contents = new MemoryStream(File.ReadAllBytes(filePath));
                    // Add the specification to the document's collection
                    doc.Collection.Add(fileSpec);
                }
            }

            // Save the portfolio PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Portfolio PDF saved to '{outputPath}'.");
    }
}
