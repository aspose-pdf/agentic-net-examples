using System;
using System.IO;
using System.Threading.Tasks;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static async Task Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        using (Document doc = new Document(inputPath))
        {
            using (PdfAnnotationEditor editor = new PdfAnnotationEditor(doc))
            {
                // Perform the annotation operation asynchronously to avoid blocking the UI thread.
                await Task.Run(() => editor.DeleteAnnotations());

                // Save the modified document.
                doc.Save(outputPath);
            }
        }

        Console.WriteLine($"Annotations processed asynchronously and saved to '{outputPath}'.");
    }
}