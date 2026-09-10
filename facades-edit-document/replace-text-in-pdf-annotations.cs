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
        // Input parameters
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";
        const string srcString  = "OldText";
        const string destString = "NewText";
        const int startPage = 1;   // first page to process (1‑based)
        const int endPage   = 3;   // last page to process

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF document (wrapped in using for deterministic disposal)
            using (Document doc = new Document(inputPath))
            {
                // Initialize the annotation editor on the loaded document
                PdfAnnotationEditor editor = new PdfAnnotationEditor(doc);

                // Iterate over the specified page range
                for (int pageNum = startPage; pageNum <= endPage; pageNum++)
                {
                    // Extract all annotation types on the current page
                    IList<Annotation> annotations = editor.ExtractAnnotations(
                        pageNum, pageNum,
                        (AnnotationType[])Enum.GetValues(typeof(AnnotationType)));

                    // Process each annotation
                    foreach (Annotation annot in annotations)
                    {
                        // Only annotations that have a Contents string can be modified
                        if (!string.IsNullOrEmpty(annot.Contents) &&
                            annot.Contents.Contains(srcString))
                        {
                            // Replace the target text within the annotation's contents
                            annot.Contents = annot.Contents.Replace(srcString, destString);

                            // Apply the modification to the annotation on this page
                            // ModifyAnnotations expects a single Annotation instance, not an array
                            editor.ModifyAnnotations(pageNum, pageNum, annot);
                        }
                    }
                }

                // Save the modified document (PDF format)
                doc.Save(outputPath);
            }

            Console.WriteLine($"Annotations updated and saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
