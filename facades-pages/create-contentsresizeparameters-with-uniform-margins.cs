using System;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Create a ContentsResizeParameters instance with 5‑point margins on all sides
        var resizeParams = PdfFileEditor.ContentsResizeParameters.Margins(5, 5, 5, 5);

        // Example usage (uncomment when a real PDF file is available):
        // var editor = new PdfFileEditor();
        // editor.ResizeContents("input.pdf", "output.pdf", resizeParams);

        Console.WriteLine("ContentsResizeParameters configured with 5‑point margins.");
    }
}