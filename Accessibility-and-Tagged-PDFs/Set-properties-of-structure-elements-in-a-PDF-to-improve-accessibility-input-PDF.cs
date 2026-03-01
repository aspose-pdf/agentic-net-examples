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
        const string outputPath = "accessible_output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Access tagged content (creates it if missing)
            ITaggedContent tagged = doc.TaggedContent;

            // Set document language and title for accessibility
            tagged.SetLanguage("en-US");
            tagged.SetTitle(Path.GetFileNameWithoutExtension(inputPath));

            // Get the root structure element (no cast needed)
            StructureElement root = tagged.RootElement;

            // Recursively set properties on existing structure elements
            SetPropertiesRecursive(root);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Accessible PDF saved to '{outputPath}'.");
    }

    // Recursively traverse the structure tree and set accessibility properties
    private static void SetPropertiesRecursive(StructureElement element)
    {
        // Set language for the current element
        element.Language = "en-US";

        // Specific handling based on element type
        switch (element)
        {
            case ParagraphElement para:
                // Ensure paragraph text is present (optional)
                // para.SetText(para.ActualText ?? ""); // Uncomment if you need to reset text
                break;

            case FigureElement fig:
                // Provide alternate text for images/figures
                if (string.IsNullOrWhiteSpace(fig.AlternativeText))
                {
                    fig.AlternativeText = "Descriptive alternate text for the figure.";
                }
                break;

            case HeaderElement header:
                // Example: set header language (already set above) or other header-specific properties
                break;

            // Add more cases as needed for other element types (TableElement, ListElement, etc.)
        }

        // Process child elements
        foreach (Element child in element.ChildElements)
        {
            if (child is StructureElement childStruct)
            {
                SetPropertiesRecursive(childStruct);
            }
        }
    }
}