using System;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Create resize parameters with uniform 15% margins on all sides.
        // The static MarginsPercent method returns a PdfFileEditor.ContentsResizeParameters instance.
        PdfFileEditor.ContentsResizeParameters resizeParams = PdfFileEditor.ContentsResizeParameters.MarginsPercent(
            left: 15,
            right: 15,
            top: 15,
            bottom: 15);

        // Example usage (optional):
        // PdfFileEditor editor = new PdfFileEditor();
        // editor.ResizeContents("input.pdf", "output.pdf", null, resizeParams);
    }
}