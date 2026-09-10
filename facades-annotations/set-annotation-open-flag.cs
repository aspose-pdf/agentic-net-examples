using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Facades;

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

        // Load the PDF document with deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Initialize the annotation editor facade and bind the document
            PdfAnnotationEditor editor = new PdfAnnotationEditor();
            editor.BindPdf(doc);

            // Iterate all pages (Aspose.Pdf uses 1‑based indexing)
            for (int pageIndex = 1; pageIndex <= doc.Pages.Count; pageIndex++)
            {
                Page page = doc.Pages[pageIndex];

                // Iterate all annotations on the current page
                for (int annIndex = 1; annIndex <= page.Annotations.Count; annIndex++)
                {
                    Annotation annotation = page.Annotations[annIndex];

                    // Set Open flag for annotations that expose the property
                    if (annotation is TextAnnotation textAnn)
                    {
                        textAnn.Open = true;
                    }
                    else if (annotation is PopupAnnotation popupAnn)
                    {
                        popupAnn.Open = true;
                    }
                }
            }

            // Save the modified PDF via the facade
            editor.Save(outputPath);
        }

        Console.WriteLine($"All annotation Open flags set to true → '{outputPath}'");
    }
}