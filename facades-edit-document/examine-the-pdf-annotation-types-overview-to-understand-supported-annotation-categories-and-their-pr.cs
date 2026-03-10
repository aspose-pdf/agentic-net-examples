using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        // Path to the PDF file to analyze
        const string inputPath = "sample.pdf";

        // ------------------------------------------------------------
        // Ensure the PDF exists – create a minimal one if it does not.
        // ------------------------------------------------------------
        if (!File.Exists(inputPath))
        {
            Console.WriteLine($"File '{inputPath}' not found. Creating a placeholder PDF.");
            var placeholder = new Document();
            placeholder.Pages.Add(); // add a single blank page
            placeholder.Save(inputPath);
        }

        // Load the PDF to obtain page count (required for extraction range)
        Document doc = new Document(inputPath);

        // ------------------------------------------------------------
        // 1. List all supported annotation categories (AnnotationType enum)
        // ------------------------------------------------------------
        Console.WriteLine("Supported annotation types:");
        foreach (AnnotationType type in Enum.GetValues(typeof(AnnotationType)))
        {
            Console.WriteLine($"- {type}");
        }

        // ------------------------------------------------------------
        // 2. Extract annotations from the whole document and display details
        // ------------------------------------------------------------
        PdfAnnotationEditor editor = new PdfAnnotationEditor();
        editor.BindPdf(inputPath);

        // Prepare an array with all annotation types for extraction
        AnnotationType[] allTypes = (AnnotationType[])Enum.GetValues(typeof(AnnotationType));

        // Extract annotations from page 1 to the last page
        IList<Annotation> annotations = editor.ExtractAnnotations(1, doc.Pages.Count, allTypes);

        Console.WriteLine($"\nFound {annotations.Count} annotation(s) in the document:\n");

        foreach (Annotation annot in annotations)
        {
            // Basic information common to all annotations
            Console.WriteLine($"Annotation Type : {annot.AnnotationType}");
            Console.WriteLine($"  Subtype       : {annot.AnnotationType}"); // Subtype not exposed on base class; using AnnotationType as a substitute

            // Title and Open are only available on markup‑type annotations (e.g., TextAnnotation)
            if (annot is TextAnnotation textAnnot)
            {
                Console.WriteLine($"  Title         : {textAnnot.Title ?? "N/A"}");
                Console.WriteLine($"  Open          : {textAnnot.Open}");
            }
            else if (annot is MarkupAnnotation markupAnnot)
            {
                // MarkupAnnotation provides Title but not Open in this API version
                Console.WriteLine($"  Title         : {markupAnnot.Title ?? "N/A"}");
                Console.WriteLine($"  Open          : N/A");
            }
            else
            {
                Console.WriteLine($"  Title         : N/A");
                Console.WriteLine($"  Open          : N/A");
            }

            Console.WriteLine($"  Color         : {(annot.Color != null ? annot.Color.ToString() : "None")}");
            Console.WriteLine($"  Rect          : {annot.Rect}");
            Console.WriteLine();
        }

        // ------------------------------------------------------------
        // 3. (Optional) Save a copy of the PDF after processing
        // ------------------------------------------------------------
        const string outputPath = "sample_processed.pdf";
        editor.Save(outputPath);
        Console.WriteLine($"Processed PDF saved to '{outputPath}'.");
    }
}
