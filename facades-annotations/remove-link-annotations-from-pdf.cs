using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_no_links.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF with Document (Document implements IDisposable).
        using (Document pdfDoc = new Document(inputPath))
        {
            // Ensure the requested page range is within the document.
            int startPage = Math.Max(3, 1);
            int endPage = Math.Min(7, pdfDoc.Pages.Count);

            for (int pageNum = startPage; pageNum <= endPage; pageNum++)
            {
                Page page = pdfDoc.Pages[pageNum];
                // Collect link annotations to remove (cannot modify collection while iterating).
                List<Annotation> linksToRemove = new List<Annotation>();
                foreach (Annotation annot in page.Annotations)
                {
                    if (annot is LinkAnnotation)
                        linksToRemove.Add(annot);
                }
                // Delete the collected link annotations.
                foreach (Annotation link in linksToRemove)
                {
                    page.Annotations.Delete(link);
                }
            }

            pdfDoc.Save(outputPath);
        }

        Console.WriteLine($"Link annotations removed. Saved to '{outputPath}'.");
    }
}
