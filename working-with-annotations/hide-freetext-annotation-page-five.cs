using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_hidden.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document (using statement ensures proper disposal)
        using (Document doc = new Document(inputPath))
        {
            // Ensure the document has at least five pages
            if (doc.Pages.Count < 5)
            {
                Console.Error.WriteLine("Document does not contain a page 5.");
                return;
            }

            // Get page five (Aspose.Pdf uses 1‑based indexing)
            Page pageFive = doc.Pages[5];

            // Locate the first FreeTextAnnotation on page five
            FreeTextAnnotation freeText = null;
            foreach (Annotation ann in pageFive.Annotations)
            {
                if (ann is FreeTextAnnotation ft)
                {
                    freeText = ft;
                    break;
                }
            }

            if (freeText == null)
            {
                Console.Error.WriteLine("No FreeTextAnnotation found on page 5.");
                return;
            }

            // Create a HideAction that hides the annotation (isHidden = true)
            HideAction hide = new HideAction(freeText, true);

            // Add the HideAction to the annotation's action collection
            freeText.Actions.Add(hide);

            // Save the modified PDF (preserves all annotation data)
            doc.Save(outputPath);
        }

        Console.WriteLine($"FreeTextAnnotation on page 5 hidden and saved to '{outputPath}'.");
    }
}