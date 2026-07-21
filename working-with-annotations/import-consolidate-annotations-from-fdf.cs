using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        // Input PDF and list of FDF files containing annotations
        const string pdfPath = "input.pdf";
        string[] fdfFiles = { "annotations1.fdf", "annotations2.fdf", "annotations3.fdf" };
        const string outputPdf = "output_with_annotations.pdf";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF not found: {pdfPath}");
            return;
        }

        // Load the PDF document (lifecycle rule: use using for deterministic disposal)
        using (Document doc = new Document(pdfPath))
        {
            // Import annotations from each FDF file into the same document
            foreach (string fdfPath in fdfFiles)
            {
                if (!File.Exists(fdfPath))
                {
                    Console.Error.WriteLine($"FDF not found: {fdfPath}");
                    continue;
                }

                // Open the FDF stream and read annotations (FdfReader.ReadAnnotations is the correct API)
                using (FileStream fdfStream = File.OpenRead(fdfPath))
                {
                    FdfReader.ReadAnnotations(fdfStream, doc);
                }
            }

            // Consolidate all annotations from all pages into a single collection
            List<Annotation> consolidated = new List<Annotation>();
            for (int i = 1; i <= doc.Pages.Count; i++) // Aspose.Pdf uses 1‑based page indexing
            {
                AnnotationCollection pageAnnotations = doc.Pages[i].Annotations;
                foreach (Annotation ann in pageAnnotations)
                {
                    consolidated.Add(ann);
                }
            }

            // Example usage: display total number of merged annotations
            Console.WriteLine($"Total merged annotations: {consolidated.Count}");

            // Save the updated PDF (no extra SaveOptions needed for PDF output)
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Merged PDF saved to '{outputPdf}'.");
    }
}