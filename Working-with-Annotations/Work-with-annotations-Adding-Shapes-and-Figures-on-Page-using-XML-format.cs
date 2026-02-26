using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Input PDF that will receive the annotations
        const string inputPdfPath = "input.pdf";
        // XFDF (XML) file that defines shapes and figure annotations
        const string xfdfPath = "annotations.xfdf";
        // Output PDF with the imported annotations
        const string outputPdfPath = "output.pdf";

        // Verify that the required files exist
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Error: PDF file not found – {inputPdfPath}");
            return;
        }

        if (!File.Exists(xfdfPath))
        {
            Console.Error.WriteLine($"Error: XFDF file not found – {xfdfPath}");
            return;
        }

        // Bind the PDF document to the annotation editor
        Aspose.Pdf.Facades.PdfAnnotationEditor editor = new Aspose.Pdf.Facades.PdfAnnotationEditor();
        editor.BindPdf(inputPdfPath);

        // Import all annotations (shapes, figures, etc.) defined in the XFDF file
        editor.ImportAnnotationsFromXfdf(xfdfPath);

        // Save the modified PDF
        editor.Save(outputPdfPath);

        Console.WriteLine($"Annotations imported successfully. Output saved to '{outputPdfPath}'.");
    }
}