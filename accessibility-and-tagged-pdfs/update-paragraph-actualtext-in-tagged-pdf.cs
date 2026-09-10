using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Tagged;               // ITaggedContent
using Aspose.Pdf.LogicalStructure;    // StructureElement, ParagraphElement

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";
        const string paragraphTitle = "Section 1 – Introduction"; // title to locate
        const string correctedActualText = "Section 1 – Introduction (updated)";

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
                // Access tagged content (structure tree)
                ITaggedContent tagged = doc.TaggedContent;

                // Ensure the document has a structure tree
                if (tagged == null || tagged.RootElement == null)
                {
                    Console.Error.WriteLine("Document does not contain tagged content.");
                    return;
                }

                // Find all paragraph elements in the structure tree (recursive search)
                StructureElement root = tagged.RootElement;
                var paragraphs = root.FindElements<ParagraphElement>(true);

                // Locate the paragraph whose ActualText matches the given title
                ParagraphElement targetParagraph = null;
                foreach (ParagraphElement para in paragraphs)
                {
                    // ActualText holds the original text of the structure element
                    if (para.ActualText != null && para.ActualText.Equals(paragraphTitle, StringComparison.OrdinalIgnoreCase))
                    {
                        targetParagraph = para;
                        break;
                    }
                }

                if (targetParagraph == null)
                {
                    Console.Error.WriteLine($"Paragraph with title \"{paragraphTitle}\" not found.");
                }
                else
                {
                    // Modify the ActualText for correction
                    targetParagraph.ActualText = correctedActualText;
                    Console.WriteLine("ActualText updated successfully.");
                }

                // Save the modified PDF
                doc.Save(outputPath);
                Console.WriteLine($"Modified PDF saved to '{outputPath}'.");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}