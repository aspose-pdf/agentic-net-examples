using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Bind the document to the PdfAnnotationEditor facade
            PdfAnnotationEditor editor = new PdfAnnotationEditor(doc);

            // Iterate all pages (Aspose.Pdf uses 1‑based indexing)
            for (int pageIdx = 1; pageIdx <= doc.Pages.Count; pageIdx++)
            {
                Page page = doc.Pages[pageIdx];

                // Iterate all annotations on the current page (also 1‑based)
                for (int annIdx = 1; annIdx <= page.Annotations.Count; annIdx++)
                {
                    Annotation ann = page.Annotations[annIdx];

                    // TextAnnotation and PopupAnnotation expose the Open property
                    if (ann is TextAnnotation txtAnn)
                    {
                        txtAnn.Open = true;
                    }
                    else if (ann is PopupAnnotation popAnn)
                    {
                        popAnn.Open = true;
                    }
                    // Other annotation types do not have an Open flag; they are ignored
                }
            }

            // Save the modified PDF via the facade
            editor.Save(outputPath);
            editor.Close(); // optional, releases resources held by the facade
        }

        Console.WriteLine($"All annotation Open flags set to true. Saved to '{outputPath}'.");
    }
}