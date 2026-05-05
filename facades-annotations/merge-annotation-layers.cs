using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

public static class AnnotationMerger
{
    /// <summary>
    /// Merges annotation layers from multiple source PDFs into a single target PDF.
    /// The target PDF (basePdfPath) is used as the base document; all annotations
    /// from the source PDFs (annotationPdfPaths) are imported and saved to outputPath.
    /// </summary>
    /// <param name="basePdfPath">Path to the PDF that will receive the merged annotations.</param>
    /// <param name="annotationPdfPaths">Array of PDF file paths whose annotations will be merged.</param>
    /// <param name="outputPath">Path where the resulting PDF with combined annotations will be saved.</param>
    public static void MergeAnnotationLayers(string basePdfPath, string[] annotationPdfPaths, string outputPath)
    {
        // Validate input parameters
        if (string.IsNullOrEmpty(basePdfPath))
            throw new ArgumentException("Base PDF path must be provided.", nameof(basePdfPath));
        if (annotationPdfPaths == null || annotationPdfPaths.Length == 0)
            throw new ArgumentException("At least one source PDF path must be provided.", nameof(annotationPdfPaths));
        if (string.IsNullOrEmpty(outputPath))
            throw new ArgumentException("Output PDF path must be provided.", nameof(outputPath));

        // Ensure the base PDF exists
        if (!File.Exists(basePdfPath))
            throw new FileNotFoundException($"Base PDF not found: {basePdfPath}");

        // Ensure all source PDFs exist
        foreach (var src in annotationPdfPaths)
        {
            if (!File.Exists(src))
                throw new FileNotFoundException($"Source PDF not found: {src}");
        }

        // Use PdfAnnotationEditor to import annotations.
        // The editor does not implement IDisposable, so we manage it manually.
        PdfAnnotationEditor editor = new PdfAnnotationEditor();

        // Bind the base PDF document to the editor.
        editor.BindPdf(basePdfPath);

        // Import all annotations from the source PDFs.
        // This overload imports all annotation types.
        editor.ImportAnnotations(annotationPdfPaths);

        // Save the merged document to the specified output path.
        editor.Save(outputPath);
    }
}

public class Program
{
    /// <summary>
    /// Entry point required for a console‑type project. The method is intentionally minimal;
    /// it can be extended to parse command‑line arguments or to invoke the merger.
    /// </summary>
    public static void Main(string[] args)
    {
        // Example placeholder – no operation performed.
        // Uncomment and adapt the following lines for a real run:
        // if (args.Length >= 3)
        // {
        //     string basePdf = args[0];
        //     string[] sources = args[1].Split(';'); // semi‑colon separated list
        //     string output = args[2];
        //     AnnotationMerger.MergeAnnotationLayers(basePdf, sources, output);
        // }
    }
}