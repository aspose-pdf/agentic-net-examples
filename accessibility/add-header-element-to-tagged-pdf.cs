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
        const string outputPath = "tagged_output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF; using ensures deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Obtain the tagged content interface
            ITaggedContent tagged = doc.TaggedContent;

            // Optional: set language and title for the tagged PDF
            tagged.SetLanguage("en-US");
            tagged.SetTitle(Path.GetFileNameWithoutExtension(inputPath));

            // Get the root of the structure tree (no cast required)
            StructureElement root = tagged.RootElement;

            // Create a level‑1 header element via the ITaggedContent factory
            HeaderElement header = tagged.CreateHeaderElement(1);
            header.SetText("Document Header");

            // Attach the header to the root element
            root.AppendChild(header);

            // Save the modified PDF; no PreSave() call is needed
            doc.Save(outputPath);
        }

        Console.WriteLine($"Tagged PDF saved to '{outputPath}'.");
    }
}