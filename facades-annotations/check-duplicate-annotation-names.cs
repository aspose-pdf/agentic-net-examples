using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string pdfPath = "sample.pdf";
        CheckDuplicateAnnotations(pdfPath);
    }

    // Checks for duplicate annotation names in the specified PDF and logs any conflicts.
    static void CheckDuplicateAnnotations(string pdfPath)
    {
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"File not found: {pdfPath}");
            return;
        }

        // Load the PDF document. Using Document gives access to page‑level annotations.
        Document pdfDoc = new Document(pdfPath);

        // Collect annotation name and the page number on which it appears.
        var annotationInfos = new List<(string Name, int PageNumber)>();
        for (int pageIdx = 1; pageIdx <= pdfDoc.Pages.Count; pageIdx++)
        {
            var page = pdfDoc.Pages[pageIdx];
            foreach (Annotation annot in page.Annotations)
            {
                if (!string.IsNullOrEmpty(annot.Name))
                {
                    annotationInfos.Add((annot.Name, pageIdx));
                }
            }
        }

        // Group by name and keep only groups with more than one occurrence.
        var duplicateGroups = annotationInfos
            .GroupBy(ai => ai.Name)
            .Where(g => g.Count() > 1);

        // Log each duplicate name with the pages on which it appears.
        foreach (var group in duplicateGroups)
        {
            string pages = string.Join(", ", group.Select(ai => ai.PageNumber));
            Console.WriteLine($"Duplicate annotation name '{group.Key}' found {group.Count()} times on pages: {pages}");
        }

        if (!duplicateGroups.Any())
        {
            Console.WriteLine("No duplicate annotation names found.");
        }
    }
}
