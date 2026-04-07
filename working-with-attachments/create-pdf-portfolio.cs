using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Output PDF Portfolio file
        const string outputPath = "portfolio.pdf";

        // Create an empty PDF document
        using (Document doc = new Document())
        {
            // Ensure the document has a collection for portfolio files
            if (doc.Collection == null)
                doc.Collection = new Collection();

            // Optional: verify source files exist before adding them to the portfolio
            string[] filesToAdd = { "file1.txt", "file2.jpg", "file3.pdf" };
            foreach (string file in filesToAdd)
            {
                if (File.Exists(file))
                {
                    // Create a FileSpecification for each file and add it to the collection
                    var fileSpec = new FileSpecification(file, Path.GetFileName(file))
                    {
                        Contents = new MemoryStream(File.ReadAllBytes(file))
                    };
                    doc.Collection.Add(fileSpec);
                }
                else
                {
                    Console.Error.WriteLine($"Warning: '{file}' not found and will be skipped.");
                }
            }

            // Optional: set document metadata
            doc.Info.Title = "PDF Portfolio";

            // Save the portfolio PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF Portfolio created at '{outputPath}'.");
    }
}
