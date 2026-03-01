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

        if (!File.Exists(inputPath)) { Console.Error.WriteLine($"Not found: {inputPath}"); return; }

        try
        {
            // Load the existing PDF document
            using (Document doc = new Document(inputPath))
            {
                // Access the tagged content interface
                ITaggedContent taggedContent = doc.TaggedContent;

                // Set language and title for accessibility
                taggedContent.SetLanguage("en-US");
                taggedContent.SetTitle(Path.GetFileNameWithoutExtension(inputPath));

                // Get the root structure element (no cast needed)
                StructureElement root = taggedContent.RootElement;

                // Create a paragraph element and set its text
                ParagraphElement para = taggedContent.CreateParagraphElement();
                para.SetText("This PDF has been made accessible.");

                // Append the paragraph to the root element
                root.AppendChild(para);  // AppendChild with one argument

                // Save the modified PDF (no PreSave required)
                doc.Save(outputPath);
            }
            Console.WriteLine($"Tagged PDF saved to '{outputPath}'.");
        }
        catch (Exception ex) { Console.Error.WriteLine($"Error: {ex.Message}"); }
    }
}