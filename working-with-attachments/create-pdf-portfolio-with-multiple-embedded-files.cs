using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // List of files of various types to be added to the PDF portfolio
        var filePaths = new List<string>
        {
            "sample.pdf",
            "image.jpg",
            "document.docx",
            "presentation.pptx"
        };

        const string outputPdf = "portfolio.pdf";

        // Create a new PDF document (wrapped in using for deterministic disposal)
        using (Document doc = new Document())
        {
            // A portfolio requires at least one page; add an empty page
            doc.Pages.Add();

            // Loop through each file and add it to the embedded files collection
            foreach (string path in filePaths)
            {
                if (!File.Exists(path))
                {
                    Console.Error.WriteLine($"File not found: {path}");
                    continue;
                }

                // Use the file name as the key inside the portfolio
                string key = Path.GetFileName(path);

                // Create a FileSpecification for the file (adds the file as an embedded object)
                FileSpecification fileSpec = new FileSpecification(path);

                // Add the file to the portfolio
                doc.EmbeddedFiles.Add(key, fileSpec);
            }

            // Save the resulting PDF containing the portfolio
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Portfolio PDF created at '{outputPdf}'.");
    }
}