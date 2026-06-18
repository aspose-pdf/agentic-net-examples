using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Annotations;

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

        // Use PdfContentEditor (Facade) to add a free‑text annotation
        using (PdfContentEditor editor = new PdfContentEditor())
        {
            // Bind the existing PDF document
            editor.BindPdf(inputPdf);

            // Define the annotation rectangle (System.Drawing.Rectangle is required by the API)
            System.Drawing.Rectangle rect = new System.Drawing.Rectangle(100, 500, 200, 100);

            // Create the free‑text annotation on page 1
            editor.CreateFreeText(rect, "Important note", 1);

            // Access the underlying Document to modify the annotation appearance
            Document doc = editor.Document;
            Page page = doc.Pages[1];

            // The newly added annotation is the last one in the collection
            Annotation annotation = page.Annotations[page.Annotations.Count];

            // Apply a light gray background color to the annotation
            if (annotation is FreeTextAnnotation freeText)
            {
                freeText.Color = Aspose.Pdf.Color.LightGray; // sets the annotation's background/border color
            }

            // Save the modified PDF
            editor.Save(outputPdf);
        }

        Console.WriteLine($"Annotation with light gray background saved to '{outputPdf}'.");
    }
}