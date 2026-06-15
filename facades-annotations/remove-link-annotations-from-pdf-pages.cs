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

        // Use PdfAnnotationEditor (facade) to work with annotations
        using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
        {
            // Load the PDF document
            editor.BindPdf(inputPath);

            // Define the page range (pages are 1‑based)
            int startPage = 3;
            int endPage   = 7;

            // Extract only Link annotations within the specified range
            string[] annotTypes = new string[] { "Link" };
            var linkAnnotations = editor.ExtractAnnotations(startPage, endPage, annotTypes);

            // Delete each extracted link annotation by its name
            foreach (Annotation annot in linkAnnotations)
            {
                if (!string.IsNullOrEmpty(annot.Name))
                {
                    editor.DeleteAnnotation(annot.Name);
                }
            }

            // Save the modified PDF
            editor.Save(outputPath);
        }

        Console.WriteLine($"Link annotations on pages {3}-{7} have been removed. Saved to '{outputPath}'.");
    }
}