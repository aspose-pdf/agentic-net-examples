using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string xfdfPath     = "annotations.xfdf";
        const string outputPdfPath = "output.pdf";

        // Mapping of annotation names to the target page number (1‑based)
        var annotationPageMap = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase)
        {
            // Example entries – adjust to your actual annotation names and target pages
            { "Annot1", 2 },
            { "Annot2", 3 }
        };

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }

        if (!File.Exists(xfdfPath))
        {
            Console.Error.WriteLine($"XFDF file not found: {xfdfPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPdfPath))
        {
            // Create the annotation editor and bind the loaded document
            using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
            {
                editor.BindPdf(doc);

                // Import all annotations from the XFDF file
                editor.ImportAnnotationsFromXfdf(xfdfPath);

                // Re‑assign annotations to pages according to the mapping dictionary
                // Iterate pages in reverse order when removing items to avoid index shift
                for (int srcPageNum = 1; srcPageNum <= doc.Pages.Count; srcPageNum++)
                {
                    Page srcPage = doc.Pages[srcPageNum];
                    for (int idx = srcPage.Annotations.Count; idx >= 1; idx--)
                    {
                        Annotation ann = srcPage.Annotations[idx];
                        if (ann == null) continue;

                        // Use the annotation's Name property as the key for mapping
                        if (!string.IsNullOrEmpty(ann.Name) &&
                            annotationPageMap.TryGetValue(ann.Name, out int targetPageNum) &&
                            targetPageNum != srcPageNum &&
                            targetPageNum >= 1 && targetPageNum <= doc.Pages.Count)
                        {
                            // Remove from the source page
                            srcPage.Annotations.Delete(idx);

                            // Add to the target page
                            Page targetPage = doc.Pages[targetPageNum];
                            targetPage.Annotations.Add(ann);
                        }
                    }
                }

                // Save the modified PDF
                editor.Save(outputPdfPath);
            }
        }

        Console.WriteLine($"Annotations imported and reassigned. Output saved to '{outputPdfPath}'.");
    }
}