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
        const string outputPath = "output_tagged.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Access the tagged content interface
            ITaggedContent taggedContent = doc.TaggedContent;

            // Set the document title metadata (write‑only setter)
            taggedContent.SetTitle("My Accessible PDF Document");

            // Optionally set language for completeness
            taggedContent.SetLanguage("en-US");

            // Obtain the root structure element (no cast required)
            StructureElement root = taggedContent.RootElement;

            // Example: add a paragraph describing the title change
            ParagraphElement para = taggedContent.CreateParagraphElement();
            para.SetText("Document title has been set via the root structure element.");
            root.AppendChild(para);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Tagged PDF saved to '{outputPath}'.");
    }
}