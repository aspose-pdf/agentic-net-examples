using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // List of files of various types to embed in the portfolio
        string[] filePaths = { "doc1.pdf", "image1.png", "report.docx", "notes.txt" };
        const string outputPath = "portfolio.pdf";

        // Create a new PDF document that will act as the portfolio container
        using (Document portfolio = new Document())
        {
            // A portfolio PDF must contain at least one page
            portfolio.Pages.Add();

            // Iterate over each file and add it to the EmbeddedFiles collection
            foreach (string path in filePaths)
            {
                if (!File.Exists(path))
                {
                    Console.Error.WriteLine($"File not found: {path}");
                    continue;
                }

                // Create a FileSpecification for the current file
                FileSpecification spec = new FileSpecification(path);

                // Optional: provide a description for the embedded file
                spec.Description = $"Embedded {Path.GetFileName(path)}";

                // Add the specification to the document's embedded file collection
                portfolio.EmbeddedFiles.Add(spec);
            }

            // Save the resulting PDF portfolio
            portfolio.Save(outputPath);
        }

        Console.WriteLine($"Portfolio created: {outputPath}");
    }
}