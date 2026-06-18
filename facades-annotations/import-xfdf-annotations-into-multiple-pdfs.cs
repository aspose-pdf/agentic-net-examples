using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Path to the XFDF file containing reviewer comments
        const string xfdfPath = "reviewer_comments.xfdf";

        // Input PDF files to which the comments will be added
        string[] inputPdfs = {
            "Document1.pdf",
            "Document2.pdf",
            "Document3.pdf"
        };

        // Output directory for the annotated PDFs
        const string outputDir = "AnnotatedOutputs";

        if (!File.Exists(xfdfPath))
        {
            Console.Error.WriteLine($"XFDF file not found: {xfdfPath}");
            return;
        }

        Directory.CreateDirectory(outputDir);

        foreach (string inputPdf in inputPdfs)
        {
            if (!File.Exists(inputPdf))
            {
                Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
                continue;
            }

            string outputPdf = Path.Combine(outputDir, Path.GetFileNameWithoutExtension(inputPdf) + "_annotated.pdf");

            try
            {
                // Bind the PDF, import annotations from the XFDF, and save the result
                using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
                {
                    editor.BindPdf(inputPdf);
                    editor.ImportAnnotationsFromXfdf(xfdfPath);
                    editor.Save(outputPdf);
                }

                Console.WriteLine($"Annotated PDF saved: {outputPdf}");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing '{inputPdf}': {ex.Message}");
            }
        }
    }
}