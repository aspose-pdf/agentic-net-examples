using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load PDF and get original rotation
        using (PdfPageEditor editor = new PdfPageEditor())
        {
            editor.BindPdf(inputPath);
            int originalRotation = editor.GetPageRotation(1);
            Console.WriteLine($"Original rotation of page 1: {originalRotation} degrees");

            // Change page size (e.g., to A4)
            editor.PageSize = PageSize.A4; // PageSize enum is in Aspose.Pdf namespace

            // Apply changes and save
            editor.ApplyChanges();
            editor.Save(outputPath);
        }

        // Verify that rotation remains unchanged after resizing
        using (PdfPageEditor verifier = new PdfPageEditor())
        {
            verifier.BindPdf(outputPath);
            int newRotation = verifier.GetPageRotation(1);
            Console.WriteLine($"Rotation after resizing page 1: {newRotation} degrees");
        }
    }
}
