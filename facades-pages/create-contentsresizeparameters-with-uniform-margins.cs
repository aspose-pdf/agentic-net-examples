using Aspose.Pdf.Facades;

class Example
{
    static void Main()
    {
        // Create a ContentsResizeParameters instance with absolute margins of 5 points
        // on the left, right, top, and bottom sides.
        PdfFileEditor.ContentsResizeParameters parameters =
            PdfFileEditor.ContentsResizeParameters.Margins(5, 5, 5, 5);

        // The 'parameters' object can now be passed to PdfFileEditor methods such as
        // ResizeContents or AddMargins to apply the specified margins.
    }
}