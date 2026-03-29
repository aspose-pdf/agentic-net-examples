using System;
using System.IO;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        string inputPdf = "input.pdf";
        string outputPdf = "output.pdf";
        string appearancePdf = "appearance.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine("Input PDF not found: " + inputPdf);
            return;
        }

        if (!File.Exists(appearancePdf))
        {
            Console.Error.WriteLine("Appearance PDF not found: " + appearancePdf);
            return;
        }

        using (PdfContentEditor editor = new PdfContentEditor())
        {
            editor.BindPdf(inputPdf);

            using (FileStream appearanceStream = File.OpenRead(appearancePdf))
            {
                System.Drawing.Rectangle annotRect = new System.Drawing.Rectangle(100, 500, 200, 100);
                editor.CreateRubberStamp(1, annotRect, "Custom Stamp", System.Drawing.Color.Blue, appearanceStream);
            }

            editor.Save(outputPdf);
        }

        Console.WriteLine("Custom appearance rubber‑stamp annotation added to " + outputPdf);
    }
}