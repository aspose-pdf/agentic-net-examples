using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        // Input PDF, output PDF and replacement strings
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";
        const string srcString  = "Old Annotation Text";   // text to look for inside annotations
        const string destString = "New Annotation Text";   // replacement text

        // Page range (1‑based). Use 0 for all pages if needed.
        const int startPage = 1;
        const int endPage   = 3;   // replace on pages 1 to 3 (inclusive)

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Determine the actual page range (0 means "all pages")
            int first = Math.Max(1, startPage);
            int last  = endPage == 0 ? doc.Pages.Count : Math.Min(endPage, doc.Pages.Count);

            // Iterate over the selected pages
            for (int pageNum = first; pageNum <= last; pageNum++)
            {
                Page page = doc.Pages[pageNum];

                // Iterate over all annotations on the current page
                foreach (Annotation annot in page.Annotations)
                {
                    // We are interested only in TextAnnotation objects
                    if (annot is TextAnnotation textAnnot)
                    {
                        // If a source string is supplied, replace only matching parts;
                        // otherwise replace the whole content.
                        if (!string.IsNullOrEmpty(srcString) &&
                            !string.IsNullOrEmpty(textAnnot.Contents) &&
                            textAnnot.Contents.Contains(srcString))
                        {
                            textAnnot.Contents = textAnnot.Contents.Replace(srcString, destString);
                        }
                        else if (string.IsNullOrEmpty(srcString))
                        {
                            textAnnot.Contents = destString;
                        }
                    }
                }
            }

            // Save the modified document.
            doc.Save(outputPath);
        }

        Console.WriteLine($"Annotations updated and saved to '{outputPath}'.");
    }
}
