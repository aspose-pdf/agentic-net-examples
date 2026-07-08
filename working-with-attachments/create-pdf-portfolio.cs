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
            // Ensure the document has a collection (required for portfolio mode)
            if (doc.Collection == null)
                doc.Collection = new Collection();

            // List of files to embed in the portfolio
            string[] filesToAdd = { "file1.pdf", "file2.docx", "image.png" };

            // Add each existing file to the portfolio using FileSpecification
            foreach (string filePath in filesToAdd)
            {
                if (File.Exists(filePath))
                {
                    // Create a file specification for the attachment
                    var fileSpec = new FileSpecification(filePath, Path.GetFileName(filePath))
                    {
                        // Load the file content into the specification
                        Contents = new MemoryStream(File.ReadAllBytes(filePath))
                    };

                    // Add the specification to the document collection (portfolio)
                    doc.Collection.Add(fileSpec);
                }
            }

            // Save the portfolio PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Portfolio PDF saved to '{outputPath}'.");
    }
}
