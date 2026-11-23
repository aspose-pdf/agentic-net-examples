using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Tagged;
using Aspose.Pdf.LogicalStructure;

class Program
{
    static void Main()
    {
        const string inputPath = "input_tagged.pdf";
        const string outputPath = "output_custom_tag.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF (must be a tagged PDF)
        using (Document doc = new Document(inputPath))
        {
            // Verify that the document actually contains a tag structure
            if (doc.TaggedContent == null)
            {
                Console.WriteLine("The document is not tagged. No structure tree to modify.");
                return;
            }

            // Access the tagged content interface
            ITaggedContent tagged = doc.TaggedContent;

            // Get the root element of the structure tree
            StructureElement root = tagged.RootElement;

            // Create a new paragraph element
            ParagraphElement para = tagged.CreateParagraphElement();
            para.SetText("This paragraph has a custom tag.");

            // Assign a custom tag name to the element
            para.SetTag("MyCustomTag");

            // Attach the new element to the root of the structure tree
            root.AppendChild(para);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with custom tag to '{outputPath}'.");
    }
}
