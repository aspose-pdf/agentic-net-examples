using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // List of files of various types to be added to the portfolio
        string[] files = { "doc1.pdf", "image1.jpg", "report.docx", "presentation.pptx" };
        const string outputPdf = "portfolio.pdf";

        // Verify that all source files exist before proceeding
        foreach (var f in files)
        {
            if (!File.Exists(f))
            {
                Console.Error.WriteLine($"File not found: {f}");
                return;
            }
        }

        // Create the PDF document that will hold the portfolio
        using (Document portfolioDoc = new Document())
        {
            // Portfolio requires at least one page; add a blank page
            portfolioDoc.Pages.Add();

            // Ensure the collection for embedded files is initialized
            if (portfolioDoc.Collection == null)
                portfolioDoc.Collection = new Collection();

            // Add each file to the portfolio in a single loop
            foreach (string filePath in files)
            {
                // Create a FileSpecification for the file
                var fileSpec = new FileSpecification(filePath, Path.GetFileName(filePath))
                {
                    // Load the file contents into a memory stream
                    Contents = new MemoryStream(File.ReadAllBytes(filePath))
                };

                // Add the specification to the document's collection (portfolio)
                portfolioDoc.Collection.Add(fileSpec);
            }

            // Save the resulting PDF containing the embedded files
            portfolioDoc.Save(outputPdf);
        }

        Console.WriteLine($"Portfolio PDF created at '{outputPdf}'.");
    }
}
