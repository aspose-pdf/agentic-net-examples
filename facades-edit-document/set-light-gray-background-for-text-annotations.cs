using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "annotated_output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document (ensures proper disposal)
        using (Document doc = new Document(inputPath))
        {
            // Initialize the annotation editor and bind it to the loaded document
            PdfAnnotationEditor editor = new PdfAnnotationEditor();
            editor.BindPdf(doc);

            // TextAnnotation does not have a parameter‑less constructor.
            // Use the (Page, Rectangle) constructor. The rectangle can be zero‑size
            // because we only need the instance to convey the property values to
            // ModifyAnnotations.
            Page firstPage = doc.Pages[1];
            var dummyRect = new Aspose.Pdf.Rectangle(0, 0, 0, 0);
            TextAnnotation template = new TextAnnotation(firstPage, dummyRect)
            {
                // Light gray background for readability on dark pages
                Color = Aspose.Pdf.Color.LightGray
            };

            // Apply the color change to all text annotations on all pages.
            // Page indexing in Aspose.Pdf is 1‑based.
            editor.ModifyAnnotations(1, doc.Pages.Count, template);

            // Save the modified PDF.
            editor.Save(outputPath);

            // Release resources held by the editor.
            editor.Close();
        }

        Console.WriteLine($"Annotated PDF saved to '{outputPath}'.");
    }
}