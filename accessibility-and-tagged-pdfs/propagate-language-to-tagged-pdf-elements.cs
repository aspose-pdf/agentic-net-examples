using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Tagged;
using Aspose.Pdf.LogicalStructure;

class Program
{
    // Recursively set the Language property on a structure element and all its descendants.
    static void PropagateLanguage(StructureElement element, string language)
    {
        // Set language on the current element.
        element.Language = language;

        // Iterate over child elements. ChildElements returns an ElementList.
        foreach (Element child in element.ChildElements)
        {
            // Only StructureElement (or derived) types have the Language property.
            if (child is StructureElement se)
            {
                PropagateLanguage(se, language);
            }
        }
    }

    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_language_propagated.pdf";
        const string language   = "en-US";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document.
        using (Document doc = new Document(inputPath))
        {
            // Access tagged content API.
            ITaggedContent taggedContent = doc.TaggedContent;

            // Set the document-level language (optional, for completeness).
            taggedContent.SetLanguage(language);

            // Get the root structure element (no cast required).
            StructureElement root = taggedContent.RootElement;

            // Propagate the language to the root and all its children.
            PropagateLanguage(root, language);

            // Save the modified PDF.
            doc.Save(outputPath);
        }

        Console.WriteLine($"Language '{language}' propagated and saved to '{outputPath}'.");
    }
}