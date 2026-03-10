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
        const string outputPath = "output_accessible.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Access the tagged content interface
            ITaggedContent tagged = doc.TaggedContent;

            // Set language and title for accessibility
            tagged.SetLanguage("en-US");
            tagged.SetTitle(Path.GetFileNameWithoutExtension(inputPath));

            // Add a paragraph element to the structure tree
            StructureElement root = tagged.RootElement; // No cast needed
            ParagraphElement para = tagged.CreateParagraphElement();
            para.SetText("This document has been made accessible using Aspose.Pdf.");
            root.AppendChild(para); // AppendChild with a single argument

            // Save the tagged content changes to the PDF document
            tagged.Save();

            // Save the updated PDF file
            doc.Save(outputPath);
        }

        Console.WriteLine($"Accessible PDF saved to '{outputPath}'.");
    }
}