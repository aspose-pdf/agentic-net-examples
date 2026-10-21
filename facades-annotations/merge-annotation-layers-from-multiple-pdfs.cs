using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input PDFs – each file may contain its own annotation layer
        string[] inputFiles = { "doc1.pdf", "doc2.pdf", "doc3.pdf" };
        string outputFile = "merged_annotations.pdf";

        // Verify that all input files exist
        foreach (string f in inputFiles)
        {
            if (!File.Exists(f))
            {
                Console.Error.WriteLine($"File not found: {f}");
                return;
            }
        }

        // Use the first PDF as the base document
        using (Document target = new Document(inputFiles[0]))
        {
            // Process remaining PDFs and copy their annotations onto the target
            for (int i = 1; i < inputFiles.Length; i++)
            {
                using (Document source = new Document(inputFiles[i]))
                {
                    // Iterate through pages – Aspose.Pdf uses 1‑based indexing
                    int pageCount = Math.Min(target.Pages.Count, source.Pages.Count);
                    for (int p = 1; p <= pageCount; p++)
                    {
                        Page targetPage = target.Pages[p];
                        Page sourcePage = source.Pages[p];

                        // Copy each annotation from the source page to the target page
                        for (int a = 0; a < sourcePage.Annotations.Count; a++)
                        {
                            // Retrieve the annotation from the source document
                            Aspose.Pdf.Annotations.Annotation srcAnnotation = sourcePage.Annotations[a];

                            // Add the same annotation instance to the target page.
                            // The annotation is detached from the source document and attached to the target.
                            targetPage.Annotations.Add(srcAnnotation);
                        }
                    }
                }
            }

            // Save the merged PDF containing all annotation layers
            target.Save(outputFile);
        }

        Console.WriteLine($"Merged PDF with combined annotations saved to '{outputFile}'.");
    }
}