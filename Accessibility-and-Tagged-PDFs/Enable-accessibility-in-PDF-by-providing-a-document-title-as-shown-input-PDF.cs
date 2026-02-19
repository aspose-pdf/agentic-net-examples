using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Tagged; // Provides ITaggedContent for accessibility features

class Program
{
    static void Main(string[] args)
    {
        // Paths for the source PDF and the output PDF
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        // Ensure the source file exists before proceeding
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: Input file not found – {inputPath}");
            return;
        }

        // Load the existing PDF document
        Document pdfDocument = new Document(inputPath);

        // Set the document title (metadata) – this is used by assistive technologies
        pdfDocument.Info.Title = "Sample Document Title";

        // If the PDF supports tagged content, also set the title via the accessibility API
        // TaggedContent may be null for PDFs that are not tagged; the null‑conditional operator handles that safely
        pdfDocument.TaggedContent?.SetTitle("Sample Document Title");

        // Save the updated PDF
        pdfDocument.Save(outputPath);
    }
}