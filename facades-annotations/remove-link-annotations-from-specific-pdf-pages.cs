using System;
using System.IO;
using System.Collections.Generic;
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

        // Use PdfAnnotationEditor to work with annotations
        using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
        {
            // Load the PDF document
            editor.BindPdf(inputPath);

            // Extract all Link annotations on pages 3 through 7
            string[] linkTypes = new string[] { "Link" };
            IList<Annotation> links = editor.ExtractAnnotations(3, 7, linkTypes);

            // Delete each extracted link annotation by its name
            foreach (Annotation link in links)
            {
                // Ensure the annotation has a name (required for DeleteAnnotation)
                if (!string.IsNullOrEmpty(link.Name))
                {
                    editor.DeleteAnnotation(link.Name);
                }
            }

            // Save the modified PDF
            editor.Save(outputPath);
            editor.Close();
        }

        Console.WriteLine($"Link annotations on pages 3‑7 removed. Saved to '{outputPath}'.");
    }
}