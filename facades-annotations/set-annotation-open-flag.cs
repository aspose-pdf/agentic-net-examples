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

        // Bind the PDF using the Facade API
        PdfAnnotationEditor editor = new PdfAnnotationEditor();
        editor.BindPdf(inputPath);

        // Access the underlying Document object
        Document doc = editor.Document;

        // Iterate through all pages (1‑based indexing)
        for (int pageIndex = 1; pageIndex <= doc.Pages.Count; pageIndex++)
        {
            Page page = doc.Pages[pageIndex];

            // Iterate through all annotations on the page (1‑based indexing)
            for (int annIndex = 1; annIndex <= page.Annotations.Count; annIndex++)
            {
                Annotation annotation = page.Annotations[annIndex];

                // Set the Open flag where the property exists
                if (annotation is TextAnnotation textAnn)
                {
                    textAnn.Open = true;
                }
                else if (annotation is PopupAnnotation popupAnn)
                {
                    popupAnn.Open = true;
                }
                // Other annotation types do not expose an Open property
            }
        }

        // Save the modified PDF via the facade
        editor.Save(outputPath);
        editor.Close();

        Console.WriteLine($"All annotation Open flags set to true and saved to '{outputPath}'.");
    }
}