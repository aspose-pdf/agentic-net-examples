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
            // Access tagged content (required for structure manipulation)
            ITaggedContent tagged = doc.TaggedContent;

            // Optionally set the document's default language to French
            tagged.SetLanguage("fr-FR");

            // Locate the specific structure element.
            // Example: find the first ParagraphElement that contains the word "Introduction"
            var paragraphs = tagged.RootElement.FindElements<ParagraphElement>(true);
            foreach (ParagraphElement para in paragraphs)
            {
                if (para.ActualText != null && para.ActualText.Contains("Introduction"))
                {
                    // Update the language of this element to French
                    para.Language = "fr-FR";
                    // If you have an element ID, you could match on para.ID instead
                }
            }

            // Save the modified PDF (PDF format is the default)
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with updated language to '{outputPath}'.");
    }
}