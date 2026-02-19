using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Tagged;

class EnableAccessibility
{
    static void Main()
    {
        // Input and output PDF file paths
        const string inputPath = "input.pdf";
        const string outputPath = "output_tagged.pdf";

        // Verify that the source PDF exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: Input file not found – {inputPath}");
            return;
        }

        try
        {
            // Load the existing PDF document
            Document pdfDocument = new Document(inputPath);

            // Obtain the tagged‑content interface
            ITaggedContent tagged = pdfDocument.TaggedContent;

            // Set basic accessibility properties
            tagged.SetLanguage("en-US");          // Natural language of the document
            tagged.SetTitle("Sample Tagged PDF"); // Document title

            // Example: create a simple paragraph element for each page
            // and attach it to the root structure element.
            for (int pageIndex = 1; pageIndex <= pdfDocument.Pages.Count; pageIndex++)
            {
                // Create a paragraph structure element
                var paragraph = tagged.CreateParagraphElement();

                // (Optional) set alternative text for the element
                // paragraph.AlternativeText = $"Page {pageIndex} content";

                // Append the paragraph to the root element of the logical structure
                tagged.RootElement.AppendChild(paragraph);
            }

            // Prepare the logical structure for saving
            tagged.PreSave();

            // Save the modified PDF (uses the provided document‑save rule)
            pdfDocument.Save(outputPath);

            Console.WriteLine($"Tagged PDF saved successfully to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}