using System;
using System.IO;
using Aspose.Pdf.Facades; // PdfAnnotationEditor resides here

class Program
{
    static void Main()
    {
        // Path to the XFDF file that contains the reviewer comments
        const string xfdfPath = "reviewer_comments.xfdf";

        // Array of source PDF files to which the same annotations will be added
        string[] sourcePdfs = new string[]
        {
            "Document1.pdf",
            "Document2.pdf",
            "Document3.pdf"
        };

        // Output directory for the annotated PDFs
        const string outputDir = "AnnotatedOutputs";

        // Ensure the output directory exists
        Directory.CreateDirectory(outputDir);

        // Verify the XFDF file exists before processing
        if (!File.Exists(xfdfPath))
        {
            Console.Error.WriteLine($"XFDF file not found: {xfdfPath}");
            return;
        }

        // Process each PDF file
        foreach (string srcPath in sourcePdfs)
        {
            if (!File.Exists(srcPath))
            {
                Console.Error.WriteLine($"Source PDF not found: {srcPath}");
                continue;
            }

            // Determine output file path (same name, different folder)
            string outputPath = Path.Combine(outputDir, Path.GetFileName(srcPath));

            // Use PdfAnnotationEditor to bind the PDF, import XFDF annotations, and save
            using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
            {
                // Bind the source PDF file
                editor.BindPdf(srcPath);

                // Import all annotations from the XFDF file
                editor.ImportAnnotationsFromXfdf(xfdfPath);

                // Save the modified PDF to the output location
                editor.Save(outputPath);
            }

            Console.WriteLine($"Annotated PDF saved: {outputPath}");
        }
    }
}