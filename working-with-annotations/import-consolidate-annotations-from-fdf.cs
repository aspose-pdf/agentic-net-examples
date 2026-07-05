using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Annotations; // FdfReader is in this namespace

class Program
{
    static void Main()
    {
        // Input PDF that will receive the annotations
        const string pdfPath = "base.pdf";

        // List of FDF files to import
        string[] fdfFiles = { "notes1.fdf", "comments.fdf", "review.fdf" };

        // Ensure the PDF exists
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF not found: {pdfPath}");
            return;
        }

        // Open the PDF document inside a using block (lifecycle rule)
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

                // Open the FDF stream and import annotations (FdfReader rule)
                using (FileStream fdfStream = File.OpenRead(fdfPath))
                {
                    FdfReader.ReadAnnotations(fdfStream, doc);
                }
            }

            // Consolidate all annotations from all pages into a single collection
            List<Annotation> allAnnotations = new List<Annotation>();

            // Pages are 1‑based (global rule)
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                AnnotationCollection pageAnnots = doc.Pages[i].Annotations;

                // Add each annotation from the page to the list
                foreach (Annotation annot in pageAnnots)
                {
                    allAnnotations.Add(annot);
                }
            }

            // Example usage: output total count
            Console.WriteLine($"Total annotations imported: {allAnnotations.Count}");

            // Save the updated PDF (lifecycle rule – still inside using)
            const string outputPath = "annotated_output.pdf";
            doc.Save(outputPath);
            Console.WriteLine($"Annotated PDF saved to '{outputPath}'.");
        }
    }
}