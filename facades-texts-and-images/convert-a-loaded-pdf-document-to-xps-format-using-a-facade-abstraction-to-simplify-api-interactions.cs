using System;
using System.IO;
using Aspose.Pdf.Facades; // Facade classes (PdfViewer, etc.)

class Program
{
    static void Main()
    {
        // Input PDF file path
        const string pdfPath = "input.pdf";
        // Desired output XPS file path
        const string xpsPath = "output.xps";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"Error: PDF file not found – {pdfPath}");
            return;
        }

        try
        {
            // PdfViewer is a facade that simplifies PDF operations (load, render, save, etc.)
            using (PdfViewer viewer = new PdfViewer())
            {
                // Load the PDF document into the facade
                viewer.BindPdf(pdfPath);

                // Save the loaded PDF as XPS. The format is inferred from the file extension.
                viewer.Save(xpsPath);
            }

            Console.WriteLine($"PDF successfully converted to XPS: '{xpsPath}'");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Conversion failed: {ex.Message}");
        }
    }
}
