using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Output PDF that will contain the portfolio
        const string outputPdf = "portfolio.pdf";

        // List of files of various types to be added to the portfolio
        List<string> filePaths = new List<string>
        {
            "sample.pdf",
            "image.png",
            "document.docx",
            "notes.txt"
        };

        // Create an empty PDF document (the portfolio container)
        using (Document doc = new Document())
        {
            // Get the embedded files collection (the portfolio)
            EmbeddedFileCollection portfolio = doc.EmbeddedFiles;

            // Loop through each file and add it to the portfolio
            foreach (string path in filePaths)
            {
                if (!File.Exists(path))
                {
                    Console.Error.WriteLine($"File not found: {path}");
                    continue;
                }

                // Create a file specification for the current file
                FileSpecification fileSpec = new FileSpecification(path)
                {
                    // Optional: set a human‑readable description
                    Description = Path.GetFileName(path)
                };

                // Add the file specification to the portfolio using the file name as the key
                portfolio.Add(Path.GetFileName(path), fileSpec);
            }

            // Save the PDF containing the portfolio
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Portfolio PDF saved to '{outputPdf}'.");
    }
}