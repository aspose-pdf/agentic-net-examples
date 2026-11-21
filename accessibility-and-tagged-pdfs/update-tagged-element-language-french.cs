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
        const string outputPath = "output_french.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Access tagged content (document must be tagged)
            ITaggedContent taggedContent = doc.TaggedContent;

            // Find the first paragraph element (as an example of a specific element)
            // FindElements<T>(true) performs a recursive search
            var paragraphs = taggedContent.RootElement.FindElements<ParagraphElement>(true);
            if (paragraphs.Count == 0)
            {
                Console.WriteLine("No paragraph elements found.");
                return;
            }

            // Update the language property of the selected element to French
            ParagraphElement targetElement = paragraphs[0];
            targetElement.Language = "fr-FR";   // Set language to French (ISO code)

            // Save the modified PDF (revalidation is implicit on save)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Document saved with updated language at '{outputPath}'.");
    }
}