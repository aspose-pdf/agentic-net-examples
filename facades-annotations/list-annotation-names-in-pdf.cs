using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Initialize the annotation editor and bind the document
            using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
            {
                editor.BindPdf(doc);

                // Extract all annotations from the first to the last page
                int startPage = 1;
                int endPage   = doc.Pages.Count;

                // Passing null for annotation types retrieves all types
                IList<Annotation> annotations = editor.ExtractAnnotations(startPage, endPage, (string[])null);

                Console.WriteLine($"Total annotations found: {annotations.Count}");

                // List each annotation's name (or indicate if none)
                foreach (Annotation ann in annotations)
                {
                    string name = ann.Name ?? "(no name)";
                    Console.WriteLine($"Annotation Name: {name}");
                }
            }
        }
    }
}