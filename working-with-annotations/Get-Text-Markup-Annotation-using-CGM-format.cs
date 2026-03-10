using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string cgmPath = "input.cgm";

        if (!File.Exists(cgmPath))
        {
            Console.Error.WriteLine($"File not found: {cgmPath}");
            return;
        }

        // Load the CGM file (input‑only format) into a PDF document
        CgmLoadOptions loadOptions = new CgmLoadOptions();
        using (Document doc = new Document(cgmPath, loadOptions))
        {
            // Iterate over all pages (1‑based indexing)
            for (int pageIndex = 1; pageIndex <= doc.Pages.Count; pageIndex++)
            {
                Page page = doc.Pages[pageIndex];

                // Iterate over all annotations on the page (also 1‑based)
                for (int annIndex = 1; annIndex <= page.Annotations.Count; annIndex++)
                {
                    Annotation annotation = page.Annotations[annIndex];

                    // Process only text‑markup annotations (highlight, underline, strikeout, etc.)
                    if (annotation is TextMarkupAnnotation markup)
                    {
                        // Get the plain text covered by the markup annotation
                        string markedText = markup.GetMarkedText();
                        Console.WriteLine($"Page {pageIndex}, Annotation {annIndex} ({markup.AnnotationType}): \"{markedText}\"");

                        // Optionally retrieve detailed text fragments
                        TextFragmentCollection fragments = markup.GetMarkedTextFragments();
                        foreach (TextFragment fragment in fragments)
                        {
                            Console.WriteLine($"  Fragment: \"{fragment.Text}\" at position {fragment.Position}");
                        }
                    }
                }
            }
        }
    }
}