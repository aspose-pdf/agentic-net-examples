using System;
using System.IO;
using System.Text;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdfPath  = "input.pdf";
        const string outputPdfPath = "output.pdf";
        const string xfdfPath      = "annotations.xfdf";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"File not found: {inputPdfPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal.
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Create the annotation editor (facade). It does NOT implement IDisposable,
            // so we do NOT wrap it in a using statement.
            PdfAnnotationEditor annotationEditor = new PdfAnnotationEditor();

            // Bind the loaded PDF to the editor.
            annotationEditor.BindPdf(pdfDoc);

            // Export all annotations to XFDF using a MemoryStream.
            using (MemoryStream xfdfStream = new MemoryStream())
            {
                // NOTE: The correct method name is ExportAnnotationsToXfdf (case‑sensitive).
                annotationEditor.ExportAnnotationsToXfdf(xfdfStream);

                // Reset stream position to read the content.
                xfdfStream.Position = 0;
                string xfdfContent = Encoding.UTF8.GetString(xfdfStream.ToArray());

                // Optionally write the XFDF to a file for inspection.
                File.WriteAllText(xfdfPath, xfdfContent, Encoding.UTF8);
                Console.WriteLine($"Annotations exported to XFDF file: {xfdfPath}");
            }

            // Save the (potentially unchanged) PDF document.
            pdfDoc.Save(outputPdfPath);
            Console.WriteLine($"PDF saved to: {outputPdfPath}");
        }
    }
}
