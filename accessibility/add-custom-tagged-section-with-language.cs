using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Tagged;
using Aspose.Pdf.LogicalStructure;

class Program
{
    static void Main()
    {
        const string inputPath = "tagged_input.pdf";
        const string outputPath = "tagged_output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the existing tagged PDF
        using (Document doc = new Document(inputPath))
        {
            // Access the tagged content interface
            ITaggedContent taggedContent = doc.TaggedContent;

            // Optional: set document language (applies to the whole PDF)
            taggedContent.SetLanguage("en-US");

            // Get the root of the structure tree
            StructureElement root = taggedContent.RootElement;

            // Create a new section element (using DivElement as a generic container)
            DivElement section = taggedContent.CreateDivElement();

            // Set a custom tag name for the new element using the proper setter method
            section.SetTag("MyCustomSection");

            // Set the language for this specific section
            section.Language = "fr-FR";

            // Add some accessible text to the section
            ParagraphElement para = taggedContent.CreateParagraphElement();
            para.SetText("Ceci est une section accessible avec une langue spécifique.");
            section.AppendChild(para);

            // Attach the new section to the root of the structure tree
            root.AppendChild(section);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Tagged PDF saved to '{outputPath}'.");
    }
}
