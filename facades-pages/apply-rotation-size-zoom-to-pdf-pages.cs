using System;
using System.IO;
using System.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

public static class PdfTransformer
{
    /// <summary>
    /// Applies rotation, size, and zoom to all pages of a PDF and saves the result.
    /// </summary>
    /// <param name="inputPath">Path to the source PDF.</param>
    /// <param name="outputPath">Path where the transformed PDF will be saved.</param>
    /// <param name="rotationAngle">Rotation angle in degrees (90, 180, 270). Any other value means no rotation.</param>
    /// <param name="targetWidth">Desired page width (points).</param>
    /// <param name="targetHeight">Desired page height (points).</param>
    /// <param name="zoomFactor">Scaling factor applied to the page content (e.g., 1.0 = no zoom, 0.5 = 50%).</param>
    public static void ApplyTransformations(string inputPath, string outputPath,
                                            int rotationAngle,
                                            double targetWidth, double targetHeight,
                                            double zoomFactor)
    {
        // Validate input file.
        if (!File.Exists(inputPath))
            throw new FileNotFoundException($"Input file not found: {inputPath}");

        // Load the PDF document.
        using (Document doc = new Document(inputPath))
        {
            // Apply rotation and page size to each page.
            foreach (Page page in doc.Pages)
            {
                // Set rotation.
                page.Rotate = MapAngleToRotationEnum(rotationAngle);

                // Set page dimensions.
                page.PageInfo.Width = targetWidth;
                page.PageInfo.Height = targetHeight;
            }

            // Save the intermediate result to a temporary file.
            string tempPath = Path.GetTempFileName();
            doc.Save(tempPath);

            // Apply zoom using PdfPageEditor.
            using (PdfPageEditor editor = new PdfPageEditor())
            {
                editor.BindPdf(tempPath);
                // Process all pages.
                editor.ProcessPages = Enumerable.Range(1, doc.Pages.Count).ToArray();
                editor.Zoom = (float)zoomFactor;
                editor.Save(outputPath);
            }

            // Clean up temporary file.
            File.Delete(tempPath);
        }
    }

    private static Rotation MapAngleToRotationEnum(int angle)
    {
        return angle switch
        {
            90 => Rotation.on90,
            180 => Rotation.on180,
            270 => Rotation.on270,
            _ => Rotation.None,
        };
    }
}

class Program
{
    static void Main(string[] args)
    {
        // Expected arguments:
        // 0 - input PDF path
        // 1 - output PDF path
        // 2 - rotation angle (90, 180, 270)
        // 3 - target width (points)
        // 4 - target height (points)
        // 5 - zoom factor (e.g., 1.0)
        if (args.Length != 6)
        {
            Console.WriteLine("Usage: <inputPath> <outputPath> <rotationAngle> <targetWidth> <targetHeight> <zoomFactor>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];
        if (!int.TryParse(args[2], out int rotationAngle))
        {
            Console.WriteLine("Invalid rotation angle.");
            return;
        }
        if (!double.TryParse(args[3], out double targetWidth) ||
            !double.TryParse(args[4], out double targetHeight) ||
            !double.TryParse(args[5], out double zoomFactor))
        {
            Console.WriteLine("Invalid numeric parameters.");
            return;
        }

        try
        {
            PdfTransformer.ApplyTransformations(inputPath, outputPath, rotationAngle, targetWidth, targetHeight, zoomFactor);
            Console.WriteLine($"Transformation completed successfully. Output saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }
}
