using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Tagged;

class Program
{
    static void Main(string[] args)
    {
        // Input and output PDF paths
        string inputPath = "input.pdf";
        string outputPath = "output_accessible.pdf";

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

            // Obtain the tagged‑content interface
            ITaggedContent tagged = pdfDocument.TaggedContent;

            // Optional: set document language and title for accessibility
            tagged.SetLanguage("en-US");
            tagged.SetTitle("Accessible PDF Example");

            // Get the root element of the logical structure tree
            var root = tagged.RootElement; // StructureElement resolved from Tagged namespace

            // -------------------------------------------------
            // Create a paragraph structure element
            // -------------------------------------------------
            var paragraph = tagged.CreateParagraphElement();
            paragraph.AlternativeText = "This paragraph provides an overview of the document content.";
            // Append the paragraph to the root element
            root.AppendChild(paragraph, true);

            // -------------------------------------------------
            // Create a figure structure element (e.g., for an image)
            // -------------------------------------------------
            var figure = tagged.CreateFigureElement();
            figure.AlternativeText = "Illustrative figure showing a diagram.";
            // Append the figure to the root element
            root.AppendChild(figure, true);

            // Prepare the tagged content for saving
            tagged.PreSave();
            tagged.Save();

            // Save the modified PDF to the output file
            pdfDocument.Save(outputPath);
            Console.WriteLine($"Accessible PDF saved to {outputPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error processing PDF: {ex.Message}");
        }
    }
}
