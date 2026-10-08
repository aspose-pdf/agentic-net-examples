using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // List of files of various types to be added to the portfolio
        string[] files = { "sample.pdf", "report.docx", "image.jpg", "data.xlsx" };
        const string outputPdf = "portfolio.pdf";

        // Verify that each file exists before proceeding
        foreach (var f in files)
        {
            if (!File.Exists(f))
            {
                Console.Error.WriteLine($"File not found: {f}");
                return;
            }
        }

        // Create a new PDF document that will serve as the portfolio container
        using (Document portfolioDoc = new Document())
        {
            // A portfolio PDF must contain at least one page; add a blank page
            portfolioDoc.Pages.Add();

            // Ensure the Collection object exists – it holds the embedded files for a portfolio
            if (portfolioDoc.Collection == null)
                portfolioDoc.Collection = new Collection();

            // Add each file to the portfolio in a single loop
            foreach (var filePath in files)
            {
                // Use the file name as a simple description for the embedded file
                string description = Path.GetFileName(filePath);

                // Create a FileSpecification for the file and load its contents into a memory stream
                var fileSpec = new FileSpecification(filePath, description)
                {
                    Contents = new MemoryStream(File.ReadAllBytes(filePath))
                };

                // Add the specification to the document's collection (portfolio)
                portfolioDoc.Collection.Add(fileSpec);
            }

            // Save the resulting PDF portfolio
            portfolioDoc.Save(outputPdf);
        }

        Console.WriteLine($"Portfolio created: {outputPdf}");
    }
}
