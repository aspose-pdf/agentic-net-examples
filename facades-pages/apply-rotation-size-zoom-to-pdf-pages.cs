using System;
using System.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

public static class PdfTransformer
{
    // Applies rotation, custom page size, and zoom (scale) to all pages of a PDF.
    // inputPath   : path to the source PDF.
    // outputPath  : path where the transformed PDF will be saved.
    // rotation    : rotation angle (None, on90, on180, on270).
    // width, height: new page dimensions in points (1 point = 1/72 inch). Use 0 to keep original size.
    // zoom        : scaling factor applied uniformly to page content (e.g., 1.0 = 100%).
    public static void Transform(string inputPath, string outputPath,
                                 Rotation rotation,
                                 double width, double height,
                                 double zoom)
    {
        if (string.IsNullOrEmpty(inputPath))
            throw new ArgumentException("Input path is required.", nameof(inputPath));
        if (string.IsNullOrEmpty(outputPath))
            throw new ArgumentException("Output path is required.", nameof(outputPath));
        if (zoom <= 0)
            throw new ArgumentOutOfRangeException(nameof(zoom), "Zoom must be greater than zero.");

        // Load the document – this gives us access to per‑page properties such as rotation and size.
        Document doc = new Document(inputPath);
        int pageCount = doc.Pages.Count;

        // Apply rotation and custom size directly on the Document pages.
        for (int i = 1; i <= pageCount; i++)
        {
            // Rotation (use Aspose.Pdf.Rotation enum). Skip if Rotation.None.
            if (rotation != Rotation.None)
                doc.Pages[i].Rotate = rotation;

            // Custom page size – only when both dimensions are positive.
            if (width > 0 && height > 0)
            {
                doc.Pages[i].PageInfo.Width = width;
                doc.Pages[i].PageInfo.Height = height;
            }
        }

        // If zoom is the default (1.0) we can simply save the modified document.
        if (Math.Abs(zoom - 1.0) < 0.0001)
        {
            doc.Save(outputPath);
            return;
        }

        // For zoom we need PdfPageEditor. Bind the *already‑modified* document via its file path.
        // PdfPageEditor works on the file, so we first save the intermediate result to a temp file.
        string tempPath = System.IO.Path.GetTempFileName();
        doc.Save(tempPath);

        var editor = new PdfPageEditor();
        editor.BindPdf(tempPath);
        // Apply zoom to all pages.
        editor.ProcessPages = Enumerable.Range(1, pageCount).ToArray();
        editor.Zoom = (float)zoom;
        editor.Save(outputPath);

        // Clean up the temporary file.
        try { System.IO.File.Delete(tempPath); } catch { /* ignore */ }
    }
}

// Minimal entry point required for a console‑type project.
public class Program
{
    public static void Main(string[] args)
    {
        // Example usage (can be removed or replaced by real arguments).
        // PdfTransformer.Transform("input.pdf", "output.pdf", Rotation.on90, 595, 842, 1.2);
    }
}
