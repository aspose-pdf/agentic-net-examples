using System;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Create a ContentsResizeParameters instance for uniform scaling using 15% margins
        PdfFileEditor.ContentsResizeParameters resizeParams =
            PdfFileEditor.ContentsResizeParameters.MarginsPercent(15, 15, 15, 15);

        // The resizeParams object can now be used with PdfFileEditor.ResizeContents(...)
        // Example (commented out):
        // PdfFileEditor editor = new PdfFileEditor();
        // editor.BindPdf("input.pdf");
        // editor.ResizeContents(resizeParams);
        // editor.Save("output.pdf");
    }
}
