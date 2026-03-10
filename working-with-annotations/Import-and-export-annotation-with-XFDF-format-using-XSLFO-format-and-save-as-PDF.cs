using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Paths for the source PDF, intermediate XFDF file and the final PDF.
        const string inputPdfPath  = "input.pdf";
        const string xfdfPath      = "annotations.xfdf";
        const string outputPdfPath = "output.pdf";

        // Verify that the source PDF exists.
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Source PDF not found: {inputPdfPath}");
            return;
        }

        // ------------------------------------------------------------
        // Export existing annotations from the PDF to an XFDF file.
        // ------------------------------------------------------------
        PdfAnnotationEditor exporter = new PdfAnnotationEditor();
        // Bind the PDF file to the editor.
        exporter.BindPdf(inputPdfPath);

        // Create (or overwrite) the XFDF file and write the annotations.
        using (FileStream xfdfOut = File.Create(xfdfPath))
        {
            exporter.ExportAnnotationsToXfdf(xfdfOut);
        }

        // Release resources held by the editor.
        exporter.Close();

        // ------------------------------------------------------------
        // Import the previously exported annotations back into the PDF
        // and save the result as a new PDF file.
        // ------------------------------------------------------------
        PdfAnnotationEditor importer = new PdfAnnotationEditor();
        // Bind the same PDF file (or a fresh copy) to the editor.
        importer.BindPdf(inputPdfPath);

        // Import all annotations from the XFDF file.
        importer.ImportAnnotationsFromXfdf(xfdfPath);

        // Save the modified PDF to the output path.
        importer.Save(outputPdfPath);

        // Release resources.
        importer.Close();

        Console.WriteLine($"Annotations exported to '{xfdfPath}' and re‑imported into '{outputPdfPath}'.");
    }
}