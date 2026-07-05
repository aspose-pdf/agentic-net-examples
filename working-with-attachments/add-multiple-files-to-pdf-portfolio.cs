using System;
using System.IO;
using Aspose.Pdf; // Document, FileSpecification, EmbeddedFileCollection

class Program
{
    static void Main()
    {
        // Folder containing files of various types to be added to the PDF portfolio
        const string sourceFolder = "FilesToEmbed";
        // Output PDF portfolio file
        const string outputPdf = "portfolio.pdf";

        if (!Directory.Exists(sourceFolder))
        {
            Console.Error.WriteLine($"Source folder not found: {sourceFolder}");
            return;
        }

        // Create a new empty PDF document (portfolio)
        using (Document doc = new Document())
        {
            // Access the embedded files collection of the document
            EmbeddedFileCollection embeddedFiles = doc.EmbeddedFiles;

            // Loop through all files in the source folder and add each to the portfolio
            foreach (string filePath in Directory.GetFiles(sourceFolder))
            {
                // Create a file specification for the current file
                FileSpecification fileSpec = new FileSpecification(filePath);
                // Add the file specification to the embedded files collection
                embeddedFiles.Add(fileSpec);
            }

            // Save the resulting PDF portfolio
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Portfolio created: {outputPdf}");
    }
}