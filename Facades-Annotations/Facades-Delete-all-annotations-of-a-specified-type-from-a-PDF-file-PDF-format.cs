using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class DeleteAnnotationsByType
{
    static void Main(string[] args)
    {
        // Expect three arguments: input PDF path, output PDF path, annotation type name
        if (args.Length != 3)
        {
            Console.WriteLine("Usage: DeleteAnnotationsByType <input.pdf> <output.pdf> <AnnotationType>");
            Console.WriteLine("Example AnnotationType values: Link, Highlight, Text, Square, etc.");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];
        string typeName = args[2];

        // Verify the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: Input file not found – {inputPath}");
            return;
        }

        // Try to parse the annotation type name to the Aspose.Pdf.Annotations.AnnotationType enum
        if (!Enum.TryParse(typeName, true, out AnnotationType targetType))
        {
            Console.Error.WriteLine($"Error: Invalid annotation type '{typeName}'.");
            return;
        }

        try
        {
            // Load the PDF document
            Document pdfDocument = new Document(inputPath);

            // Iterate through all pages (1‑based indexing)
            for (int pageIndex = 1; pageIndex <= pdfDocument.Pages.Count; pageIndex++)
            {
                Page page = pdfDocument.Pages[pageIndex];
                // Collect annotations of the specified type
                List<Annotation> toDelete = new List<Annotation>();
                foreach (Annotation annot in page.Annotations)
                {
                    if (annot.AnnotationType == targetType)
                        toDelete.Add(annot);
                }

                // Delete the collected annotations
                foreach (Annotation annot in toDelete)
                {
                    page.Annotations.Delete(annot);
                }
            }

            // Save the modified PDF (simple save without options)
            pdfDocument.Save(outputPath);
            Console.WriteLine($"All '{targetType}' annotations have been removed. Output saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}