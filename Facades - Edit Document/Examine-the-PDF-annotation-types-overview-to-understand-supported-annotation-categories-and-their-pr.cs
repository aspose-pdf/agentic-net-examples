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
        const string inputPath = "sample.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Initialize the annotation editor and bind the loaded document
            PdfAnnotationEditor editor = new PdfAnnotationEditor();
            editor.BindPdf(doc);

            // Retrieve all possible annotation types – an empty array means "no filter"
            AnnotationType[] allTypes = Array.Empty<AnnotationType>();

            // Extract annotations from the first to the last page
            IList<Annotation> annotations = editor.ExtractAnnotations(1, doc.Pages.Count, allTypes);

            Console.WriteLine($"Total annotations found: {annotations.Count}");

            // Display key properties of each annotation
            foreach (Annotation ann in annotations)
            {
                // Base properties are always available
                Console.WriteLine($"Type: {ann.AnnotationType}");
                Console.WriteLine($"  Contents : {ann.Contents}");
                Console.WriteLine($"  Modified : {ann.Modified}");

                // Markup‑specific properties (Title, Subject, Color)
                if (ann is MarkupAnnotation markup)
                {
                    Console.WriteLine($"  Title   : {markup.Title}");
                    Console.WriteLine($"  Subject : {markup.Subject}");
                    Console.WriteLine($"  Color   : {markup.Color}");
                }

                // The "Open" property exists only on TextAnnotation (a derived type of MarkupAnnotation)
                if (ann is TextAnnotation textAnn)
                {
                    Console.WriteLine($"  Open    : {textAnn.Open}");
                }

                Console.WriteLine();
            }

            // Example operation: delete all Highlight annotations and save the result
            // DeleteAnnotations(string) expects the annotation type name (case‑insensitive).
            editor.DeleteAnnotations("Highlight");
            editor.Save("sample_without_highlights.pdf");
        }

        Console.WriteLine("Annotation processing completed.");
    }
}
