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
        const string targetTitle = "Section 1: Introduction";
        const string correctedActualText = "Corrected alternative text for introduction.";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        using (Document doc = new Document(inputPath))
        {
            ITaggedContent tagged = doc.TaggedContent;
            if (tagged == null || tagged.RootElement == null)
            {
                Console.WriteLine("Document does not contain tagged content.");
                doc.Save(outputPath);
                return;
            }

            var paragraphs = tagged.RootElement.FindElements<ParagraphElement>(true);
            bool found = false;
            foreach (var para in paragraphs)
            {
                if (!string.IsNullOrEmpty(para.ActualText) &&
                    para.ActualText.Equals(targetTitle, StringComparison.OrdinalIgnoreCase))
                {
                    para.ActualText = correctedActualText;
                    found = true;
                    break;
                }
            }

            if (!found)
                Console.WriteLine($"Paragraph with title '{targetTitle}' not found.");

            doc.Save(outputPath);
        }

        Console.WriteLine($"Modified PDF saved to '{outputPath}'.");
    }
}
