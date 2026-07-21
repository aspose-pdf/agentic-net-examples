using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Annotations;

public static class PdfAnnotationUtilities
{
    /// <summary>
    /// Checks a PDF for duplicate annotation names and writes any conflicts to a log file.
    /// </summary>
    /// <param name="pdfPath">Path to the source PDF file.</param>
    /// <param name="logPath">Path to the log file where duplicate names will be recorded.</param>
    public static void CheckDuplicateAnnotationNames(string pdfPath, string logPath)
    {
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {pdfPath}");
            return;
        }

        // Dictionary to track the first occurrence of each annotation name.
        // Key: annotation name, Value: page number where it first appears.
        var nameMap = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);
        // List to collect duplicate entries for logging.
        var duplicates = new List<string>();

        // Use PdfAnnotationEditor (facade) to work with annotations.
        using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
        {
            // Load the PDF document.
            editor.BindPdf(pdfPath);

            // Access the underlying Document object.
            Document doc = editor.Document;

            // Iterate through all pages (1‑based indexing).
            for (int pageIndex = 1; pageIndex <= doc.Pages.Count; pageIndex++)
            {
                Page page = doc.Pages[pageIndex];

                // Iterate through each annotation on the current page.
                foreach (Annotation annotation in page.Annotations)
                {
                    // Annotation.Name may be null or empty; ignore such cases.
                    string annotName = annotation.Name;
                    if (string.IsNullOrEmpty(annotName))
                        continue;

                    if (nameMap.TryGetValue(annotName, out int firstPage))
                    {
                        // Duplicate found – record the conflict.
                        duplicates.Add(
                            $"Duplicate annotation name \"{annotName}\" found on pages {firstPage} and {pageIndex}.");
                    }
                    else
                    {
                        // First occurrence – store the page number.
                        nameMap[annotName] = pageIndex;
                    }
                }
            }

            // No modifications are made, but we can still save the original PDF if desired.
            // editor.Save(pdfPath); // Uncomment if you need to rewrite the file unchanged.
        }

        // Write the duplicate report to the specified log file.
        using (StreamWriter writer = new StreamWriter(logPath, false))
        {
            if (duplicates.Count == 0)
            {
                writer.WriteLine("No duplicate annotation names were found.");
                Console.WriteLine("No duplicates detected.");
            }
            else
            {
                writer.WriteLine("Duplicate annotation names detected:");
                foreach (string line in duplicates)
                {
                    writer.WriteLine(line);
                }
                Console.WriteLine($"{duplicates.Count} duplicate(s) found. Details written to {logPath}.");
            }
        }
    }
}

// Simple entry point to make the project compile. In real usage replace the arguments
// with actual file paths or integrate the utility into another application.
public class Program
{
    public static void Main(string[] args)
    {
        if (args.Length < 2)
        {
            Console.WriteLine("Usage: PdfAnnotationUtilities <pdfPath> <logPath>");
            return;
        }

        string pdfPath = args[0];
        string logPath = args[1];
        PdfAnnotationUtilities.CheckDuplicateAnnotationNames(pdfPath, logPath);
    }
}
