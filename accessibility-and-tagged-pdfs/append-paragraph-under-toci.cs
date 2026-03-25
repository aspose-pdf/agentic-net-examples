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
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        using (Document doc = new Document(inputPath))
        {
            ITaggedContent tagged = doc.TaggedContent;

            // Create a TOCI element and set its ActualText
            TOCIElement toci = tagged.CreateTOCIElement();
            toci.ActualText = "Table of contents entry";

            // Create a Paragraph element, set its ActualText, and attach it to the TOCI
            ParagraphElement para = tagged.CreateParagraphElement();
            para.ActualText = "Paragraph under TOCI";
            toci.AppendChild(para);

            // Append the TOCI element to the document root
            StructureElement root = tagged.RootElement;
            root.AppendChild(toci);

            doc.Save(outputPath);
        }

        Console.WriteLine($"Saved tagged PDF to '{outputPath}'.");
    }
}