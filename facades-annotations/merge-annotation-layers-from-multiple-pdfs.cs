using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    // Merges annotation layers from multiple PDFs into a single PDF.
    // All source PDFs are expected to have the same page count.
    // Annotations from the first PDF are kept, and annotations from the
    // remaining PDFs are added to the corresponding pages.
    static void MergeAnnotationLayers(string[] sourcePdfPaths, string outputPdfPath)
    {
        if (sourcePdfPaths == null || sourcePdfPaths.Length == 0)
        {
            Console.Error.WriteLine("No source PDFs provided.");
            return;
        }

        // Load the first PDF as the target document.
        using (Document targetDoc = new Document(sourcePdfPaths[0]))
        {
            // Process each additional PDF.
            for (int srcIndex = 1; srcIndex < sourcePdfPaths.Length; srcIndex++)
            {
                string srcPath = sourcePdfPaths[srcIndex];
                if (!File.Exists(srcPath))
                {
                    Console.Error.WriteLine($"Source file not found: {srcPath}");
                    continue;
                }

                using (Document srcDoc = new Document(srcPath))
                {
                    // Ensure the target has at least as many pages as the source.
                    while (targetDoc.Pages.Count < srcDoc.Pages.Count)
                    {
                        targetDoc.Pages.Add();
                    }

                    // Iterate through pages (1‑based indexing).
                    for (int pageNum = 1; pageNum <= srcDoc.Pages.Count; pageNum++)
                    {
                        Aspose.Pdf.Page srcPage = srcDoc.Pages[pageNum];
                        Aspose.Pdf.Page tgtPage = targetDoc.Pages[pageNum];

                        // Copy each annotation from the source page to the target page.
                        foreach (Annotation annot in srcPage.Annotations)
                        {
                            // Directly add the annotation to the target page.
                            // Aspose.Pdf allows adding an existing annotation instance to another page.
                            tgtPage.Annotations.Add(annot);
                        }
                    }
                }
            }

            // Save the merged document with all annotation layers.
            targetDoc.Save(outputPdfPath);
        }
    }

    // Entry point required for a console application.
    static void Main(string[] args)
    {
        // Example usage:
        // args[0] = output PDF path
        // args[1..n] = input PDF paths to merge annotation layers from
        if (args.Length < 2)
        {
            Console.WriteLine("Usage: MergeAnnotations <output.pdf> <input1.pdf> [<input2.pdf> ...]");
            return;
        }

        string outputPath = args[0];
        string[] inputPaths = new string[args.Length - 1];
        Array.Copy(args, 1, inputPaths, 0, inputPaths.Length);

        MergeAnnotationLayers(inputPaths, outputPath);
        Console.WriteLine($"Merged annotation layers saved to '{outputPath}'.");
    }
}