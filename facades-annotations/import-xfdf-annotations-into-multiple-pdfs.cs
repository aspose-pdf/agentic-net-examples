using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Path to the XFDF file that contains the reviewer comments.
        const string xfdfPath = "reviewer_comments.xfdf";

        // List of PDF files that should receive the same comments.
        string[] pdfFiles = {
            "Document1.pdf",
            "Document2.pdf",
            "Document3.pdf"
        };

        // Verify that the XFDF file exists.
        if (!File.Exists(xfdfPath))
        {
            Console.Error.WriteLine($"XFDF file not found: {xfdfPath}");
            return;
        }

        // Process each PDF file.
        foreach (string inputPdf in pdfFiles)
        {
            if (!File.Exists(inputPdf))
            {
                Console.Error.WriteLine($"PDF file not found: {inputPdf}");
                continue;
            }

            // Create an output file name (you can change the naming scheme as needed).
            string outputPdf = Path.Combine(
                Path.GetDirectoryName(inputPdf) ?? string.Empty,
                Path.GetFileNameWithoutExtension(inputPdf) + "_with_comments.pdf");

            // Use PdfAnnotationEditor to bind the PDF, import the XFDF annotations, and save.
            using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
            {
                // Bind the source PDF.
                editor.BindPdf(inputPdf);

                // Import all annotations (comments) from the XFDF file.
                editor.ImportAnnotationsFromXfdf(xfdfPath);

                // Save the modified PDF to a new file.
                editor.Save(outputPdf);
            }

            Console.WriteLine($"Processed '{inputPdf}' → '{outputPdf}'");
        }
    }
}