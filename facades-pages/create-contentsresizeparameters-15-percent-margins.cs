using System;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Create a ContentsResizeParameters instance with uniform 15% margins
        // on left, right, top and bottom. The static MarginsPercent method
        // returns a fully configured object ready for use with PdfFileEditor.
        PdfFileEditor.ContentsResizeParameters resizeParams = PdfFileEditor.ContentsResizeParameters.MarginsPercent(
            left:   15,   // 15% left margin
            right:  15,   // 15% right margin
            top:    15,   // 15% top margin
            bottom: 15    // 15% bottom margin
        );

        // Example usage (optional):
        // PdfFileEditor editor = new PdfFileEditor();
        // editor.ResizeContents("input.pdf", "output.pdf", null, resizeParams);
    }
}
