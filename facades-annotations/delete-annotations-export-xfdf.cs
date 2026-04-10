using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string cleanedPdf = "cleaned.pdf";
        const string xfdfFile = "remaining_annotations.xfdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Initialize the annotation editor and bind the PDF document
        PdfAnnotationEditor editor = new PdfAnnotationEditor();
        editor.BindPdf(inputPdf);

        // Delete all annotations of a specific type (e.g., Text annotations)
        editor.DeleteAnnotations("Text");

        // Save the PDF after deletion
        editor.Save(cleanedPdf);

        // Export the remaining annotations (all types) to an XFDF file
        using (FileStream xfdfStream = File.Create(xfdfFile))
        {
            editor.ExportAnnotationsToXfdf(xfdfStream);
        }

        // Release resources
        editor.Close();

        Console.WriteLine($"Deleted 'Text' annotations. Clean PDF saved to '{cleanedPdf}'.");
        Console.WriteLine($"Remaining annotations exported to XFDF file '{xfdfFile}'.");
    }
}