using System;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Create a ContentsResizeParameters instance with absolute margins of 5 points
        // on the left, right, top, and bottom sides.
        var parameters = PdfFileEditor.ContentsResizeParameters.Margins(5, 5, 5, 5);

        // Example usage: the parameters can now be passed to PdfFileEditor methods
        // such as ResizeContents or AddMargins.
        Console.WriteLine($"Left margin: {parameters.LeftMargin}");
        Console.WriteLine($"Right margin: {parameters.RightMargin}");
        Console.WriteLine($"Top margin: {parameters.TopMargin}");
        Console.WriteLine($"Bottom margin: {parameters.BottomMargin}");
    }
}