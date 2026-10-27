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
        const string language   = "en-US";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF and access its tagged content
        using (Document doc = new Document(inputPath))
        {
            ITaggedContent taggedContent = doc.TaggedContent;

            // Ensure the document has a structure tree; if not, enable tagging
            if (taggedContent == null || taggedContent.RootElement == null)
            {
                Console.Error.WriteLine("Document does not contain a structure tree.");
                return;
            }

            // Set the language attribute on the root element of the structure tree
            StructureElement root = taggedContent.RootElement;
            root.Language = language; // applies to the entire structure tree

            // Optionally also set the document-level language entry
            taggedContent.SetLanguage(language);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Language '{language}' added to structure tree. Saved as '{outputPath}'.");
    }
}