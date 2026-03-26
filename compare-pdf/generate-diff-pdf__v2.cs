using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf;
using Aspose.Pdf.Comparison;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string originalPath = "original.pdf";
        const string modifiedPath = "modified.pdf";
        const string diffPath = "diff_output.pdf";

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

        // Load the two PDFs to be compared
        using (Document originalDoc = new Document(originalPath))
        using (Document modifiedDoc = new Document(modifiedPath))
        {
            // Perform a flat text comparison
            ComparisonOptions options = new ComparisonOptions();
            List<DiffOperation> diffOps = TextPdfComparer.CompareFlatDocuments(originalDoc, modifiedDoc, options);

            // Generate a diff PDF using the default output style (default highlight colors)
            PdfOutputGenerator generator = new PdfOutputGenerator();
            generator.GenerateOutput(diffOps, diffPath);
        }

        // Verify that the highlight color of the first annotation matches the documented default
        using (Document diffDoc = new Document(diffPath))
        {
            if (diffDoc.Pages.Count > 0 && diffDoc.Pages[1].Annotations.Count > 0)
            {
                Annotation firstAnnotation = diffDoc.Pages[1].Annotations[1];
                Aspose.Pdf.Color expectedColor = Aspose.Pdf.Color.Yellow; // documented default highlight color
                bool colorMatches = firstAnnotation.Color.Equals(expectedColor);
                Console.WriteLine($"First highlight color matches default: {colorMatches}");
            }
            else
            {
                Console.WriteLine("No annotations found in the diff PDF.");
            }
        }
    }
}