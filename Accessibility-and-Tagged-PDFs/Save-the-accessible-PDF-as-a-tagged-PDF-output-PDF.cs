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
                // Access tagged content interface
                ITaggedContent tagged = doc.TaggedContent;

                // Set language and title for the PDF
                tagged.SetLanguage("en-US");
                tagged.SetTitle(Path.GetFileNameWithoutExtension(inputPath));

                // Add a simple paragraph to the logical structure
                StructureElement root = tagged.RootElement; // no cast needed
                ParagraphElement para = tagged.CreateParagraphElement();
                para.SetText("This PDF has been made accessible and tagged.");
                root.AppendChild(para); // AppendChild with one argument

                // Persist changes to the tagged structure
                tagged.Save();

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