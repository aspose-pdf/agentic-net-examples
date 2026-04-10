using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Path to the XFDF file that contains the reviewer comments
        const string xfdfPath = "reviewer_comments.xfdf";

        // Input PDF files that should receive the same annotations
        string[] inputPdfs = new string[]
        {
            "Document1.pdf",
            "Document2.pdf",
            "Document3.pdf"
        };

        // Directory where the annotated PDFs will be saved
        const string outputDirectory = "AnnotatedOutputs";

        // Ensure the output directory exists
        Directory.CreateDirectory(outputDirectory);

        // Process each PDF
        foreach (string inputPdf in inputPdfs)
        {
            // Verify the source PDF exists
            if (!File.Exists(inputPdf))
            {
                Console.Error.WriteLine($"Source PDF not found: {inputPdf}");
                continue;
            }

            // Verify the XFDF file exists (checked once per run)
            if (!File.Exists(xfdfPath))
            {
                Console.Error.WriteLine($"XFDF file not found: {xfdfPath}");
                break;
            }

            // Create a PdfAnnotationEditor facade
            PdfAnnotationEditor editor = new PdfAnnotationEditor();

            // Bind the PDF to the editor
            editor.BindPdf(inputPdf);

            // Import all annotations (comments) from the XFDF file
            editor.ImportAnnotationsFromXfdf(xfdfPath);

            // Build the output file name
            string outputPdf = Path.Combine(
                outputDirectory,
                Path.GetFileNameWithoutExtension(inputPdf) + "_annotated.pdf");

            // Save the modified PDF
            editor.Save(outputPdf);

            // Release resources held by the editor
            editor.Close();

            Console.WriteLine($"Annotated PDF saved: {outputPdf}");
        }
    }
}