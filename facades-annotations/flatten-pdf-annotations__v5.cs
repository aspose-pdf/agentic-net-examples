using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "flattened.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF document
            using (Document doc = new Document(inputPath))
            {
                // Bind the document to PdfAnnotationEditor and flatten annotations
                using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
                {
                    editor.BindPdf(doc);
                    // Flatten all annotations (redactions are applied automatically)
                    // The method does not trigger form events, matching the intended CallEvents = false behavior.
                    editor.FlatteningAnnotations();
                }

                // Save the flattened PDF
                doc.Save(outputPath);
            }

            Console.WriteLine($"Flattened PDF saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
