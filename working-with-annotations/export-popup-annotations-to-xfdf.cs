using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";          // source PDF
        const string outputXfdf = "popups.xfdf";       // XFDF containing only popup annotations

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPdf))
        {
            // Iterate through all pages
            for (int pageIdx = 1; pageIdx <= doc.Pages.Count; pageIdx++)
            {
                Page page = doc.Pages[pageIdx];

                // Remove non‑popup annotations.
                // Iterate backwards because Delete shifts the collection.
                for (int annIdx = page.Annotations.Count; annIdx >= 1; annIdx--)
                {
                    Annotation ann = page.Annotations[annIdx];

                    // Keep only PopupAnnotation instances
                    if (!(ann is PopupAnnotation))
                    {
                        page.Annotations.Delete(annIdx);
                    }
                }
            }

            // Export the remaining (popup) annotations to XFDF
            doc.ExportAnnotationsToXfdf(outputXfdf);
        }

        Console.WriteLine($"Popup annotations exported to '{outputXfdf}'.");
    }
}