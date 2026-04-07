using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_hidden_freetext.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document (using ensures proper disposal)
        using (Document doc = new Document(inputPath))
        {
            // Aspose.Pdf uses 1‑based page indexing; page 5 is accessed via index 5
            if (doc.Pages.Count < 5)
            {
                Console.Error.WriteLine("The document has fewer than 5 pages.");
                return;
            }

            Page pageFive = doc.Pages[5];

            // Locate the first FreeTextAnnotation on page five
            FreeTextAnnotation freeText = null;
            foreach (Annotation ann in pageFive.Annotations)
            {
                if (ann is FreeTextAnnotation ftAnn)
                {
                    freeText = ftAnn;
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

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Free‑text annotation on page 5 hidden and saved to '{outputPath}'.");
    }
}