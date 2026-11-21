using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "signed_input.pdf";
        const string outputPath = "modified_output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // PdfPageEditor provides page‑level operations without loading the whole document.
        // In recent Aspose.PDF versions the PreserveSignature property has been removed.
        // To rotate a page, specify the target pages via ProcessPages and set the Rotation value (0, 90, 180, 270).
        PdfPageEditor pageEditor = new PdfPageEditor();
        pageEditor.BindPdf(inputPath);

        // Target the first page (1‑based index).
        pageEditor.ProcessPages = new int[] { 1 };
        // Rotate 90 degrees clockwise.
        pageEditor.Rotation = 90;

        // Save the modified PDF to a new file.
        pageEditor.Save(outputPath);
        pageEditor.Close();

        Console.WriteLine($"Modified PDF saved to '{outputPath}'.");
    }
}
