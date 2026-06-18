using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Input PDF file path
        const string inputPdf = "input.pdf";

        // Output directory for extracted vector graphics (SVG files)
        const string outputDir = "VectorGraphics";
        Directory.CreateDirectory(outputDir);

        // Ensure the PDF file exists before processing
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Open the PDF document inside a using block to guarantee disposal
        using (Document pdfDoc = new Document(inputPdf))
        {
            // Retrieve and display basic metadata (author and title)
            Console.WriteLine($"Author: {pdfDoc.Info.Author}");
            Console.WriteLine($"Title : {pdfDoc.Info.Title}");

            // -----------------------------------------------------------------
            // Extract vector graphics by saving the document (or each page) as SVG.
            // SVG is a vector format, preserving paths, shapes, text etc.
            // -----------------------------------------------------------------

            // Option 1: Save the entire document as a single SVG file
            string fullSvgPath = Path.Combine(outputDir, "document.svg");
            SvgSaveOptions fullSvgOptions = new SvgSaveOptions();
            pdfDoc.Save(fullSvgPath, fullSvgOptions);
            Console.WriteLine($"Saved full document SVG to '{fullSvgPath}'");

            // Option 2: Save each page individually as separate SVG files
            for (int pageNumber = 1; pageNumber <= pdfDoc.Pages.Count; pageNumber++)
            {
                // Create a temporary document containing only the current page
                using (Document singlePageDoc = new Document())
                {
                    // Add the specific page to the new document
                    singlePageDoc.Pages.Add(pdfDoc.Pages[pageNumber]);

                    // Define the output SVG file name for this page
                    string pageSvgPath = Path.Combine(outputDir, $"page_{pageNumber}.svg");

                    // Save the single‑page document as SVG
                    SvgSaveOptions pageSvgOptions = new SvgSaveOptions();
                    singlePageDoc.Save(pageSvgPath, pageSvgOptions);
                    Console.WriteLine($"Saved page {pageNumber} SVG to '{pageSvgPath}'");
                }
            }

            // -----------------------------------------------------------------
            // (Optional) Extract text using TextAbsorber for additional documentation.
            // -----------------------------------------------------------------
            TextAbsorber absorber = new TextAbsorber();
            pdfDoc.Pages.Accept(absorber);
            string extractedText = absorber.Text;
            Console.WriteLine($"Extracted text length: {extractedText.Length}");
        }
    }
}