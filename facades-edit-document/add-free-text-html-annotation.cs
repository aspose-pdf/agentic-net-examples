using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;                // PdfContentEditor
using Aspose.Pdf.Annotations;            // FreeTextAnnotation

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_with_html_annotation.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the PDF and ensure deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Initialize the facade for editing content
            PdfContentEditor editor = new PdfContentEditor();
            editor.BindPdf(doc);

            // Define the annotation rectangle using System.Drawing.Rectangle (required by CreateFreeText)
            // Position: lower‑left (100, 500), width 200, height 100
            System.Drawing.Rectangle rect = new System.Drawing.Rectangle(100, 500, 200, 100);

            // Create a free‑text annotation on page 1 (contents empty for now)
            editor.CreateFreeText(rect, string.Empty, 1);

            // Retrieve the newly added annotation (Aspose.Pdf.Rectangle is used internally by the annotation)
            Annotation rawAnn = doc.Pages[1].Annotations[doc.Pages[1].Annotations.Count];
            if (rawAnn is FreeTextAnnotation freeText)
            {
                // Set the visual appearance (optional)
                freeText.Color = Aspose.Pdf.Color.Yellow;

                // Embed HTML content – this will be rendered as rich text
                freeText.RichText = "<b>Bold Text</b> and <i>Italic Text</i> with <u>underline</u>.";

                // You can also set a plain Contents fallback
                freeText.Contents = "Bold Text and Italic Text with underline.";
            }

            // Save the modified PDF
            editor.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with HTML‑rich free‑text annotation: {outputPath}");
    }
}
