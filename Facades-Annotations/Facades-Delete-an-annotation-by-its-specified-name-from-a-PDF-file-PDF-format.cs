using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main(string[] args)
    {
        // Arguments: input PDF path, annotation name to delete, output PDF path
        if (args.Length < 3)
        {
            Console.WriteLine("Usage: <inputPdf> <annotationName> <outputPdf>");
            return;
        }

        string inputPath = args[0];
        string annotationName = args[1];
        string outputPath = args[2];

        // Verify input file exists
        if (!File.Exists(inputPath))
        {
            Console.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF document
            Document pdfDocument = new Document(inputPath);

            bool annotationDeleted = false;

            // Pages collection is 1‑based
            for (int pageIndex = 1; pageIndex <= pdfDocument.Pages.Count; pageIndex++)
            {
                Page page = pdfDocument.Pages[pageIndex];

                // Find annotation by its Name property
                Annotation annotation = page.Annotations.FindByName(annotationName);
                if (annotation != null)
                {
                    // Delete the found annotation
                    page.Annotations.Delete(annotation);
                    annotationDeleted = true;
                    // Assuming unique names; exit after deletion
                    break;
                }
            }

            if (!annotationDeleted)
            {
                Console.WriteLine($"Annotation named '{annotationName}' was not found.");
            }

            // Save the modified PDF
            pdfDocument.Save(outputPath);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error processing PDF: {ex.Message}");
        }
    }
}