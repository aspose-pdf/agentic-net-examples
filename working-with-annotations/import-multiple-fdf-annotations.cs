using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Annotations; // FdfReader resides here

class Program
{
    static void Main()
    {
        // Input PDF that will receive the annotations
        const string pdfPath = "source.pdf";

        // Array of FDF files to import
        string[] fdfFiles = { "comments1.fdf", "comments2.fdf", "comments3.fdf" };

        // Ensure the source PDF exists
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF not found: {pdfPath}");
            return;
        }

        // Load the PDF document (lifecycle rule: use using for disposal)
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

                using (FileStream fdfStream = File.OpenRead(fdfPath))
                {
                    // Static method reads annotations from the stream and adds them to the document
                    FdfReader.ReadAnnotations(fdfStream, doc);
                }
            }

            // Consolidate all annotations from all pages into a single list
            List<Annotation> allAnnotations = new List<Annotation>();
            foreach (Page page in doc.Pages)
            {
                AnnotationCollection pageAnnotations = page.Annotations;
                foreach (Annotation ann in pageAnnotations)
                {
                    allAnnotations.Add(ann);
                }
            }

            // Example: output total count of consolidated annotations
            Console.WriteLine($"Total annotations imported: {allAnnotations.Count}");

            // Save the PDF with the combined annotations
            const string outputPath = "consolidated_output.pdf";
            doc.Save(outputPath);
            Console.WriteLine($"PDF saved with consolidated annotations to '{outputPath}'.");
        }
    }
}