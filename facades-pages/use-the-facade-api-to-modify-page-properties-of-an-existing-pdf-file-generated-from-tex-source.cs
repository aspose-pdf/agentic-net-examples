using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input_from_tex.pdf";
        const string outputPdf = "modified.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Bind the existing PDF to the PdfPageEditor facade
        using (Aspose.Pdf.Facades.PdfPageEditor editor = new Aspose.Pdf.Facades.PdfPageEditor())
        {
            editor.BindPdf(inputPdf);

            // Rotate all pages 90 degrees
            editor.Rotation = 90;

            // Set zoom to 80%
            editor.Zoom = 0.8f;

            // Move the origin by (10, 20) points
            editor.MovePosition(10f, 20f);

            // Change page size to A4
            editor.PageSize = Aspose.Pdf.PageSize.A4;

            // Apply the changes and save the result
            editor.ApplyChanges();
            editor.Save(outputPdf);
        }

        Console.WriteLine($"Modified PDF saved to '{outputPdf}'.");
    }
}