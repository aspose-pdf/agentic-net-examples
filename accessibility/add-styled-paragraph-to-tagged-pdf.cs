using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Tagged;
using Aspose.Pdf.LogicalStructure;
using Aspose.Pdf.Text; // FontStyles

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "tagged_output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        using (Document doc = new Document(inputPath))
        {
            ITaggedContent tagged = doc.TaggedContent;
            if (tagged == null)
            {
                Console.Error.WriteLine("Document does not support tagging.");
                return;
            }

            // optional metadata
            tagged.SetLanguage("en-US");
            tagged.SetTitle(Path.GetFileNameWithoutExtension(inputPath));

            StructureElement root = tagged.RootElement;

            // create a paragraph element
            ParagraphElement paragraph = tagged.CreateParagraphElement();
            paragraph.SetText("This paragraph is styled via StructureTextState.");

            // configure font size and style via the read‑only StructureTextState property
            paragraph.StructureTextState.FontSize = 14f;
            paragraph.StructureTextState.FontStyle = FontStyles.Bold;

            // attach the paragraph to the root of the structure tree
            root.AppendChild(paragraph);

            // save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Tagged PDF saved to '{outputPath}'.");
    }
}
