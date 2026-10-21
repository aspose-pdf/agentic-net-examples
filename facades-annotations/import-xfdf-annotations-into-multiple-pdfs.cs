using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Annotations; // Needed for AnnotationType

class Program
{
    static void Main()
    {
        // Path to the XFDF file that contains the reviewer comments.
        const string xfdfPath = "reviewer_notes.xfdf";

        // Input PDF files that should receive the same comments.
        string[] inputPdfs = {
            "Document1.pdf",
            "Document2.pdf",
            "Document3.pdf"
        };

        // Directory where the annotated PDFs will be saved.
        const string outputDir = "AnnotatedPdfs";

        // Validate XFDF file existence.
        if (!File.Exists(xfdfPath))
        {
            Console.Error.WriteLine($"XFDF file not found: {xfdfPath}");
            return;
        }

        // Ensure the output directory exists.
        Directory.CreateDirectory(outputDir);

        // Create a single PdfAnnotationEditor instance (it does not implement IDisposable).
        PdfAnnotationEditor annotationEditor = new PdfAnnotationEditor();

        foreach (string inputPath in inputPdfs)
        {
            if (!File.Exists(inputPath))
            {
                Console.Error.WriteLine($"Input PDF not found: {inputPath}");
                continue;
            }

            // Load the PDF inside a using block for deterministic disposal.
            using (Document pdfDoc = new Document(inputPath))
            {
                // Import all annotations from the XFDF file into the current PDF.
                // The ImportAnnotations overload in Aspose.Pdf.Facades expects an array of XFDF file paths
                // and an optional array of AnnotationType to filter. Passing null for the second argument
                // imports all annotation types.
                annotationEditor.ImportAnnotations(new string[] { xfdfPath }, null);

                // Build the output file path (same file name, different folder).
                string outputPath = Path.Combine(outputDir, Path.GetFileName(inputPath));

                // Save the modified PDF.
                pdfDoc.Save(outputPath);
                Console.WriteLine($"Annotated PDF saved: {outputPath}");
            }
        }
    }
}
