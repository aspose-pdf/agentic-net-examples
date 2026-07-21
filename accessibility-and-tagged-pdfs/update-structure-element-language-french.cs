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

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Access the tagged content API
            ITaggedContent tagged = doc.TaggedContent;

            // OPTIONAL: set the document language (overall) to French
            tagged.SetLanguage("fr-FR");

            // Locate the specific structure element you want to modify.
            // For illustration, we pick the first ParagraphElement in the structure tree.
            StructureElement root = tagged.RootElement;
            ParagraphElement targetParagraph = null;

            // FindElements<T>(true) performs a deep search for the specified type.
            foreach (ParagraphElement para in root.FindElements<ParagraphElement>(true))
            {
                targetParagraph = para;
                break; // take the first match
            }

            if (targetParagraph != null)
            {
                // Update the language of the selected structure element to French.
                targetParagraph.Language = "fr-FR";

                // If you need to revalidate compliance (e.g., PDF/UA), you would invoke the
                // appropriate validation API here. Aspose.Pdf provides a Validate method
                // on Document for PDF/UA compliance. The call is shown for completeness,
                // but it is optional depending on your validation requirements.
                // var validationResult = doc.Validate(); // Uncomment if validation is needed
            }
            else
            {
                Console.WriteLine("No ParagraphElement found in the tagged structure.");
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Document saved with updated language at '{outputPath}'.");
    }
}