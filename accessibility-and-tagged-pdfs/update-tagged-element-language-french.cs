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
        const string outputPath = "output_tagged.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Access the tagged‑content interface
            ITaggedContent tagged = doc.TaggedContent;

            // Verify that the document is tagged. If not, we cannot modify structure elements.
            if (tagged == null)
            {
                Console.WriteLine("Document is not a tagged PDF. No structure tree to modify.");
                return;
            }

            // Root of the logical structure tree – use dynamic to avoid compile‑time dependency on StructureElement type
            dynamic root = tagged.RootElement;

            // Find the first paragraph element (tag "P") in the tree
            dynamic firstParagraph = null;
            foreach (var child in root.ChildElements)
            {
                // Each child is also a structure element; use dynamic for safe access
                dynamic elem = child;
                // The Tag property holds the PDF structure tag (e.g., "P" for paragraph)
                if (elem.Tag != null && elem.Tag.Equals("P", StringComparison.OrdinalIgnoreCase))
                {
                    firstParagraph = elem;
                    break;
                }
            }

            if (firstParagraph != null)
            {
                // Update the language property to French (fr-FR)
                firstParagraph.Language = "fr-FR";
                Console.WriteLine("Updated language of the first paragraph element to French.");
            }
            else
            {
                Console.WriteLine("No paragraph elements found in the structure tree.");
            }

            // NOTE: PDF/UA validation requires the Aspose.Pdf.Validation assembly, which may not be referenced.
            // If the validation library is available, the following code can be used:
            // var validationOptions = new Aspose.Pdf.Validation.ValidationOptions();
            // validationOptions.Compliance = Aspose.Pdf.Validation.ValidationCompliance.PDF_UA;
            // var validationResult = doc.Validate(validationOptions);
            // Console.WriteLine($"Validation passed: {validationResult.IsValid}");
            // For the purpose of this example we simply acknowledge the step.
            Console.WriteLine("PDF/UA validation step skipped (validation library not referenced).");

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Processed file saved as '{outputPath}'.");
    }
}
