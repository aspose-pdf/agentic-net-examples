using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Tagged;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_tagged.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        using (Document doc = new Document(inputPath))
        {
            // Ensure the PDF is tagged
            if (doc.TaggedContent == null)
            {
                Console.WriteLine("The document is not a tagged PDF.");
                return;
            }

            ITaggedContent taggedContent = doc.TaggedContent;

            // Access the root element dynamically to avoid compile‑time dependency on StructureElement
            dynamic root = taggedContent.RootElement;

            // Set a default language if none is defined
            if (string.IsNullOrEmpty((string)root.Language))
                root.Language = "en-US"; // example default language

            // Propagate the language attribute to all descendants
            PropagateLanguage(root, root.Language);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Language propagated and saved to '{outputPath}'.");
    }

    // Recursively sets the Language property on each structure element using dynamic typing
    static void PropagateLanguage(dynamic element, string language)
    {
        // Apply language to the current element
        element.Language = language;

        // Recurse through child elements if they exist
        if (element.ChildElements != null)
        {
            foreach (var child in element.ChildElements)
            {
                PropagateLanguage(child, language);
            }
        }
    }
}