using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Annotations;

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

        using (Document doc = new Document(inputPath))
        {
            // Flatten all annotations using the facade
            PdfAnnotationEditor editor = new PdfAnnotationEditor();
            editor.BindPdf(doc);
            editor.FlatteningAnnotations();

            // Save the flattened document
            doc.Save(outputPath);

            // Verify that no annotations remain in the document structure
            bool anyAnnotations = false;
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];
                if (page.Annotations.Count > 0)
                {
                    anyAnnotations = true;
                    break;
                }
            }

            Console.WriteLine(anyAnnotations ? "Annotations still present after flattening." : "No annotations found after flattening.");
        }
    }
}