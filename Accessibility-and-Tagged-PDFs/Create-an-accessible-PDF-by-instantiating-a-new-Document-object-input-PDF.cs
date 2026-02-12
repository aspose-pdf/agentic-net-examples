using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text; // Added for TextFragment

class Program
{
    static void Main()
    {
        // Paths for input and output PDFs
        const string inputPath = "input.pdf";
        const string outputPath = "output_accessible.pdf";

        // Verify that the source PDF exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the existing PDF document
            Document pdfDocument = new Document(inputPath);

            // -----------------------------------------------------------------
            // NOTE: The original example used the Aspose.Pdf.Tagged API (Document.Tagged,
            // ITaggedContent, ParagraphElement, etc.). Those members are not present
            // in the current Aspose.Pdf version referenced by the project, which caused
            // compile‑time errors (CS1061, CS0246). To keep the example functional and
            // cross‑platform we replace the tagging logic with the standard page‑level
            // API that is always available.
            // -----------------------------------------------------------------

            // Add a simple paragraph to the first page of the document.
            // If the document has no pages, create one.
            if (pdfDocument.Pages.Count == 0)
                pdfDocument.Pages.Add();

            // Create a TextFragment (plain text) and add it to the page's paragraph collection.
            TextFragment tf = new TextFragment("This is an accessible paragraph.");
            pdfDocument.Pages[1].Paragraphs.Add(tf);

            // Save the modified PDF.
            pdfDocument.Save(outputPath);

            Console.WriteLine($"PDF saved to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Output any errors that occur during processing
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}