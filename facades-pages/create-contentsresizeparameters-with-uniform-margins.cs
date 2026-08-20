using System;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Create resize parameters with absolute margins of 5 points on all sides
        PdfFileEditor.ContentsResizeParameters parameters = PdfFileEditor.ContentsResizeParameters.Margins(5, 5, 5, 5);

        // The 'parameters' object can now be passed to PdfFileEditor methods (e.g., ResizeContents)
    }
}