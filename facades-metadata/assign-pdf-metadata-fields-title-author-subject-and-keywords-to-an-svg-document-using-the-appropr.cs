using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string svgPath = "input.svg";
        const string outputPdfPath = "output.pdf";

        if (!File.Exists(svgPath))
        {
            Console.Error.WriteLine($"Input SVG not found: {svgPath}");
            return;
        }

        // Load the SVG and convert it to a PDF document
        using (Document svgDoc = new Document(svgPath, new SvgLoadOptions()))
        {
            // Save the intermediate PDF to a temporary file
            string tempPdfPath = Path.GetTempFileName();
            svgDoc.Save(tempPdfPath);

            // Open the temporary PDF with PdfFileInfo to set metadata
            using (PdfFileInfo pdfInfo = new PdfFileInfo(tempPdfPath))
            {
                pdfInfo.Title = "Sample SVG Document";
                pdfInfo.Author = "John Doe";
                pdfInfo.Subject = "Converted SVG to PDF";
                pdfInfo.Keywords = "svg, pdf, metadata";

                // Save the PDF with the updated metadata to the final output file
                pdfInfo.SaveNewInfo(outputPdfPath);
            }

            // Clean up the temporary PDF file
            File.Delete(tempPdfPath);
        }

        Console.WriteLine($"SVG converted to PDF with metadata saved as '{outputPdfPath}'.");
    }
}