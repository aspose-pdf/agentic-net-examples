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
        const string outputPath = "output_accessible.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF document
            using (Document doc = new Document(inputPath))
            {
                // Access the tagged content interface
                ITaggedContent tagged = doc.TaggedContent;

                // Set language and title for the tagged PDF
                tagged.SetLanguage("en-US");
                tagged.SetTitle(Path.GetFileNameWithoutExtension(inputPath));

                // Add a simple paragraph element to the structure tree
                StructureElement root = tagged.RootElement; // no cast required
                ParagraphElement paragraph = tagged.CreateParagraphElement();
                paragraph.SetText("This PDF has been made accessible.");
                root.AppendChild(paragraph); // AppendChild with a single argument

                // Persist the tagged content changes to the PDF
                tagged.Save();

                // Save the modified PDF document
                doc.Save(outputPath);
            }

            Console.WriteLine($"Accessible PDF saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}