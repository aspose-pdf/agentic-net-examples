using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Text; // for any text handling if needed

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_with_summary.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the source PDF inside a using block (document-disposal-with-using rule)
        using (Document doc = new Document(inputPath))
        {
            // Create a new blank page that will serve as the summary page
            // Page indexing is 1‑based (page-indexing-one-based rule)
            Page summaryPage = doc.Pages.Add();

            // Optional: set a reasonable size for the summary page (same as first page)
            if (doc.Pages.Count > 1)
            {
                Page firstPage = doc.Pages[1];
                summaryPage.PageInfo.Width  = firstPage.PageInfo.Width;
                summaryPage.PageInfo.Height = firstPage.PageInfo.Height;
            }

            // Iterate over all existing pages and collect RichMediaAnnotations
            foreach (Page page in doc.Pages)
            {
                // The Annotations collection may be empty; iterate safely
                for (int i = 0; i < page.Annotations.Count; i++)
                {
                    Annotation ann = page.Annotations[i];
                    if (ann is RichMediaAnnotation richMediaAnn)
                    {
                        // Clone the annotation (free‑form code – no specific rule exists)
                        // RichMediaAnnotation implements ICloneable, so Clone() returns object
                        RichMediaAnnotation clonedAnn = (RichMediaAnnotation)richMediaAnn.Clone();

                        // Adjust the rectangle if needed – here we keep the original position
                        // Ensure we use the fully qualified Rectangle type to avoid ambiguity
                        Aspose.Pdf.Rectangle rect = clonedAnn.Rect;
                        clonedAnn.Rect = rect; // no change, just explicit

                        // Add the cloned annotation to the summary page
                        summaryPage.Annotations.Add(clonedAnn);
                    }
                }
            }

            // Save the modified document (PDF format, no special SaveOptions needed)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Summary PDF saved to '{outputPath}'.");
    }
}