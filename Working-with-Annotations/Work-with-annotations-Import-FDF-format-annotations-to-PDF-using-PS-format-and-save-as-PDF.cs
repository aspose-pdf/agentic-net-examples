using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdfPath  = "input.pdf";   // PDF to receive annotations
        const string inputFdfPath  = "annotations.fdf"; // FDF file containing annotations
        const string outputPdfPath = "output.pdf";  // Resulting PDF

        // Verify source files exist
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Error: PDF file not found – {inputPdfPath}");
            return;
        }
        if (!File.Exists(inputFdfPath))
        {
            Console.Error.WriteLine($"Error: FDF file not found – {inputFdfPath}");
            return;
        }

        try
        {
            // Load the PDF inside a using block for deterministic disposal
            using (Document pdfDoc = new Document(inputPdfPath))
            {
                // PdfAnnotationEditor works on the loaded document
                using (PdfAnnotationEditor editor = new PdfAnnotationEditor(pdfDoc))
                {
                    // Bind the editor to the document (optional when constructor receives the document)
                    editor.BindPdf(pdfDoc);

                    // Import all annotations from the FDF file
                    editor.ImportAnnotationsFromFdf(inputFdfPath);

                    // Save the modified PDF
                    editor.Save(outputPdfPath);
                }
            }

            Console.WriteLine($"Annotations imported successfully. Output saved to '{outputPdfPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}