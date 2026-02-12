using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Tagged;
using Aspose.Pdf.Structure;

class Program
{
    static void Main()
    {
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

            // Obtain the tagged‑content interface
            ITaggedContent tagged = pdfDocument.TaggedContent;

            // Set language and title – essential for accessibility
            tagged.SetLanguage("en-US");
            tagged.SetTitle("Accessible PDF Example");

            // Create a paragraph structure element and provide alternate text
            var paragraph = tagged.CreateParagraphElement();
            paragraph.AlternativeText = "This is a sample paragraph for accessibility.";

            // Append the paragraph to the root element of the structure tree
            var root = tagged.RootElement; // RootElement derives from Element
            root.AppendChild(paragraph, true);

            // Prepare the tagged content before saving
            tagged.PreSave();

            // Save the modified PDF
            pdfDocument.Save(outputPath);

            Console.WriteLine($"Accessible PDF saved to: {outputPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error processing PDF: {ex.Message}");
        }
    }
}