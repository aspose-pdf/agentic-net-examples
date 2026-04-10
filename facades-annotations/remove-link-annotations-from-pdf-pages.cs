using System;
using System.IO;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Annotations;

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

        // Bind the PDF to the annotation editor facade
        using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
        {
            editor.BindPdf(inputPath);

            // Extract all Link annotations on pages 3 through 7
            string[] linkTypes = new string[] { "Link" };
            var links = editor.ExtractAnnotations(3, 7, linkTypes);

            // Delete each extracted link annotation by its name
            foreach (Annotation annot in links)
            {
                // Annotation.Name may be null; skip if unavailable
                if (!string.IsNullOrEmpty(annot.Name))
                {
                    editor.DeleteAnnotation(annot.Name);
                }
            }

            // Save the modified PDF
            editor.Save(outputPath);
        }

        Console.WriteLine($"Link annotations on pages 3‑7 removed. Saved to '{outputPath}'.");
    }
}