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

        // Load the PDF inside a using block for proper disposal
        using (Document doc = new Document(inputPath))
        {
            // Access the tagged content API
            ITaggedContent tagged = doc.TaggedContent;

            // Optionally set the document title (metadata)
            tagged.SetTitle("Accessible PDF with Paragraph Title");

            // Get the root structure element of the tagged PDF
            StructureElement root = tagged.RootElement;

            // Create a new paragraph structure element
            ParagraphElement para = tagged.CreateParagraphElement();

            // Set the visible text of the paragraph
            para.SetText("This paragraph provides a concise summary of the document.");

            // Set the title property on the paragraph to act as a summary
            para.Title = "Summary Paragraph";

            // Attach the paragraph to the root element
            root.AppendChild(para);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to '{outputPath}'.");
    }
}