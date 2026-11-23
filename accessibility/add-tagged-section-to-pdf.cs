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
        const string outputPath = "output_tagged.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            using (Document doc = new Document(inputPath))
            {
                // Access the tagged content of the PDF
                ITaggedContent tagged = doc.TaggedContent;

                // Optional: set language and title for the document
                tagged.SetLanguage("en-US");
                tagged.SetTitle(Path.GetFileNameWithoutExtension(inputPath));

                // Get the root element of the structure tree
                StructureElement root = tagged.RootElement;

                // Create a new section (DivElement) and attach it to the root
                DivElement section = tagged.CreateDivElement();
                root.AppendChild(section);

                // Create a header element (H1) and add it to the section
                HeaderElement header = tagged.CreateHeaderElement(1);
                header.SetText("Section Title");
                section.AppendChild(header);

                // Create a paragraph element and add it to the section
                ParagraphElement paragraph = tagged.CreateParagraphElement();
                paragraph.SetText("This is a new paragraph added to the PDF for accessibility.");
                section.AppendChild(paragraph);

                // Save the modified PDF
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