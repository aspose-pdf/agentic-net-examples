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
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Iterate over all pages
            foreach (Page page in doc.Pages)
            {
                // Collect popup annotations first to avoid modifying the collection while iterating
                List<PopupAnnotation> popups = new List<PopupAnnotation>();
                foreach (Annotation annot in page.Annotations)
                {
                    if (annot is PopupAnnotation popup)
                        popups.Add(popup);
                }

                // Remove each popup annotation and clear its reference from the parent markup annotation
                foreach (PopupAnnotation popup in popups)
                {
                    // Detach from parent markup annotation, if any
                    if (popup.Parent is MarkupAnnotation parentMarkup)
                    {
                        // The parent markup annotation holds a reference to its popup via the Popup property
                        parentMarkup.Popup = null;
                    }

                    // Delete the popup annotation from the page's annotation collection
                    page.Annotations.Delete(popup);
                }
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"All popup annotations removed. Output saved to '{outputPath}'.");
    }
}