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
        const string outputPath = "output_tagged.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the existing PDF
            using (Document doc = new Document(inputPath))
            {
                // Access tagged content (creates a tagged structure if none exists)
                ITaggedContent taggedContent = doc.TaggedContent;

                // Set language and title for accessibility
                taggedContent.SetLanguage("en-US");
                taggedContent.SetTitle(Path.GetFileNameWithoutExtension(inputPath));

                // Root element of the structure tree
                StructureElement root = taggedContent.RootElement;

                // Create an illustration (use FigureElement as the illustration element)
                FigureElement illustration = taggedContent.CreateFigureElement();
                illustration.AlternativeText = "Descriptive text for the illustration.";
                // Insert the illustration before adding new paragraphs
                root.AppendChild(illustration);

                // Example paragraphs that follow the illustration
                ParagraphElement para1 = taggedContent.CreateParagraphElement();
                para1.SetText("This is the first paragraph after the illustration.");
                root.AppendChild(para1);

                ParagraphElement para2 = taggedContent.CreateParagraphElement();
                para2.SetText("This is the second paragraph following the illustration.");
                root.AppendChild(para2);

                // Save the modified PDF (no PreSave call needed)
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