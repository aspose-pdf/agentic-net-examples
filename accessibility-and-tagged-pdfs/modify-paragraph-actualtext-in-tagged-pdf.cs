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
        const string targetTitle = "Original Title";
        const string correctedText = "Corrected Title";

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
                Console.Error.WriteLine("Document is not tagged.");
                return;
            }

            // Find all paragraph elements recursively
            var paragraphs = tagged.RootElement.FindElements<ParagraphElement>(true);
            foreach (ParagraphElement para in paragraphs)
            {
                // Locate the paragraph by its current text (ActualText)
                if (string.Equals(para.ActualText, targetTitle, StringComparison.OrdinalIgnoreCase))
                {
                    // Modify the paragraph text
                    para.SetText(correctedText);
                    break;
                }
            }

            doc.Save(outputPath);
        }

        Console.WriteLine($"Modified PDF saved to '{outputPath}'.");
    }
}