using System;
using System.Reflection;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

public static class PdfPageEditorHelper
{
    /// <summary>
    /// Applies rotation, page size, and zoom to a PDF file and saves the result.
    /// </summary>
    /// <param name="inputPdfPath">Path to the source PDF.</param>
    /// <param name="outputPdfPath">Path where the edited PDF will be saved.</param>
    /// <param name="rotationDegrees">
    /// Rotation angle in degrees. Valid values are 0, 90, 180, or 270.
    /// </param>
    /// <param name="pageSize">
    /// Desired page size (e.g., PageSize.A4, PageSize.Letter). Use Aspose.Pdf.PageSize.
    /// </param>
    /// <param name="zoomFactor">
    /// Zoom coefficient where 1.0 = 100%. Values greater than 1 enlarge, less than 1 shrink.
    /// </param>
    public static void ApplyPageEdits(
        string inputPdfPath,
        string outputPdfPath,
        int rotationDegrees,
        PageSize pageSize,
        double zoomFactor)
    {
        // Validate input arguments (optional but helpful)
        if (string.IsNullOrWhiteSpace(inputPdfPath))
            throw new ArgumentException("Input PDF path must be provided.", nameof(inputPdfPath));
        if (string.IsNullOrWhiteSpace(outputPdfPath))
            throw new ArgumentException("Output PDF path must be provided.", nameof(outputPdfPath));
        if (rotationDegrees != 0 && rotationDegrees != 90 && rotationDegrees != 180 && rotationDegrees != 270)
            throw new ArgumentException("Rotation must be 0, 90, 180, or 270 degrees.", nameof(rotationDegrees));
        if (zoomFactor <= 0)
            throw new ArgumentException("Zoom factor must be greater than zero.", nameof(zoomFactor));

        // Use PdfPageEditor facade to edit the document.
        // The facade implements IDisposable, so wrap it in a using block for deterministic disposal.
        using (PdfPageEditor editor = new PdfPageEditor())
        {
            // Bind the source PDF file.
            editor.BindPdf(inputPdfPath);

            // Set desired rotation (must be 0, 90, 180, or 270).
            editor.Rotation = rotationDegrees;

            // Set the output page size.
            editor.PageSize = pageSize;

            // Set zoom factor (property expects a float).
            editor.Zoom = (float)zoomFactor;

            // Apply all configured changes to the document.
            editor.ApplyChanges();

            // Save the edited PDF to the specified output path.
            editor.Save(outputPdfPath);
        }
    }
}

public class Program
{
    /// <summary>
    /// Entry point required for a console‑application build. Demonstrates a simple call to the helper.
    /// </summary>
    public static void Main(string[] args)
    {
        // Expected arguments: inputPath outputPath rotation pageSize zoomFactor
        // Example: "input.pdf" "output.pdf" 90 "A4" 1.25
        if (args.Length < 5)
        {
            Console.WriteLine("Usage: <inputPdf> <outputPdf> <rotation> <pageSize> <zoomFactor>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];
        if (!int.TryParse(args[2], out int rotation))
        {
            Console.WriteLine("Invalid rotation value.");
            return;
        }

        // PageSize in Aspose.Pdf is a class with static properties (A4, Letter, etc.), not an enum.
        // Therefore we cannot use Enum.TryParse. Instead we resolve the property via reflection.
        PageSize size = ParsePageSize(args[3]);
        if (size == null)
        {
            Console.WriteLine($"Invalid page size '{args[3]}'. Falling back to A4.");
            size = PageSize.A4;
        }

        if (!double.TryParse(args[4], out double zoom))
        {
            Console.WriteLine("Invalid zoom factor.");
            return;
        }

        try
        {
            PdfPageEditorHelper.ApplyPageEdits(inputPath, outputPath, rotation, size, zoom);
            Console.WriteLine($"PDF edited successfully and saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }

    /// <summary>
    /// Parses a string into an Aspose.Pdf.PageSize instance.
    /// The method looks for a public static property on the PageSize class that matches the name (case‑insensitive).
    /// Returns null if no matching property is found.
    /// </summary>
    private static PageSize ParsePageSize(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            return null;

        // Aspose.Pdf.PageSize defines static properties like A4, Letter, Legal, etc.
        var prop = typeof(PageSize).GetProperty(name, BindingFlags.Public | BindingFlags.Static | BindingFlags.IgnoreCase);
        if (prop != null && typeof(PageSize).IsAssignableFrom(prop.PropertyType))
        {
            return (PageSize)prop.GetValue(null);
        }
        return null;
    }
}
