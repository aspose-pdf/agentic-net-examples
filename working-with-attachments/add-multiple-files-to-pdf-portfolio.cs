using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;               // Core PDF API
using Aspose.Pdf.Facades;      // For advanced features if needed (not used here)

class PortfolioExample
{
    static void Main()
    {
        // List of file paths of various types to embed in the PDF portfolio
        List<string> filesToAdd = new List<string>
        {
            "sample.pdf",
            "image.png",
            "document.docx",
            "notes.txt"
        };

        // Create a new PDF document that will act as the portfolio container
        using (Document portfolioDoc = new Document())
        {
            // Ensure the EmbeddedFiles collection exists (it does by default)
            foreach (string filePath in filesToAdd)
            {
                // Skip missing files to avoid runtime errors
                if (!File.Exists(filePath))
                    continue;

                // Create a file specification for the current file
                FileSpecification fileSpec = new FileSpecification(filePath);

                // Add the file specification to the document's embedded files collection
                portfolioDoc.EmbeddedFiles.Add(fileSpec);
            }

            // Save the resulting PDF portfolio
            portfolioDoc.Save("PortfolioOutput.pdf");
        }

        Console.WriteLine("Portfolio PDF created successfully.");
    }
}