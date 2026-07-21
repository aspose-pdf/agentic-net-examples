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

        // Initialize the facade and bind the PDF document
        PdfAnnotationEditor editor = new PdfAnnotationEditor();
        editor.BindPdf(inputPdf);

        // Access the underlying Document object
        Document doc = editor.Document;

        // Iterate through all pages (1‑based indexing)
        for (int pageNum = 1; pageNum <= doc.Pages.Count; pageNum++)
        {
            Page page = doc.Pages[pageNum];

            // Iterate through all annotations on the current page
            foreach (Annotation annotation in page.Annotations)
            {
                // TextAnnotation and PopupAnnotation expose the Open property
                switch (annotation)
                {
                    case TextAnnotation textAnn:
                        textAnn.Open = true;
                        break;
                    case PopupAnnotation popupAnn:
                        popupAnn.Open = true;
                        break;
                    // Other annotation types do not have an Open property; ignore them
                }
            }
        }

        // Save the modified PDF
        editor.Save(outputPdf);

        // Release resources held by the facade
        editor.Close();

        Console.WriteLine($"All annotation Open flags set to true and saved to '{outputPdf}'.");
    }
}