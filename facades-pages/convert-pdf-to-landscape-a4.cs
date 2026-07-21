using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputPdf = "output_landscape.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Initialize the PdfPageEditor facade
        PdfPageEditor pageEditor = new PdfPageEditor();

        // Bind the source PDF file
        pageEditor.BindPdf(inputPdf);

        // Set the output page size to A4 and switch to landscape orientation
        pageEditor.PageSize = PageSize.A4;          // A4 portrait dimensions
        pageEditor.PageSize.IsLandscape = true;    // Change orientation to landscape

        // Apply the changes to the document
        pageEditor.ApplyChanges();

        // Save the modified PDF
        pageEditor.Save(outputPdf);

        // Release resources
        pageEditor.Close();

        Console.WriteLine($"Landscape PDF saved to '{outputPdf}'.");
    }
}