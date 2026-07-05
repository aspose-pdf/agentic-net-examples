using System;
using Aspose.Pdf.Facades;
using Aspose.Pdf; // for PageSize enum

public static class PdfEditor
{
    /// <summary>
    /// Applies rotation, page size, and zoom to a PDF file and saves the result.
    /// </summary>
    /// <param name="inputPath">Full path to the source PDF.</param>
    /// <param name="outputPath">Full path where the edited PDF will be saved.</param>
    /// <param name="rotationDegrees">
    /// Rotation angle in degrees. Valid values are 0, 90, 180, or 270.
    /// </param>
    /// <param name="pageSize">
    /// Desired page size (e.g., PageSize.A4, PageSize.Letter). Use Aspose.Pdf.PageSize enum.
    /// </param>
    /// <param name="zoomFactor">
    /// Zoom coefficient where 1.0 = 100%. Values greater than 1 enlarge, less than 1 shrink.
    /// </param>
    public static void ApplyEdits(string inputPath, string outputPath,
                                 int rotationDegrees, PageSize pageSize, double zoomFactor)
    {
        // Validate input arguments
        if (string.IsNullOrWhiteSpace(inputPath))
            throw new ArgumentException("Input path must be provided.", nameof(inputPath));
        if (string.IsNullOrWhiteSpace(outputPath))
            throw new ArgumentException("Output path must be provided.", nameof(outputPath));
        if (rotationDegrees != 0 && rotationDegrees != 90 && rotationDegrees != 180 && rotationDegrees != 270)
            throw new ArgumentException("Rotation must be 0, 90, 180, or 270 degrees.", nameof(rotationDegrees));
        if (zoomFactor <= 0)
            throw new ArgumentException("Zoom factor must be greater than zero.", nameof(zoomFactor));

        // Use PdfPageEditor facade to edit the PDF.
        // The facade implements IDisposable, so wrap it in a using block for deterministic cleanup.
        using (PdfPageEditor editor = new PdfPageEditor())
        {
            // Bind the source PDF file.
            editor.BindPdf(inputPath);

            // Set desired rotation for all pages (or you can set PageRotations hashtable for per‑page control).
            editor.Rotation = rotationDegrees;

            // Set the output page size.
            editor.PageSize = pageSize;

            // Set zoom coefficient (1.0 = 100%). PdfPageEditor.Zoom expects an integer percentage.
            editor.Zoom = (int)(zoomFactor * 100);

            // Apply the configured changes to the document.
            editor.ApplyChanges();

            // Save the edited PDF to the specified output path.
            editor.Save(outputPath);
        }
    }
}

// Dummy entry point to satisfy the compiler when the project is built as an executable.
public class Program
{
    public static void Main(string[] args)
    {
        // The Main method is optional for library usage. It is provided only to satisfy the
        // compiler's requirement for an entry point when the project type is a console app.
        // Example usage (commented out to avoid runtime errors if paths are not valid):
        // PdfEditor.ApplyEdits("input.pdf", "output.pdf", 90, PageSize.A4, 1.0);
    }
}
