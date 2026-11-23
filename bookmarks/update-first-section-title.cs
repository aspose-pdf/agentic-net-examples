using System;
using System.IO;
using System.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Tagged;
using Aspose.Pdf.LogicalStructure;

class Program
{
    static void Main()
    {
        const string inputPath = "sample.pdf";
        const string outputPath = "childupdated.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Access tagged content (document must be tagged)
            ITaggedContent taggedContent = doc.TaggedContent;

            // Root of the structure tree
            var root = taggedContent.RootElement;

            // Find the HeaderElement whose text is "Chapter One"
            HeaderElement chapterOne = root
                .FindElements<HeaderElement>(true) // recursive search
                .FirstOrDefault(h => h.ActualText == "Chapter One");

            if (chapterOne == null)
            {
                Console.Error.WriteLine("Chapter One not found in the structure tree.");
                return;
            }

            // Get the first child of the chapter (e.g., a section)
            Element firstChild = chapterOne.ChildElements.FirstOrDefault();

            if (firstChild == null)
            {
                Console.Error.WriteLine("Chapter One has no child elements.");
                return;
            }

            // Update the accessible text of the first child.
            // The base Element type does not expose ActualText, so cast to a concrete type that does.
            switch (firstChild)
            {
                case HeaderElement header:
                    header.ActualText = "Section 1.1 Revised";
                    break;
                case ParagraphElement paragraph:
                    paragraph.ActualText = "Section 1.1 Revised";
                    break;
                case DivElement div:
                    div.ActualText = "Section 1.1 Revised";
                    break;
                default:
                    Console.Error.WriteLine("First child is of an unsupported type for text update.");
                    return;
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Updated PDF saved to '{outputPath}'.");
    }
}
