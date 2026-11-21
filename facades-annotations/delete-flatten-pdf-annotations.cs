using System;
using System.IO;
using Aspose.Pdf.Facades;

class PdfAccessibilityProcessor
{
    // Deletes all annotations, flattens any remaining ones, and saves the result.
    // The whole operation is performed with Aspose.Pdf.Facades.PdfAnnotationEditor.
    public static void DeleteAndFlatten(string inputPdfPath, string outputPdfPath)
    {
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Create the editor and bind the source PDF.
        PdfAnnotationEditor editor = new PdfAnnotationEditor();
        editor.BindPdf(inputPdfPath);

        // Delete all annotations from the bound document.
        editor.DeleteAnnotations();

        // Flatten any remaining annotations (if any).
        editor.FlatteningAnnotations();

        // Save the processed PDF to the desired output path.
        editor.Save(outputPdfPath);

        Console.WriteLine($"Processed PDF saved to '{outputPdfPath}'.");
    }

    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_processed.pdf";

        DeleteAndFlatten(inputPath, outputPath);
    }
}
