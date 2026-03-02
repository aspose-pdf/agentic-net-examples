using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Tagged;               // ITaggedContent
using Aspose.Pdf.LogicalStructure;    // StructureElement, HeaderElement

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_with_headings.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the existing PDF
            using (Document doc = new Document(inputPath))
            {
                // Access tagged content (creates a tagged structure if none exists)
                ITaggedContent taggedContent = doc.TaggedContent;

                // Optional: set document language and title for accessibility
                taggedContent.SetLanguage("en-US");
                taggedContent.SetTitle(Path.GetFileNameWithoutExtension(inputPath));

                // Root element of the logical structure tree
                StructureElement root = taggedContent.RootElement;

                // Create a heading (HeaderElement) of level 1
                HeaderElement heading = taggedContent.CreateHeaderElement(1);
                heading.SetText("Document Heading Added by Aspose.Pdf");

                // Append the heading to the root of the structure tree
                root.AppendChild(heading);

                // Save the modified PDF
                doc.Save(outputPath);
            }

            Console.WriteLine($"PDF with heading saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}