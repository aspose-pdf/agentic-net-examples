using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputPdf = "output.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Manipulate pages using the PdfPageEditor facade
        using (PdfPageEditor pageEditor = new PdfPageEditor())
        {
            // Load the source PDF into the facade
            pageEditor.BindPdf(inputPdf);

            // Rotate every page 90 degrees clockwise
            pageEditor.Rotation = 90;

            // Change the output page size to A4 (optional)
            pageEditor.PageSize = PageSize.A4;

            // Apply the changes to the underlying document
            pageEditor.ApplyChanges();

            // Save the modified document to a new PDF file
            pageEditor.Save(outputPdf);
        }

        Console.WriteLine($"Edited PDF saved to '{outputPdf}'.");
    }
}