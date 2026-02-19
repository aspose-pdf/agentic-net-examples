using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Tagged;

class Program
{
    static void Main(string[] args)
    {
        // Expect two arguments: input PDF path and output PDF path
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <input-pdf> <output-pdf>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Validate input file existence
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: Input file not found – {inputPath}");
            return;
        }

        try
        {
            // Load the existing PDF document
            Document pdfDocument = new Document(inputPath);

            // Access the tagged content interface
            ITaggedContent tagged = pdfDocument.TaggedContent;

            // Set document language and title for accessibility
            tagged.SetLanguage("en-US");
            tagged.SetTitle("Accessible PDF Document");

            // Example: create a simple paragraph element and add it to the root
            // (In real scenarios you would associate this element with actual page content)
            var paragraph = tagged.CreateParagraphElement();
            // The ParagraphElement does not expose a direct Text property.
            // To associate visible text, you would normally add a marked-content block.
            // Here we simply attach the element to the root to ensure a logical structure exists.
            tagged.RootElement.AppendChild(paragraph);

            // Prepare the tagged structure for saving
            tagged.PreSave();

            // Save the modified PDF with tagging enabled
            pdfDocument.Save(outputPath);

            Console.WriteLine($"Tagged PDF saved successfully to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}