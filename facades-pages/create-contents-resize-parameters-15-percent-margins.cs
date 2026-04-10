using System;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Create resize parameters with uniform 15% margins on all sides.
        // Margins are expressed as percentages of the original page size.
        PdfFileEditor.ContentsResizeParameters parameters =
            PdfFileEditor.ContentsResizeParameters.MarginsPercent(15, 15, 15, 15);

        // Example usage (optional):
        // PdfFileEditor editor = new PdfFileEditor();
        // editor.ResizeContents("input.pdf", "output.pdf", null, parameters);
    }
}