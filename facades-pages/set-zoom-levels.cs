using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string previewPath = "preview.pdf";
        const string finalPath = "final.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Set preview zoom to 0.5 using PdfPageEditor
        PdfPageEditor previewEditor = new PdfPageEditor();
        previewEditor.BindPdf(inputPath);
        previewEditor.Zoom = 0.5f;
        previewEditor.Save(previewPath);

        // Set final output zoom to 1.0 using PdfViewer (affects rendering)
        PdfViewer viewer = new PdfViewer();
        viewer.BindPdf(inputPath);
        viewer.ScaleFactor = 1.0f;

        // Save the final PDF (no modifications, just demonstration of zoom setting)
        using (Document finalDoc = new Document(inputPath))
        {
            finalDoc.Save(finalPath);
        }

        Console.WriteLine("Preview and final PDFs have been generated.");
    }
}
