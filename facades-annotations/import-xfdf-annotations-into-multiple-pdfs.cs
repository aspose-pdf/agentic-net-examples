using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input XFDF file containing the reviewer comments
        const string xfdfPath = "reviewer_comments.xfdf";

        // List of PDF files to which the comments will be applied
        string[] pdfFiles = {
            "Document1.pdf",
            "Document2.pdf",
            "Document3.pdf"
        };

        // Directory where the annotated PDFs will be saved
        const string outputDir = "AnnotatedOutputs";

        // Validate inputs
        if (!File.Exists(xfdfPath))
        {
            Console.Error.WriteLine($"XFDF file not found: {xfdfPath}");
            return;
        }

        Directory.CreateDirectory(outputDir);

        foreach (string pdfPath in pdfFiles)
        {
            if (!File.Exists(pdfPath))
            {
                Console.Error.WriteLine($"PDF file not found: {pdfPath}");
                continue;
            }

            // Build output file name (original name with "_annotated" suffix)
            string outputPath = Path.Combine(
                outputDir,
                Path.GetFileNameWithoutExtension(pdfPath) + "_annotated.pdf");

            // Use PdfAnnotationEditor to bind the PDF, import XFDF annotations, and save
            using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
            {
                // Load the target PDF
                editor.BindPdf(pdfPath);

                // Import all annotations from the XFDF file
                editor.ImportAnnotationsFromXfdf(xfdfPath);

                // Save the modified PDF
                editor.Save(outputPath);
            }

            Console.WriteLine($"Annotated PDF saved: {outputPath}");
        }
    }
}