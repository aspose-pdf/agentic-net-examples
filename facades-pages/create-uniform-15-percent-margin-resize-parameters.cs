using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Create resize parameters with uniform 15% margins on all sides.
        // The static MarginsPercent method sets left, right, top, and bottom margins
        // as percentages of the original page size.
        PdfFileEditor.ContentsResizeParameters parameters =
            PdfFileEditor.ContentsResizeParameters.MarginsPercent(
                left:   15,   // 15% left margin
                right:  15,   // 15% right margin
                top:    15,   // 15% top margin
                bottom: 15    // 15% bottom margin
            );

        // The 'parameters' instance can now be passed to PdfFileEditor methods
        // such as ResizeContents, AddMarginsPct, etc., to apply the uniform scaling.
    }
}