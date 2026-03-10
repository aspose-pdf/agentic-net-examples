using System;
using System.IO;
using Aspose.Pdf; // Core PDF API – SvgLoadOptions resides directly in this namespace

class Program
{
    static void Main()
    {
        // Paths for the source SVG and the resulting PDF
        const string svgInputPath = "input.svg";
        const string pdfOutputPath = "output.pdf";

        // Verify that the SVG file exists
        if (!File.Exists(svgInputPath))
        {
            Console.Error.WriteLine($"SVG file not found: {svgInputPath}");
            return;
        }

        try
        {
            // Load the SVG file into a Document using SvgLoadOptions.
            // SvgLoadOptions allows configuration of the conversion engine, page size, etc.
            using (Document pdfDoc = new Document(svgInputPath, new SvgLoadOptions()))
            {
                // Save the loaded content as a PDF file
                pdfDoc.Save(pdfOutputPath);
            }

            Console.WriteLine($"SVG has been successfully converted to PDF: {pdfOutputPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during conversion: {ex.Message}");
        }
    }
}
