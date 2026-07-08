using System;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Create a ContentsResizeParameters instance with uniform 15% margins
        // on left, right, top and bottom. The static factory method
        // MarginsPercent returns a ready‑to‑use object.
        PdfFileEditor.ContentsResizeParameters parameters =
            PdfFileEditor.ContentsResizeParameters.MarginsPercent(
                left:   15,   // 15% left margin
                right:  15,   // 15% right margin
                top:    15,   // 15% top margin
                bottom: 15    // 15% bottom margin
            );

        // The 'parameters' object can now be passed to PdfFileEditor methods,
        // e.g., ResizeContents or AddMarginsPct, to apply the uniform scaling.
        // Example (commented out):
        // PdfFileEditor editor = new PdfFileEditor();
        // editor.ResizeContents("input.pdf", "output.pdf", null, parameters);
    }
}