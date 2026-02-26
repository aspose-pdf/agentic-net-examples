using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";          // source PDF
        const string outputPdf = "output_with_figure.pdf"; // result PDF
        const string imagePath = "figure.png";         // image to embed

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }

        if (!File.Exists(imagePath))
        {
            Console.Error.WriteLine($"Image file not found: {imagePath}");
            return;
        }

        try
        {
            // Load the PDF document
            using (Document doc = new Document(inputPdf))
            {
                // PdfFileMend works on an existing Document instance.
                // It provides AddImage overloads that accept a file path,
                // page number and four float coordinates (llx, lly, urx, ury).
                PdfFileMend mend = new PdfFileMend(doc);

                // Define the rectangle where the image will be placed.
                // Coordinates are in points (1/72 inch) and are relative to the
                // lower‑left corner of the page.
                float llx = 100f; // lower‑left X
                float lly = 500f; // lower‑left Y
                float urx = 300f; // upper‑right X
                float ury = 700f; // upper‑right Y

                // Add the image to page 1.
                mend.AddImage(imagePath, 1, llx, lly, urx, ury);

                // Save the modified document.
                doc.Save(outputPdf);
            }

            Console.WriteLine($"Figure annotation added successfully: {outputPdf}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}