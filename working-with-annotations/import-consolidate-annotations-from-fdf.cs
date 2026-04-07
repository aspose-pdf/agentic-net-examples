using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Facades; // FdfReader resides here

class Program
{
    static void Main()
    {
        const string inputPdfPath = "source.pdf";
        string[] fdfFiles = { "notes1.fdf", "notes2.fdf", "notes3.fdf" };
        const string outputPdfPath = "consolidated.pdf";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }

        using (Document doc = new Document(inputPdfPath))
        {
            foreach (string fdfPath in fdfFiles)
            {
                if (!File.Exists(fdfPath))
                {
                    Console.Error.WriteLine($"FDF file not found: {fdfPath}");
                    continue;
                }

                using (FileStream fdfStream = File.OpenRead(fdfPath))
                {
                    // Import annotations from the FDF stream into the document
                    FdfReader.ReadAnnotations(fdfStream, doc);
                }
            }

            // Consolidate all annotations from all pages into a single collection
            List<Annotation> allAnnotations = new List<Annotation>();
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];
                foreach (Annotation ann in page.Annotations)
                {
                    allAnnotations.Add(ann);
                }
            }

            Console.WriteLine($"Total annotations imported: {allAnnotations.Count}");
            doc.Save(outputPdfPath);
        }

        Console.WriteLine($"Consolidated PDF saved to '{outputPdfPath}'.");
    }
}
