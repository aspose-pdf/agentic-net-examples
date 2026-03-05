using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Tagged;
using Aspose.Pdf.LogicalStructure;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "tagged_output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the source PDF inside a using block for deterministic disposal
            using (Document doc = new Document(inputPath))
            {
                // Obtain the tagged content interface
                ITaggedContent tagged = doc.TaggedContent;

                // Set language and title for the tagged PDF
                tagged.SetLanguage("en-US");
                tagged.SetTitle(Path.GetFileNameWithoutExtension(inputPath));

                // Example: add a paragraph element to the root of the structure tree
                StructureElement root = tagged.RootElement;
                ParagraphElement paragraph = tagged.CreateParagraphElement();
                paragraph.SetText("This PDF has been made accessible.");
                root.AppendChild(paragraph); // AppendChild with a single argument

                // Save the tagged content changes back into the PDF document
                tagged.Save();

                // Finally, save the PDF file (output remains a tagged PDF)
                doc.Save(outputPath);
            }

            Console.WriteLine($"Tagged PDF saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}