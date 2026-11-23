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
        const string outputFolder = "ExtractedElements";

        // Verify input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Ensure output directory exists
        Directory.CreateDirectory(outputFolder);

        // Load PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Check if the PDF is tagged by inspecting TaggedContent (IsTagged does not exist)
            if (doc.TaggedContent == null)
            {
                Console.WriteLine("Document is not tagged. No structure to extract.");
                return;
            }

            // Access tagged content via the correct API
            ITaggedContent tagged = doc.TaggedContent;
            StructureElement root = tagged.RootElement; // No cast needed

            int elementIndex = 0;
            // Recursively walk the structure tree and write each element's text
            TraverseAndSave(root, outputFolder, ref elementIndex);
        }

        Console.WriteLine("Extraction completed.");
    }

    // Recursively processes a StructureElement and its children
    static void TraverseAndSave(StructureElement element, string folder, ref int index)
    {
        // Prefer ActualText; if missing, use AlternativeText; otherwise ignore empty elements
        string text = element.ActualText ?? element.AlternativeText ?? string.Empty;

        if (!string.IsNullOrWhiteSpace(text))
        {
            // Create a unique filename per element
            string fileName = Path.Combine(folder, $"Element_{index:D4}_{element.GetType().Name}.txt");
            File.WriteAllText(fileName, text);
            index++;
        }

        // Iterate over child elements using the correct ChildElements property
        foreach (Element child in element.ChildElements)
        {
            if (child is StructureElement childStruct)
            {
                TraverseAndSave(childStruct, folder, ref index);
            }
        }
    }
}
