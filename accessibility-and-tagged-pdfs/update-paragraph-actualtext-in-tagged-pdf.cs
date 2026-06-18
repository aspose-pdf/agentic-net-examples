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
        const string correctedActualText = "Corrected paragraph actual text";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Access tagged content (if the PDF is tagged)
            ITaggedContent tagged = doc.TaggedContent;

            // Ensure the document has a structure tree
            if (tagged == null || tagged.RootElement == null)
            {
                Console.Error.WriteLine("Document does not contain tagged content.");
                return;
            }

            // Get the root of the structure tree
            StructureElement root = tagged.RootElement;

            // Find all paragraph elements in the document (recursive search)
            var paragraphs = root.FindElements<ParagraphElement>(true);

            // Locate the paragraph with the specified title (ActualText)
            foreach (ParagraphElement para in paragraphs)
            {
                // Compare the existing ActualText with the target title
                if (string.Equals(para.ActualText, targetTitle, StringComparison.OrdinalIgnoreCase))
                {
                    // Update the ActualText property with the corrected value
                    para.ActualText = correctedActualText;
                    Console.WriteLine("Paragraph ActualText updated.");
                    break; // Assuming only one match is needed
                }
            }

            // Save the modified PDF
            doc.Save(outputPath);
            Console.WriteLine($"Modified PDF saved to '{outputPath}'.");
        }
    }
}