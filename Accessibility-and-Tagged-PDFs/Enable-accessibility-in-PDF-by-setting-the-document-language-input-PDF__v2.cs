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
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            using (Document doc = new Document(inputPath))
            {
                // Access tagged content and set the document language
                ITaggedContent taggedContent = doc.TaggedContent;
                taggedContent.SetLanguage("en-US"); // set language identifier

                // Optional: set a title for the tagged PDF
                taggedContent.SetTitle(Path.GetFileNameWithoutExtension(inputPath));

                // Optional: add a simple paragraph to demonstrate tagged structure
                StructureElement root = taggedContent.RootElement;
                ParagraphElement para = taggedContent.CreateParagraphElement();
                para.SetText("This PDF has been made accessible.");
                root.AppendChild(para);

                // Save the modified PDF
                doc.Save(outputPath);
            }

            Console.WriteLine($"Language set and PDF saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}