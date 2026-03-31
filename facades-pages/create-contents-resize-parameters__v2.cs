using System;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Create resize parameters with uniform 15% margins on all sides
        PdfFileEditor.ContentsResizeParameters resizeParams =
            PdfFileEditor.ContentsResizeParameters.MarginsPercent(15, 15, 15, 15);

        // The object can now be used with PdfFileEditor methods such as ResizeContentsPct
        Console.WriteLine("ContentsResizeParameters created with 15% margins.");
    }
}