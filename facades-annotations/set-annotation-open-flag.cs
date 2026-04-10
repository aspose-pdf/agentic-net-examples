using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Bind the PDF using PdfAnnotationEditor (facade API)
        using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
        {
            editor.BindPdf(inputPath);

            // Access the underlying Document to iterate pages and annotations
            Document doc = editor.Document;

            // Pages are 1‑based in Aspose.Pdf
            for (int pageIndex = 1; pageIndex <= doc.Pages.Count; pageIndex++)
            {
                Page page = doc.Pages[pageIndex];

                // Annotations collection is also 1‑based
                for (int annIndex = 1; annIndex <= page.Annotations.Count; annIndex++)
                {
                    Annotation annotation = page.Annotations[annIndex];

                    // TextAnnotation and PopupAnnotation expose the Open property
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

            // Save the modified PDF
            editor.Save(outputPath);
            editor.Close();
        }

        Console.WriteLine($"All annotation Open flags set to true. Saved to '{outputPath}'.");
    }
}