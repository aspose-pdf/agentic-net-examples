using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Facades;
// Alias the nested FlattenSettings class to avoid ambiguity with Aspose.Pdf.Facades.Form
using FlattenSettings = Aspose.Pdf.Forms.Form.FlattenSettings;

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

        // Create flatten settings instance and disable form events
        var flattenSettings = new FlattenSettings { CallEvents = false };

        // Bind the PDF to the annotation editor
        using (var editor = new PdfAnnotationEditor())
        {
            editor.BindPdf(inputPath);

            // Retrieve the first annotation on page 7 (pages and annotation collections are 1‑based)
            if (editor.Document.Pages.Count < 7)
            {
                Console.Error.WriteLine("The document does not contain page 7.");
                return;
            }

            var page = editor.Document.Pages[7];
            if (page.Annotations.Count < 1)
            {
                Console.Error.WriteLine("Annotation not found on page 7, index 1.");
                return;
            }

            Annotation annotation = page.Annotations[1]; // 1‑based index

            // Change the annotation colour to orange
            annotation.Color = Color.Orange;

            // Flatten annotations using the explicit settings (CallEvents = false)
            editor.FlatteningAnnotations(flattenSettings);

            // Save the modified PDF
            editor.Save(outputPath);
        }

        Console.WriteLine($"Modified PDF saved to '{outputPath}'.");
    }
}
