using System;
using System.IO;
using Aspose.Pdf.Facades;

public static class AnnotationMerger
{
    /// <summary>
    /// Merges all annotations from the specified source PDFs into the target PDF
    /// and saves the combined result.
    /// </summary>
    /// <param name="targetPdfPath">Path to the PDF that will receive the annotations.</param>
    /// <param name="sourcePdfPaths">Array of PDF file paths whose annotations will be imported.</param>
    /// <param name="outputPdfPath">Path where the merged PDF will be saved.</param>
    public static void MergeAnnotationLayers(string targetPdfPath, string[] sourcePdfPaths, string outputPdfPath)
    {
        if (string.IsNullOrWhiteSpace(targetPdfPath) ||
            sourcePdfPaths == null || sourcePdfPaths.Length == 0 ||
            string.IsNullOrWhiteSpace(outputPdfPath))
        {
            throw new ArgumentException("Invalid input parameters.");
        }

        if (!File.Exists(targetPdfPath))
            throw new FileNotFoundException($"Target PDF not found: {targetPdfPath}");

        foreach (var src in sourcePdfPaths)
        {
            if (!File.Exists(src))
                throw new FileNotFoundException($"Source PDF not found: {src}");
        }

        // Initialize the annotation editor and bind the target PDF.
        PdfAnnotationEditor editor = new PdfAnnotationEditor();
        editor.BindPdf(targetPdfPath);

        // Import all annotations from each source PDF.
        // Using the overload that accepts an array of file paths imports all annotation types.
        editor.ImportAnnotations(sourcePdfPaths);

        // Save the merged document.
        editor.Save(outputPdfPath);

        // Release resources held by the editor.
        editor.Close();
    }
}

public class Program
{
    public static void Main(string[] args)
    {
        // Optional demonstration of how to call the merger.
        // Uncomment and adjust the paths to use.
        //
        // string target = "target.pdf";
        // string[] sources = { "source1.pdf", "source2.pdf" };
        // string output = "merged.pdf";
        // AnnotationMerger.MergeAnnotationLayers(target, sources, output);
    }
}
