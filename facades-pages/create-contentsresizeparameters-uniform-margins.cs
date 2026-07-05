using System;
using Aspose.Pdf.Facades; // For PdfFileEditor and its nested ContentsResizeParameters

class Program
{
    static void Main()
    {
        // Create a ContentsResizeParameters instance with absolute margins of 5 points
        // on the left, right, top, and bottom sides using integer point values.
        var parameters = PdfFileEditor.ContentsResizeParameters.Margins(5, 5, 5, 5);

        // The 'parameters' object can now be passed to PdfFileEditor methods
        // such as ResizeContents, AddMargins, etc., to apply the specified margins.
    }
}