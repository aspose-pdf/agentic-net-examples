using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Tagged;
using Aspose.Pdf.LogicalStructure;
using Aspose.Pdf.Text; // for TextAbsorber fallback

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Determine if the PDF is tagged by checking the TaggedContent property
            if (doc.TaggedContent == null)
            {
                Console.WriteLine("Document is not tagged. Extracting raw text via TextAbsorber.");
                TextAbsorber absorber = new TextAbsorber();
                doc.Pages.Accept(absorber);
                Console.WriteLine(absorber.Text);
                return;
            }

            // Access the tagged content structure
            ITaggedContent taggedContent = doc.TaggedContent;
            StructureElement root = taggedContent.RootElement; // no cast required

            // Recursively traverse the structure tree and output textual content
            TraverseStructure(root, 0);
        }
    }

    // Recursive helper to walk the structure tree
    static void TraverseStructure(StructureElement element, int depth)
    {
        string indent = new string(' ', depth * 2);

        // ActualText holds the visible text for the element
        string visibleText = element.ActualText ?? string.Empty;
        // AlternativeText may hold alt text for images or figures
        string altText = element.AlternativeText ?? string.Empty;

        if (!string.IsNullOrEmpty(visibleText) || !string.IsNullOrEmpty(altText))
        {
            Console.WriteLine($"{indent}[{element.GetType().Name}] Text: '{visibleText}' Alt: '{altText}'");
        }

        // Iterate over child elements using the correct ChildElements property
        foreach (Element child in element.ChildElements)
        {
            if (child is StructureElement childStructure)
            {
                TraverseStructure(childStructure, depth + 1);
            }
        }
    }
}
