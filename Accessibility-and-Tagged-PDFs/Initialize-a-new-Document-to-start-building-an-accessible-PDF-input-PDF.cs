using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Tagged; // Provides ITaggedContent for accessibility features

class Program
{
    static void Main(string[] args)
    {
        // Path to the existing PDF that will be made accessible
        const string inputPath = "input.pdf";
        // Path where the accessible PDF will be saved
        const string outputPath = "output_accessible.pdf";

        // Ensure the source PDF exists before attempting to load it
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: Input file not found – {inputPath}");
            return;
        }

        // Load the existing PDF document
        Document pdfDocument = new Document(inputPath);

        // Obtain the tagged‑content interface (used for accessibility)
        ITaggedContent tagged = pdfDocument.TaggedContent;

        // Set basic accessibility metadata
        tagged.SetLanguage("en-US");          // Natural language of the document
        tagged.SetTitle("Accessible PDF Demo"); // Title for assistive technologies

        // Create a simple paragraph element and attach it to the root of the structure tree
        var paragraph = tagged.CreateParagraphElement();
        tagged.RootElement.AppendChild(paragraph);

        // Finalize the tagged structure before saving
        tagged.PreSave();

        // Save the modified PDF (uses the provided document-save rule)
        pdfDocument.Save(outputPath);

        Console.WriteLine($"Accessible PDF successfully saved to '{outputPath}'.");
    }
}