using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;   // for FdfReader

class Program
{
    static void Main()
    {
        // Paths – adjust as needed
        const string htmlInputPath = "input.html";   // source HTML to be converted to PDF
        const string fdfPath       = "annotations.fdf"; // FDF file containing annotations
        const string outputPdfPath = "output.pdf";   // final PDF with imported annotations

        // Verify input files exist
        if (!File.Exists(htmlInputPath))
        {
            Console.Error.WriteLine($"HTML file not found: {htmlInputPath}");
            return;
        }
        if (!File.Exists(fdfPath))
        {
            Console.Error.WriteLine($"FDF file not found: {fdfPath}");
            return;
        }

        try
        {
            // Load the HTML document and convert it to a PDF document.
            // HtmlLoadOptions tells Aspose.Pdf that the source format is HTML.
            using (Document pdfDoc = new Document(htmlInputPath, new HtmlLoadOptions()))
            {
                // Import annotations from the FDF file into the PDF document.
                // FdfReader.ReadAnnotations reads the FDF stream and adds the annotations to the document.
                using (FileStream fdfStream = File.OpenRead(fdfPath))
                {
                    FdfReader.ReadAnnotations(fdfStream, pdfDoc);
                }

                // Save the resulting PDF (contains both the converted HTML content and the imported annotations).
                pdfDoc.Save(outputPdfPath);
            }

            Console.WriteLine($"PDF created with imported annotations: {outputPdfPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}