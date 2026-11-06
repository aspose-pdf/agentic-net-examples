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
                // Bind the document to the annotation editor
                using (PdfAnnotationEditor annotEditor = new PdfAnnotationEditor())
                {
                    annotEditor.BindPdf(doc);

                    // Convert interactive annotations to static graphics while preserving appearance
                    annotEditor.FlatteningAnnotations();

                    // Save the flattened PDF
                    annotEditor.Save(outputPath);
                }
            }

            Console.WriteLine($"Annotations flattened and saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
