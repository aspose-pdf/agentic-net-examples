using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";
        const string tempXfdf = "temp.xfdf";
        const string tempEdited = "temp_edited.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // 1. Export existing annotations to XFDF (preserves all annotation data)
        using (PdfAnnotationEditor exportEditor = new PdfAnnotationEditor())
        {
            exportEditor.BindPdf(inputPath);
            using (FileStream xfdfStream = File.Create(tempXfdf))
            {
                exportEditor.ExportAnnotationsToXfdf(xfdfStream);
            }
        }

        // 2. Perform content editing (e.g., replace placeholder text) while keeping annotations separate
        using (PdfContentEditor contentEditor = new PdfContentEditor())
        {
            contentEditor.BindPdf(inputPath);
            // Example edit: replace all occurrences of "PLACEHOLDER" with "New Text"
            contentEditor.ReplaceText("PLACEHOLDER", "New Text");
            // Save the edited PDF to a temporary file
            contentEditor.Save(tempEdited);
        }

        // 3. Import the previously exported annotations back into the edited PDF
        using (PdfAnnotationEditor importEditor = new PdfAnnotationEditor())
        {
            importEditor.BindPdf(tempEdited);
            importEditor.ImportAnnotationsFromXfdf(tempXfdf);
            // Save the final document with annotations correctly positioned
            importEditor.Save(outputPath);
        }

        // Clean up temporary files
        try { File.Delete(tempXfdf); } catch { }
        try { File.Delete(tempEdited); } catch { }

        Console.WriteLine($"Edited PDF saved to '{outputPath}' with original annotations preserved.");
    }
}