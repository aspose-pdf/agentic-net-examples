using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Ensure the document has at least five pages
            if (doc.Pages.Count < 5)
            {
                Console.Error.WriteLine("The document does not contain a page 5.");
                return;
            }

            // Get page five (1‑based indexing)
            Page pageFive = doc.Pages[5];

            // Locate the first FreeTextAnnotation on page five
            FreeTextAnnotation freeText = null;
            foreach (Annotation ann in pageFive.Annotations)
            {
                if (ann is FreeTextAnnotation fta)
                {
                    freeText = fta;
                    break;
                }
            }

            if (freeText == null)
            {
                Console.Error.WriteLine("No FreeTextAnnotation found on page 5.");
                return;
            }

            // Add a HideAction to make the annotation invisible while keeping its data
            // The second parameter 'true' sets the annotation to hidden.
            freeText.Actions.Add(new HideAction(freeText, true));

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"FreeTextAnnotation on page 5 has been hidden. Saved to '{outputPath}'.");
    }
}