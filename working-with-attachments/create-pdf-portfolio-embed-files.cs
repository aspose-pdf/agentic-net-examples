using System;
using System.IO;
using Aspose.Pdf; // Core PDF API

class Program
{
    static void Main()
    {
        // Input regular PDF
        const string inputPdf = "regular.pdf";

        // Output PDF that will become a portfolio
        const string outputPdf = "portfolio.pdf";

        // Files to embed into the portfolio
        string[] filesToEmbed = new string[]
        {
            "document1.docx",
            "image1.png",
            "spreadsheet1.xlsx"
        };

        // Verify that the source PDF and all files exist
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }

        foreach (var f in filesToEmbed)
        {
            if (!File.Exists(f))
            {
                Console.Error.WriteLine($"File to embed not found: {f}");
                return;
            }
        }

        try
        {
            // Load the regular PDF inside a using block for deterministic disposal
            using (Document doc = new Document(inputPdf))
            {
                // Add each file as an embedded file (PDF Portfolio)
                foreach (var filePath in filesToEmbed)
                {
                    // Use FileSpecification (core API) to embed files.
                    // Adding it to the document's EmbeddedFiles collection creates a portfolio entry.
                    doc.EmbeddedFiles.Add(new FileSpecification(filePath));
                }

                // Save the modified document; it now contains the embedded files as a PDF Portfolio
                doc.Save(outputPdf);
            }

            Console.WriteLine($"Portfolio created successfully: {outputPdf}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
