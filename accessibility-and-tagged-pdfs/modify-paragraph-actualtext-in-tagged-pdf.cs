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
        const string targetTitle = "Original Paragraph Title";
        const string correctedActualText = "Corrected paragraph actual text.";

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
                // Access tagged content (must be a tagged PDF)
                ITaggedContent tagged = doc.TaggedContent;

                // Get the root of the structure tree
                StructureElement root = tagged.RootElement;

                // Find all paragraph elements in the structure tree
                var paragraphs = root.FindElements<ParagraphElement>(true);

                bool found = false;
                foreach (ParagraphElement para in paragraphs)
                {
                    // Compare the visible text (ActualText) with the target title
                    if (string.Equals(para.ActualText, targetTitle, StringComparison.OrdinalIgnoreCase))
                    {
                        // Modify the ActualText property
                        para.ActualText = correctedActualText;
                        found = true;
                        break;
                    }
                }

                if (!found)
                {
                    Console.WriteLine($"Paragraph with title \"{targetTitle}\" not found.");
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