using System;
using System.IO;
using System.Reflection;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Comparison; // retained for runtime type lookup

class DiffHighlightVerifier
{
    static void Main()
    {
        // Input PDF paths
        const string originalPath = "original.pdf";
        const string modifiedPath = "modified.pdf";
        const string diffPath = "diff.pdf";

        // Verify input files exist
        if (!File.Exists(originalPath))
        {
            Console.Error.WriteLine($"File not found: {originalPath}");
            return;
        }
        if (!File.Exists(modifiedPath))
        {
            Console.Error.WriteLine($"File not found: {modifiedPath}");
            return;
        }

        // -----------------------------------------------------------------
        // Generate diff PDF using default style (no custom options)
        // -----------------------------------------------------------------
        using (Document originalDoc = new Document(originalPath))
        using (Document modifiedDoc = new Document(modifiedPath))
        {
            // PdfComparator lives in Aspose.Pdf.Comparison.dll which may not be
            // referenced at compile time. Resolve it via reflection to avoid a
            // compile‑time dependency.
            const string comparatorTypeName = "Aspose.Pdf.Comparison.PdfComparator, Aspose.Pdf.Comparison";
            Type comparatorType = Type.GetType(comparatorTypeName);
            if (comparatorType == null)
            {
                Console.Error.WriteLine("PdfComparator type not found. Ensure Aspose.Pdf.Comparison assembly is available.");
                return;
            }

            // Create an instance: new PdfComparator(originalDoc, modifiedDoc)
            object comparator = Activator.CreateInstance(comparatorType, originalDoc, modifiedDoc);
            MethodInfo compareMethod = comparatorType.GetMethod("Compare", new[] { typeof(string) });
            if (compareMethod == null)
            {
                Console.Error.WriteLine("Compare method not found on PdfComparator.");
                return;
            }
            // Execute: comparator.Compare(diffPath);
            compareMethod.Invoke(comparator, new object[] { diffPath });
        }

        // -----------------------------------------------------------------
        // Verify that highlight colors in the diff PDF match defaults
        // (default highlight color is Aspose.Pdf.Color.Yellow)
        // -----------------------------------------------------------------
        if (!File.Exists(diffPath))
        {
            Console.Error.WriteLine($"Diff PDF was not created: {diffPath}");
            return;
        }

        bool allHighlightsMatch = true;
        int highlightCount = 0;

        using (Document diffDoc = new Document(diffPath))
        {
            // Pages are 1‑based; iterate accordingly
            for (int i = 1; i <= diffDoc.Pages.Count; i++)
            {
                Page page = diffDoc.Pages[i];
                // Annotations collection may contain HighlightAnnotation objects
                foreach (Annotation annot in page.Annotations)
                {
                    if (annot is HighlightAnnotation highlight)
                    {
                        highlightCount++;
                        // Compare with the documented default color (Yellow)
                        if (!highlight.Color.Equals(Aspose.Pdf.Color.Yellow))
                        {
                            allHighlightsMatch = false;
                            Console.WriteLine($"Page {i}: Highlight color mismatch. Found: {highlight.Color}");
                        }
                    }
                }
            }
        }

        // Report verification result
        if (highlightCount == 0)
        {
            Console.WriteLine("No highlight annotations were found in the diff PDF.");
        }
        else if (allHighlightsMatch)
        {
            Console.WriteLine($"All {highlightCount} highlight annotations use the default color (Yellow).");
        }
        else
        {
            Console.WriteLine("Some highlight annotations do not use the default color. See messages above.");
        }
    }
}
