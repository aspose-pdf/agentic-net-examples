using System;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Define a 15% margin. Aspose.Pdf.ResizeContents expects the margin value as a double representing a percentage.
        double fifteenPercent = 15.0;

        // Use the PdfFileEditor overload that accepts individual margin values (left, right, top, bottom).
        // This avoids the need for the non‑existent ContentsResizeParameters and Length classes in the current library version.
        PdfFileEditor editor = new PdfFileEditor();
        // Uncomment and provide real file paths when you are ready to run the operation.
        // editor.ResizeContents("input.pdf", "output.pdf", fifteenPercent, fifteenPercent, fifteenPercent, fifteenPercent);
    }
}
