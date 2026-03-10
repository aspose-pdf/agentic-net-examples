using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";
        const string xfdfPath = "annotations.xfdf";
        const string outputPdf = "output.pdf";
        const string outputXfdf = "updated_annotations.xfdf";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF not found: {pdfPath}");
            return;
        }

        if (!File.Exists(xfdfPath))
        {
            Console.Error.WriteLine($"XFDF not found: {xfdfPath}");
            return;
        }

        // Initialize the annotation editor and bind the PDF document
        PdfAnnotationEditor editor = new PdfAnnotationEditor();
        editor.BindPdf(pdfPath);

        // Import all annotations from the XFDF file into the PDF
        editor.ImportAnnotationsFromXfdf(xfdfPath);

        // Save the PDF with the newly imported annotations
        editor.Save(outputPdf);

        // Export the current annotations back to an XFDF file
        using (FileStream xfdfOut = File.Create(outputXfdf))
        {
            editor.ExportAnnotationsToXfdf(xfdfOut);
        }

        // Release resources held by the editor
        editor.Close();

        Console.WriteLine($"PDF saved to '{outputPdf}'.");
        Console.WriteLine($"Annotations exported to '{outputXfdf}'.");
    }
}