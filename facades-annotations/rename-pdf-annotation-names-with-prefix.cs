using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Annotations; // Added namespace for Annotation

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputPdf = "renamed_annotations.pdf";
        const string prefix    = "Std_";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Bind the PDF to the annotation editor facade
        using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
        {
            editor.BindPdf(inputPdf);

            // Access the underlying Document through the facade
            Document doc = editor.Document;

            int counter = 1;
            // Pages are 1‑based in Aspose.Pdf
            for (int pageIndex = 1; pageIndex <= doc.Pages.Count; pageIndex++)
            {
                Page page = doc.Pages[pageIndex];

                // Annotations collection is also 1‑based
                for (int annotIndex = 1; annotIndex <= page.Annotations.Count; annotIndex++)
                {
                    Annotation annot = page.Annotations[annotIndex];
                    // Rename the annotation using the standardized prefix
                    annot.Name = $"{prefix}{counter}";
                    counter++;
                }
            }

            // Save the modified PDF
            editor.Save(outputPdf);
        }

        Console.WriteLine($"Annotations renamed and saved to '{outputPdf}'.");
    }
}
