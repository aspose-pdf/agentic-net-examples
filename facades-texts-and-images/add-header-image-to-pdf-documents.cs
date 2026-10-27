using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main(string[] args)
    {
        // Path to the header image that will be placed on each page.
        const string headerImagePath = "header.png";

        if (!File.Exists(headerImagePath))
        {
            Console.Error.WriteLine($"Header image not found: {headerImagePath}");
            return;
        }

        // List of PDF files to process. If command‑line arguments are supplied, use them;
        // otherwise, process all PDFs in the current directory.
        string[] pdfFiles = args.Length > 0
            ? args
            : Directory.GetFiles(Directory.GetCurrentDirectory(), "*.pdf");

        foreach (string pdfPath in pdfFiles)
        {
            if (!File.Exists(pdfPath))
            {
                Console.Error.WriteLine($"PDF not found: {pdfPath}");
                continue;
            }

            // Build output file name by appending "_header" before the extension.
            string outputPath = Path.Combine(
                Path.GetDirectoryName(pdfPath) ?? string.Empty,
                Path.GetFileNameWithoutExtension(pdfPath) + "_header.pdf");

            // Load the PDF inside a using block to ensure deterministic disposal.
            using (Document doc = new Document(pdfPath))
            {
                // Create an image stamp for the header.
                ImageStamp headerStamp = new ImageStamp(headerImagePath)
                {
                    // Place the image at the top centre of each page.
                    Background = false,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Top,
                    // Distance from the top edge of the page.
                    TopMargin = 10
                };

                // Apply the stamp to every page (Aspose.Pdf uses 1‑based indexing).
                for (int pageNumber = 1; pageNumber <= doc.Pages.Count; pageNumber++)
                {
                    doc.Pages[pageNumber].AddStamp(headerStamp);
                }

                // Save the modified PDF.
                doc.Save(outputPath);
            }

            Console.WriteLine($"Processed '{pdfPath}' → '{outputPath}'");
        }
    }
}
