using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Paths – adjust as needed
        const string inputPdfPath  = "input.pdf";
        const string outputPdfPath = "output.pdf";
        const string xfdfPath      = "annotations.xfdf";

        // Verify input file exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Initialize the annotation editor and bind it to the loaded document
            PdfAnnotationEditor editor = new PdfAnnotationEditor();
            editor.BindPdf(pdfDoc);

            // -------------------------------------------------
            // Export all annotations to an XFDF file
            // -------------------------------------------------
            using (FileStream xfdfExportStream = new FileStream(xfdfPath, FileMode.Create, FileAccess.Write))
            {
                editor.ExportAnnotationsToXfdf(xfdfExportStream);
            }
            Console.WriteLine($"Annotations exported to '{xfdfPath}'.");

            // (Optional) Remove all annotations from the document to demonstrate import
            // editor.DeleteAnnotations();

            // -------------------------------------------------
            // Import annotations back from the XFDF file
            // -------------------------------------------------
            using (FileStream xfdfImportStream = new FileStream(xfdfPath, FileMode.Open, FileAccess.Read))
            {
                editor.ImportAnnotationsFromXfdf(xfdfImportStream);
            }
            Console.WriteLine("Annotations imported from XFDF.");

            // Save the modified PDF to a new file
            editor.Save(outputPdfPath);
            Console.WriteLine($"Modified PDF saved to '{outputPdfPath}'.");

            // Clean up the editor (closes the bound document internally)
            editor.Close();
        }
    }
}