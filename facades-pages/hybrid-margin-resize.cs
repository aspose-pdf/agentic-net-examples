using System;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Define mixed margins: left 10% (percentage), right 20 points (absolute),
        // top 5% (percentage), bottom 30 points (absolute).
        var leftMargin   = PdfFileEditor.ContentsResizeValue.Percents(10);
        var rightMargin  = PdfFileEditor.ContentsResizeValue.Units(20);
        var topMargin    = PdfFileEditor.ContentsResizeValue.Percents(5);
        var bottomMargin = PdfFileEditor.ContentsResizeValue.Units(30);
        var autoSize     = PdfFileEditor.ContentsResizeValue.Auto();

        // Create the hybrid resize parameters (mixed percentage and absolute values).
        var resizeParams = new PdfFileEditor.ContentsResizeParameters(
            leftMargin,
            autoSize,
            rightMargin,
            topMargin,
            autoSize,
            bottomMargin
        );

        // Display the configured margin values.
        Console.WriteLine("Hybrid ContentsResizeParameters configuration:");
        Console.WriteLine($"Left Margin:   {GetMarginString(resizeParams.LeftMargin)}");
        Console.WriteLine($"Right Margin:  {GetMarginString(resizeParams.RightMargin)}");
        Console.WriteLine($"Top Margin:    {GetMarginString(resizeParams.TopMargin)}");
        Console.WriteLine($"Bottom Margin: {GetMarginString(resizeParams.BottomMargin)}");
    }

    /// <summary>
    /// Returns a human‑readable representation of a ContentsResizeValue.
    /// The PercentValue property is write‑only in the Aspose API, so we cannot read it directly.
    /// For percentage based values we return a placeholder "percentage" (or you could use reflection
    /// if you really need the numeric value). For absolute values we return the numeric Value.
    /// </summary>
    private static string GetMarginString(PdfFileEditor.ContentsResizeValue value)
    {
        if (value.IsPercent)
        {
            // Percent value is not publicly readable; indicate that it is a percentage.
            return "percentage";
        }
        else
        {
            return value.Value.ToString();
        }
    }
}
