using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

public static class PdfTransformer
{
    /// <summary>
    /// Applies rotation, page size and zoom to a PDF file using Aspose.Pdf.Facades.PdfPageEditor.
    /// </summary>
    /// <param name="inputPath">Full path to the source PDF.</param>
    /// <param name="outputPath">Full path where the transformed PDF will be saved.</param>
    /// <param name="rotationDegrees">Rotation angle (must be 0, 90, 180 or 270).</param>
    /// <param name="newPageSize">Desired page size; pass null to keep the original size.</param>
    /// <param name="zoomFactor">Zoom coefficient (1.0 = 100%).</param>
    public static void TransformPdf(
        string inputPath,
        string outputPath,
        int rotationDegrees,
        PageSize newPageSize,               // reference type – can be null
        double zoomFactor)
    {
        if (string.IsNullOrWhiteSpace(inputPath))
            throw new ArgumentException("Input path is required.", nameof(inputPath));

        if (string.IsNullOrWhiteSpace(outputPath))
            throw new ArgumentException("Output path is required.", nameof(outputPath));

        if (!File.Exists(inputPath))
            throw new FileNotFoundException($"Source PDF not found: {inputPath}");

        // PdfPageEditor implements IDisposable, so wrap it in a using block.
        using (PdfPageEditor editor = new PdfPageEditor())
        {
            // Bind the source PDF file.
            editor.BindPdf(inputPath);

            // Set rotation (allowed values: 0, 90, 180, 270).
            editor.Rotation = rotationDegrees;

            // Set a new page size if one is provided.
            if (newPageSize != null)
                editor.PageSize = newPageSize;

            // Set zoom factor (float is required by the API).
            editor.Zoom = (float)zoomFactor;

            // Apply the configured changes to the document.
            editor.ApplyChanges();

            // Save the modified PDF to the specified output path.
            editor.Save(outputPath);
        }
    }
}

public class Program
{
    public static void Main(string[] args)
    {
        // Example usage – replace with real paths and parameters as needed.
        // PdfTransformer.TransformPdf("input.pdf", "output.pdf", 90, PageSize.A4, 1.0);
    }
}