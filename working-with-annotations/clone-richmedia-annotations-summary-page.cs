using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_with_summary.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the source PDF
        using (Document doc = new Document(inputPath))
        {
            // Collect all RichMediaAnnotations from the existing pages
            List<RichMediaAnnotation> richMediaAnnotations = new List<RichMediaAnnotation>();

            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];
                foreach (Annotation ann in page.Annotations)
                {
                    if (ann is RichMediaAnnotation rma)
                    {
                        richMediaAnnotations.Add(rma);
                    }
                }
            }

            // Add a new blank page that will serve as the summary page
            Page summaryPage = doc.Pages.Add();

            // Clone each collected RichMediaAnnotation onto the summary page
            foreach (RichMediaAnnotation original in richMediaAnnotations)
            {
                // Create a new RichMediaAnnotation on the summary page with the same rectangle
                RichMediaAnnotation clone = new RichMediaAnnotation(summaryPage, original.Rect);

                // Copy relevant properties (adjust as needed for your scenario)
                clone.Contents = original.Contents;
                clone.Type = original.Type;
                clone.Color = original.Color;
                clone.Name = original.Name;
                clone.ActivateOn = original.ActivateOn;
                clone.Flags = original.Flags;
                clone.CustomFlashVariables = original.CustomFlashVariables;
                clone.CustomPlayer = original.CustomPlayer;
                clone.Height = original.Height;
                clone.Width = original.Width;
                clone.ZIndex = original.ZIndex;

                // Add the cloned annotation to the summary page
                summaryPage.Annotations.Add(clone);
            }

            // Save the modified document
            doc.Save(outputPath);
        }

        Console.WriteLine($"Document saved with cloned RichMediaAnnotations on summary page: {outputPath}");
    }
}