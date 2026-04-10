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
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Bind the PDF and chain page property modifications
        using (PdfPageEditor editor = new PdfPageEditor())
        {
            // Load the source PDF
            editor.BindPdf(inputPdf);

            // Specify which pages to edit (optional; comment out to edit all pages)
            editor.ProcessPages = new int[] { 1, 2, 3 };

            // Rotate pages by 90 degrees
            editor.Rotation = 90;

            // Set a new page size (e.g., A4: 595 x 842 points)
            editor.PageSize = new PageSize(595, 842);

            // Apply a zoom factor (0.75 = 75%)
            editor.Zoom = 0.75f;

            // Apply all changes to the document
            editor.ApplyChanges();

            // Save the modified PDF
            editor.Save(outputPdf);
        }

        Console.WriteLine($"Edited PDF saved to '{outputPdf}'.");
    }
}