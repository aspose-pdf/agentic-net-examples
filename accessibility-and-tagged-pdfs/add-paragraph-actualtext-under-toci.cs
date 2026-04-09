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

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Access the tagged content API
            ITaggedContent tagged = doc.TaggedContent;

            // Ensure the document has a root element
            StructureElement root = tagged.RootElement;

            // Create a TOCI (Table of Contents Item) element
            TOCIElement toci = tagged.CreateTOCIElement();

            // Append the TOCI element to the root
            root.AppendChild(toci); // one‑argument form

            // Create a Paragraph element
            ParagraphElement paragraph = tagged.CreateParagraphElement();

            // Set the ActualText for accessibility
            paragraph.ActualText = "This is the descriptive paragraph under the TOCI element.";

            // Append the paragraph under the TOCI element
            toci.AppendChild(paragraph); // one‑argument form

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with TOCI and paragraph: {outputPath}");
    }
}