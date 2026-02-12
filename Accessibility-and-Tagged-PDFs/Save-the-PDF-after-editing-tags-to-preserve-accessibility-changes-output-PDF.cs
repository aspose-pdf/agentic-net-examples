using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Tagged;

class Program
{
    static void Main()
    {
        // Paths for the source PDF and the output PDF
        string inputPath = "input.pdf";
        string outputPath = "output.pdf";

        // Verify that the source file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the existing PDF document
        Document pdfDocument = new Document(inputPath);

        // Obtain the tagged content interface (required for accessibility changes)
        ITaggedContent tagged = pdfDocument.TaggedContent;

        // If the document does not contain tagged content, exit (or enable auto‑tagging via conversion)
        if (tagged == null)
        {
            Console.Error.WriteLine("The PDF does not contain tagged content.");
            return;
        }

        // Set the natural language and title of the PDF (accessibility metadata)
        tagged.SetLanguage("en-US");
        tagged.SetTitle("Sample Tagged PDF");

        // Create a new paragraph structure element and attach it to the root of the structure tree
        var paragraph = tagged.CreateParagraphElement();
        tagged.RootElement.AppendChild(paragraph);

        // Prepare the tagged content for saving (updates structure tree, etc.)
        tagged.PreSave();

        // Save the tagged content back into the PDF document
        tagged.Save();

        // Save the modified PDF to the output file (uses the document-save rule)
        pdfDocument.Save(outputPath);
    }
}