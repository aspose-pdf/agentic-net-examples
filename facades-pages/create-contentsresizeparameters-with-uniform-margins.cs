using System;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Create a ContentsResizeParameters instance with absolute margins of 5 points on all sides
        var resizeParams = PdfFileEditor.ContentsResizeParameters.Margins(5, 5, 5, 5);

        // Example usage with PdfFileEditor (optional)
        // PdfFileEditor editor = new PdfFileEditor();
        // editor.ResizeContents("input.pdf", "output.pdf", resizeParams);
    }
}