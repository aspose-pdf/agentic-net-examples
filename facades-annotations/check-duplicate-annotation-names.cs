using System;
using System.Collections.Generic;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    // Checks for duplicate annotation names in the specified PDF and logs conflicts.
    static void CheckDuplicateAnnotationNames(string pdfPath)
    {
        if (!System.IO.File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"File not found: {pdfPath}");
            return;
        }

        try
        {
            // Load the PDF document.
            Document pdfDoc = new Document(pdfPath);

            // Dictionary to collect annotation names and the pages they appear on.
            var nameMap = new Dictionary<string, List<int>>(StringComparer.OrdinalIgnoreCase);

            // Iterate through all pages and their annotations.
            for (int pageIndex = 1; pageIndex <= pdfDoc.Pages.Count; pageIndex++)
            {
                Page page = pdfDoc.Pages[pageIndex];
                foreach (Annotation annot in page.Annotations)
                {
                    // Some annotations may not have a name; skip them.
                    string name = annot.Name;
                    if (string.IsNullOrEmpty(name))
                        continue;

                    if (!nameMap.TryGetValue(name, out List<int> pages))
                    {
                        pages = new List<int>();
                        nameMap[name] = pages;
                    }
                    pages.Add(pageIndex);
                }
            }

            // Log any names that occur more than once.
            bool duplicatesFound = false;
            foreach (var kvp in nameMap)
            {
                if (kvp.Value.Count > 1)
                {
                    duplicatesFound = true;
                    Console.WriteLine($"Duplicate annotation name '{kvp.Key}' found on pages: {string.Join(", ", kvp.Value)}");
                }
            }

            if (!duplicatesFound)
                Console.WriteLine("No duplicate annotation names were found.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error processing PDF: {ex.Message}");
        }
    }

    static void Main()
    {
        const string inputPdf = "sample.pdf";
        CheckDuplicateAnnotationNames(inputPdf);
    }
}
