using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Comparison;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string originalPath = "original.pdf";
        const string modifiedPath = "modified.pdf";
        const string diffPath = "diff.pdf";

        if (!File.Exists(originalPath) || !File.Exists(modifiedPath))
        {
            Console.Error.WriteLine("Required input PDFs not found.");
            return;
        }

        try
        {
            // Load the two PDFs to be compared
            using (Document originalDoc = new Document(originalPath))
            using (Document modifiedDoc = new Document(modifiedPath))
            {
                // Create visual side‑by‑side comparison PDF using default highlight colors
                var options = new SideBySideComparisonOptions(); // defaults: Added → Yellow, Deleted → Red
                SideBySidePdfComparer.Compare(originalDoc, modifiedDoc, diffPath, options);
            }

            // Verify that the highlight colors in the generated diff PDF match the defaults
            using (Document diffDoc = new Document(diffPath))
            {
                bool allMatch = true;

                foreach (Page page in diffDoc.Pages)
                {
                    foreach (Annotation annot in page.Annotations)
                    {
                        if (annot is HighlightAnnotation highlight)
                        {
                            // Title is defined on MarkupAnnotation, so we need to cast
                            string title = string.Empty;
                            if (highlight is MarkupAnnotation markup)
                                title = markup.Title ?? string.Empty;

                            Aspose.Pdf.Color expectedColor = null;

                            if (title.Equals("Added", StringComparison.OrdinalIgnoreCase))
                                expectedColor = Aspose.Pdf.Color.Yellow;
                            else if (title.Equals("Deleted", StringComparison.OrdinalIgnoreCase))
                                expectedColor = Aspose.Pdf.Color.Red;

                            if (expectedColor != null && !highlight.Color.Equals(expectedColor))
                            {
                                allMatch = false;
                                Console.WriteLine($"Page {page.Number}: Highlight '{title}' has unexpected color.");
                            }
                        }
                    }
                }

                Console.WriteLine(allMatch
                    ? "All highlight colors match documentation defaults."
                    : "Some highlight colors do not match defaults.");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
