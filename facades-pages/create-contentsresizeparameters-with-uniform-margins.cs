using Aspose.Pdf.Facades;

class Example
{
    static void Main()
    {
        // Create a ContentsResizeParameters instance with 5‑point margins on all sides.
        // The static Margins method accepts absolute values (default space units).
        PdfFileEditor.ContentsResizeParameters parameters =
            PdfFileEditor.ContentsResizeParameters.Margins(5, 5, 5, 5);

        // The 'parameters' object can now be used with PdfFileEditor methods
        // such as ResizeContents, AddMargins, etc.
    }
}