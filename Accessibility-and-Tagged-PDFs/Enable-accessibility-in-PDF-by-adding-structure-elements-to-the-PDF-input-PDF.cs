using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Tagged;
using Aspose.Pdf.LogicalStructure;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "tagged_output.pdf";

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
                // Access the tagged content interface
                ITaggedContent taggedContent = doc.TaggedContent;

                // Set language and title for the tagged PDF
                taggedContent.SetLanguage("en-US");
                taggedContent.SetTitle(Path.GetFileNameWithoutExtension(inputPath));

                // Get the root structure element (no cast needed)
                StructureElement root = taggedContent.RootElement;

                // Create a paragraph element and set its text
                ParagraphElement para = taggedContent.CreateParagraphElement();
                para.SetText("This PDF has been made accessible.");

                // Attach the paragraph to the root of the structure tree
                root.AppendChild(para);

                // Save the modified PDF (no PreSave call required)
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