using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";          // source PDF
        const string outputPdf = "output.pdf";         // PDF after deletion
        const string xfdfPath  = "remaining_annotations.xfdf"; // XFDF output

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // PdfAnnotationEditor implements IDisposable, so wrap it in a using block
        using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
        {
            // Load the PDF document into the editor
            editor.BindPdf(inputPdf);

            // Delete all annotations of a specific type, e.g., "Highlight"
            editor.DeleteAnnotations("Highlight");

            // Save the modified PDF (annotations of the specified type are removed)
            editor.Save(outputPdf);

            // Export the remaining annotations to an XFDF file
            using (FileStream xfdfStream = File.Create(xfdfPath))
            {
                editor.ExportAnnotationsToXfdf(xfdfStream);
            }
        }

        Console.WriteLine($"Processed PDF saved to '{outputPdf}'.");
        Console.WriteLine($"Remaining annotations exported to '{xfdfPath}'.");
    }
}