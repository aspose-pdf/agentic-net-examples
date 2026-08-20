using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;                     // Core API
using Aspose.Pdf.Facades;            // For FileSpecification if needed (also in Aspose.Pdf)

class PortfolioBuilder
{
    static void Main()
    {
        // Input files of various types to be added to the PDF portfolio
        List<string> filesToAdd = new List<string>
        {
            "document1.pdf",
            "image1.png",
            "report.docx",
            "presentation.pptx",
            "data.xlsx"
        };

        // Ensure all source files exist before proceeding
        foreach (string path in filesToAdd)
        {
            if (!File.Exists(path))
            {
                Console.Error.WriteLine($"File not found: {path}");
                return;
            }
        }

        // Create a new PDF document that will act as the portfolio container
        using (Document portfolio = new Document())
        {
            // The EmbeddedFiles collection holds the files inside the portfolio
            EmbeddedFileCollection embedded = portfolio.EmbeddedFiles;

            // Loop once and add each file to the collection
            foreach (string filePath in filesToAdd)
            {
                // Create a FileSpecification for the current file
                // The constructor automatically reads the file stream and sets the name
                FileSpecification spec = new FileSpecification(filePath);

                // Add the specification to the portfolio's embedded files
                embedded.Add(spec);
            }

            // Save the resulting PDF portfolio
            string outputPath = "portfolio.pdf";
            portfolio.Save(outputPath);
            Console.WriteLine($"Portfolio created: {outputPath}");
        }
    }
}