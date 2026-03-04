using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf.Facades;          // Facade classes for annotation handling
using Aspose.Pdf.Annotations;      // Annotation types and enum

class Program
{
    static void Main()
    {
        const string inputPath = "sample.pdf";

        // Verify that the source PDF exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Use PdfAnnotationEditor to work with annotations.
        // The class implements IDisposable, so wrap it in a using block.
        using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
        {
            // Bind the PDF file to the editor.
            editor.BindPdf(inputPath);

            // Determine the total number of pages (1‑based indexing).
            int pageCount = editor.Document.Pages.Count;

            // Retrieve all possible annotation types.
            AnnotationType[] allTypes = (AnnotationType[])Enum.GetValues(typeof(AnnotationType));

            // Extract all annotations from every page.
            IList<Annotation> annotations = editor.ExtractAnnotations(1, pageCount, allTypes);

            Console.WriteLine($"Total annotations found: {annotations.Count}");

            // Iterate through the extracted annotations and display key properties.
            foreach (Annotation annot in annotations)
            {
                // AnnotationType is an enum indicating the specific kind (Text, Highlight, etc.).
                // PageIndex is 1‑based.
                Console.WriteLine($"Page: {annot.PageIndex}, Type: {annot.AnnotationType}, Name: {annot.Name}, Contents: {annot.Contents}");
            }

            // Example operation: flatten all annotations (convert them to regular page content).
            editor.FlatteningAnnotations();

            // Save the modified PDF to a new file.
            const string outputPath = "flattened_output.pdf";
            editor.Save(outputPath);
            Console.WriteLine($"Flattened PDF saved to '{outputPath}'.");
        }
    }
}