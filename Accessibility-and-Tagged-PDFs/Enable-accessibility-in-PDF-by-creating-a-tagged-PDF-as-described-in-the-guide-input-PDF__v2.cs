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
            using (Document doc = new Document(inputPath))
            {
                ITaggedContent taggedContent = doc.TaggedContent;
                taggedContent.SetLanguage("en-US");
                taggedContent.SetTitle(Path.GetFileNameWithoutExtension(inputPath));

                // StructureElement — NO explicit cast, direct assignment
                StructureElement root = taggedContent.RootElement;

                ParagraphElement para = taggedContent.CreateParagraphElement();
                para.SetText("This PDF has been made accessible.");  // use SetText()
                root.AppendChild(para);  // AppendChild with one argument

                doc.Save(outputPath);  // no PreSave() needed
            }
            Console.WriteLine($"Tagged PDF saved to '{outputPath}'.");
        }
        catch (Exception ex) { Console.Error.WriteLine($"Error: {ex.Message}"); }
    }
}