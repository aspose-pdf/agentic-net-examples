using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Input and output file paths
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        // Verify that the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Open the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // -----------------------------------------------------------------
            // Basic information
            // -----------------------------------------------------------------
            Console.WriteLine($"Pages: {doc.Pages.Count}");
            Console.WriteLine($"Author: {doc.Info.Author}");
            Console.WriteLine($"Title : {doc.Info.Title}");
            Console.WriteLine($"Subject: {doc.Info.Subject}");

            // -----------------------------------------------------------------
            // Extract all text using TextAbsorber (the correct API for text extraction)
            // -----------------------------------------------------------------
            TextAbsorber absorber = new TextAbsorber();
            // Optional: set extraction options (e.g., pure formatting)
            absorber.ExtractionOptions = new TextExtractionOptions(TextExtractionOptions.TextFormattingMode.Pure);
            doc.Pages.Accept(absorber);
            string extractedText = absorber.Text;
            Console.WriteLine($"Extracted text length: {extractedText.Length}");

            // -----------------------------------------------------------------
            // Add a simple text annotation to the first page
            // -----------------------------------------------------------------
            if (doc.Pages.Count >= 1)
            {
                Page firstPage = doc.Pages[1]; // 1‑based indexing

                // Fully qualified rectangle to avoid ambiguity with System.Drawing.Rectangle
                Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);

                // Create the annotation
                TextAnnotation txtAnn = new TextAnnotation(firstPage, rect)
                {
                    Title    = "Note",
                    Contents = "This is a sample text annotation.",
                    Color    = Aspose.Pdf.Color.Yellow,
                    Open     = true,
                    Icon     = TextIcon.Note
                };

                // Add the annotation to the page
                firstPage.Annotations.Add(txtAnn);
            }

            // -----------------------------------------------------------------
            // Save the modified document (no SaveOptions needed for PDF output)
            // -----------------------------------------------------------------
            doc.Save(outputPath);
        }

        Console.WriteLine($"Processing completed. Output saved to '{outputPath}'.");
    }
}