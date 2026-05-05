using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string pdfPath   = "input.pdf";      // source PDF file
        const string xfdfPath  = "annotations.xfdf"; // XFDF data file
        const string outputPath = "output.pdf";    // result PDF with imported annotations

        // Validate input files
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF file not found: {pdfPath}");
            return;
        }
        if (!File.Exists(xfdfPath))
        {
            Console.Error.WriteLine($"XFDF file not found: {xfdfPath}");
            return;
        }

        try
        {
            // Open the PDF file as a stream (no temporary files)
            using (FileStream pdfStream = File.OpenRead(pdfPath))
            // Open the XFDF data as a stream
            using (FileStream xfdfStream = File.OpenRead(xfdfPath))
            // Initialize the annotation editor facade
            using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
            {
                // Bind the PDF stream to the editor
                editor.BindPdf(pdfStream);

                // Import all annotations from the XFDF stream
                editor.ImportAnnotationsFromXfdf(xfdfStream);

                // Save the modified PDF to the output file
                editor.Save(outputPath);
            }

            Console.WriteLine($"Annotations imported successfully. Output saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}