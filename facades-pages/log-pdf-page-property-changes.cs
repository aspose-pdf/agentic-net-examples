using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputPdf = "output.pdf";
        const string logFile   = "modifications.log";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Initialize the facade and bind the source PDF
        PdfPageEditor editor = new PdfPageEditor();
        editor.BindPdf(inputPdf);

        // Capture original property values
        float originalZoom      = editor.Zoom;
        int   originalRotation  = editor.Rotation;
        PageSize originalSize   = editor.PageSize;

        // Apply modifications
        editor.Zoom = 0.5f;                     // 50% zoom
        editor.Rotation = 90;                  // rotate 90 degrees
        editor.PageSize = new PageSize(595, 842); // A4 size in points

        // Log the changes
        using (StreamWriter log = new StreamWriter(logFile, true))
        {
            log.WriteLine($"[{DateTime.Now}] Modifications applied to '{inputPdf}':");
            if (Math.Abs(originalZoom - editor.Zoom) > 0.0001f)
                log.WriteLine($"  Zoom changed from {originalZoom} to {editor.Zoom}");
            if (originalRotation != editor.Rotation)
                log.WriteLine($"  Rotation changed from {originalRotation}° to {editor.Rotation}°");
            if (originalSize.Width != editor.PageSize.Width || originalSize.Height != editor.PageSize.Height)
                log.WriteLine($"  PageSize changed from {originalSize.Width}x{originalSize.Height} to {editor.PageSize.Width}x{editor.PageSize.Height}");
            log.WriteLine(); // blank line separator
        }

        // Save the modified document
        editor.Save(outputPdf);
        editor.Close();

        Console.WriteLine($"Modifications saved to '{outputPdf}'. Log written to '{logFile}'.");
    }
}