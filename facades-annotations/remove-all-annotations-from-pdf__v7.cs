using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "cleaned.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF document.
            using (Document doc = new Document(inputPath))
            {
                // PdfAnnotationEditor implements IDisposable, so wrap it in a using block.
                using (PdfAnnotationEditor annotEditor = new PdfAnnotationEditor())
                {
                    // Bind the editor to the loaded document.
                    annotEditor.BindPdf(doc);

                    // Delete all annotations from the document.
                    // The correct method name is DeleteAnnotations (not DeleteAllAnnotations).
                    annotEditor.DeleteAnnotations();
                }

                // Save the cleaned PDF.
                doc.Save(outputPath);
            }

            Console.WriteLine($"All annotations removed. Clean PDF saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
