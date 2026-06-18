using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        // Input PDF and FDF files
        const string pdfPath = "input.pdf";
        string[] fdfPaths = { "annotations1.fdf", "annotations2.fdf", "annotations3.fdf" };
        const string outputPdfPath = "output_with_all_annotations.pdf";

        // Verify input PDF exists
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF not found: {pdfPath}");
            return;
        }

        // Verify each FDF file exists
        foreach (string fdf in fdfPaths)
        {
            if (!File.Exists(fdf))
            {
                Console.Error.WriteLine($"FDF not found: {fdf}");
                return;
            }
        }

        // Load the PDF document (using statement ensures proper disposal)
        using (Document doc = new Document(pdfPath))
        {
            // Import annotations from each FDF file into the document
            foreach (string fdfFile in fdfPaths)
            {
                using (FileStream fdfStream = File.OpenRead(fdfFile))
                {
                    // FdfReader reads annotations from the stream and adds them to the document
                    FdfReader.ReadAnnotations(fdfStream, doc);
                }
            }

            // Consolidate all annotations from all pages into a single collection
            List<Annotation> allAnnotations = new List<Annotation>();
            for (int pageIndex = 1; pageIndex <= doc.Pages.Count; pageIndex++) // 1‑based indexing
            {
                Page page = doc.Pages[pageIndex];
                foreach (Annotation ann in page.Annotations) // AnnotationCollection is enumerable
                {
                    allAnnotations.Add(ann);
                }
            }

            // Example usage of the consolidated collection (e.g., report count)
            Console.WriteLine($"Total annotations after import: {allAnnotations.Count}");

            // Save the updated PDF with all imported annotations
            doc.Save(outputPdfPath);
        }

        Console.WriteLine($"PDF saved with consolidated annotations to '{outputPdfPath}'.");
    }
}