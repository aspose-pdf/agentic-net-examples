using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPdf = "sample.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        CheckDuplicateAnnotationNames(inputPdf);
    }

    // Checks for duplicate annotation names in the specified PDF and logs any conflicts.
    static void CheckDuplicateAnnotationNames(string pdfPath)
    {
        // Initialize the annotation editor and bind the PDF document.
        using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
        {
            editor.BindPdf(pdfPath);

            // Access the underlying Document instance.
            Document doc = editor.Document;

            // Dictionary to count occurrences of each annotation name.
            Dictionary<string, int> nameCounts = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);

            // Iterate through all pages and their annotations.
            foreach (Page page in doc.Pages)
            {
                foreach (Annotation annotation in page.Annotations)
                {
                    // Annotation.Name may be null or empty; skip such entries.
                    string name = annotation.Name;
                    if (string.IsNullOrEmpty(name))
                        continue;

                    if (nameCounts.ContainsKey(name))
                        nameCounts[name]++;
                    else
                        nameCounts[name] = 1;
                }
            }

            // Log any names that appear more than once.
            bool duplicatesFound = false;
            foreach (var kvp in nameCounts.Where(k => k.Value > 1))
            {
                duplicatesFound = true;
                Console.WriteLine($"Duplicate annotation name \"{kvp.Key}\" found {kvp.Value} times.");
            }

            if (!duplicatesFound)
            {
                Console.WriteLine("No duplicate annotation names were found.");
            }

            // No modifications are made, so saving is optional.
            // editor.Save can be called if a copy with changes is desired.
            editor.Close();
        }
    }
}