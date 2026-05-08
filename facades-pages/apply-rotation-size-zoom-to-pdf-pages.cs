using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

public static class PdfPageEditorHelper
{
    /// <summary>
    /// Applies rotation, page size, and zoom to a PDF file and saves the result.
    /// </summary>
    /// <param name="inputPath">Full path to the source PDF.</param>
    /// <param name="outputPath">Full path where the edited PDF will be saved.</param>
    /// <param name="rotationDegrees">Rotation angle (must be 0, 90, 180, or 270).</param>
    /// <param name="pageWidth">Desired page width (points; 1 inch = 72 points).</param>
    /// <param name="pageHeight">Desired page height (points).</param>
    /// <param name="zoomFactor">Zoom coefficient (1.0 = 100%).</param>
    public static void ApplyPageEdits(
        string inputPath,
        string outputPath,
        int rotationDegrees,
        double pageWidth,
        double pageHeight,
        double zoomFactor)
    {
        // Validate input file existence.
        if (!File.Exists(inputPath))
            throw new FileNotFoundException($"Input PDF not found: {inputPath}");

        // Create the PdfPageEditor facade.
        PdfPageEditor editor = new PdfPageEditor();

        // Bind the source PDF file.
        editor.BindPdf(inputPath);

        // Set rotation (allowed values: 0, 90, 180, 270).
        editor.Rotation = rotationDegrees;

        // Set the target page size. PageSize constructor expects float values.
        editor.PageSize = new PageSize((float)pageWidth, (float)pageHeight);

        // Set zoom factor.
        editor.Zoom = (float)zoomFactor;

        // Apply the configured changes.
        editor.ApplyChanges();

        // Save the edited PDF to the specified output path.
        editor.Save(outputPath);

        // Release resources.
        editor.Close();
    }
}

public class Program
{
    /// <summary>
    /// Simple entry point to demonstrate the helper. Adjust or remove for production use.
    /// </summary>
    public static void Main(string[] args)
    {
        // Expected arguments: inputPath outputPath rotationDegrees pageWidth pageHeight zoomFactor
        if (args.Length != 6)
        {
            Console.WriteLine("Usage: <inputPath> <outputPath> <rotationDegrees> <pageWidth> <pageHeight> <zoomFactor>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];
        int rotationDegrees = int.Parse(args[2]);
        double pageWidth = double.Parse(args[3]);
        double pageHeight = double.Parse(args[4]);
        double zoomFactor = double.Parse(args[5]);

        try
        {
            PdfPageEditorHelper.ApplyPageEdits(inputPath, outputPath, rotationDegrees, pageWidth, pageHeight, zoomFactor);
            Console.WriteLine("PDF edited and saved successfully.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }
}