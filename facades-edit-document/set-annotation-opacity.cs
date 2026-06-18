using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputPdf = "output.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Use PdfAnnotationEditor (Facade) to work with annotations
        using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
        {
            // Bind the PDF document
            editor.BindPdf(inputPdf);

            // Iterate through all pages and their annotations
            foreach (Page page in editor.Document.Pages)
            {
                foreach (Annotation ann in page.Annotations)
                {
                    // Only markup annotations have the Opacity property
                    if (ann is MarkupAnnotation markup)
                    {
                        markup.Opacity = 0.5; // 50% transparent
                    }
                }
            }

            // Save the modified PDF
            editor.Save(outputPdf);
        }

        Console.WriteLine($"Annotation opacity set to 0.5 and saved as '{outputPdf}'.");
    }
}