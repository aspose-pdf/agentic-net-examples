using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Annotations;

class AnnotationDuplicateChecker
{
    /// <summary>
    /// Checks a PDF for duplicate annotation names and writes a report to the specified log file.
    /// </summary>
    /// <param name="pdfPath">Path to the input PDF.</param>
    /// <param name="logPath">Path to the log file where duplicate information will be written.</param>
    public static void CheckDuplicateAnnotationNames(string pdfPath, string logPath)
    {
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {pdfPath}");
            return;
        }

        // Use PdfAnnotationEditor (Facades API) to work with annotations.
        using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
        {
            // Load the PDF document.
            editor.BindPdf(pdfPath);

            // Access the underlying Document.
            Document doc = editor.Document;

            // Dictionary to track annotation names and their locations.
            // Key: annotation name, Value: list of (page number, annotation index) tuples.
            var nameMap = new Dictionary<string, List<(int pageNumber, int annotationIndex)>>();

            // Iterate through all pages (Aspose.Pdf uses 1‑based indexing).
            for (int pageIdx = 1; pageIdx <= doc.Pages.Count; pageIdx++)
            {
                Page page = doc.Pages[pageIdx];
                AnnotationCollection annots = page.Annotations;

                // Iterate through annotations on the current page.
                for (int annIdx = 1; annIdx <= annots.Count; annIdx++)
                {
                    Annotation annot = annots[annIdx];

                    // Annotation.Name may be null or empty; ignore such entries.
                    string name = annot.Name;
                    if (string.IsNullOrEmpty(name))
                        continue;

                    if (!nameMap.TryGetValue(name, out var locations))
                    {
                        locations = new List<(int, int)>();
                        nameMap[name] = locations;
                    }

                    locations.Add((pageIdx, annIdx));
                }
            }

            // Prepare the log output.
            using (StreamWriter writer = new StreamWriter(logPath, false))
            {
                bool anyDuplicates = false;

                foreach (var kvp in nameMap)
                {
                    if (kvp.Value.Count > 1)
                    {
                        anyDuplicates = true;
                        writer.WriteLine($"Duplicate annotation name: \"{kvp.Key}\" found {kvp.Value.Count} times.");
                        foreach (var loc in kvp.Value)
                        {
                            writer.WriteLine($"  - Page {loc.pageNumber}, Annotation index {loc.annotationIndex}");
                        }
                        writer.WriteLine();
                    }
                }

                if (!anyDuplicates)
                {
                    writer.WriteLine("No duplicate annotation names were found.");
                }
            }

            // No modifications are made, but to satisfy the lifecycle rule we can save the document unchanged.
            // This uses the Save method of PdfAnnotationEditor.
            string tempOutput = Path.Combine(Path.GetDirectoryName(pdfPath) ?? "", "temp_output.pdf");
            editor.Save(tempOutput);
        }

        Console.WriteLine($"Duplicate annotation check completed. Log written to: {logPath}");
    }

    // Example usage.
    static void Main()
    {
        string inputPdf = "sample.pdf";
        string logFile = "annotation_duplicates.log";

        CheckDuplicateAnnotationNames(inputPdf, logFile);
    }
}