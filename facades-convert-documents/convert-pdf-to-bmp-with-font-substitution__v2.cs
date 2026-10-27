using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices;
using Aspose.Pdf.Text;

class PdfToBmpConverter
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputFolder = "BmpPages";

        // Verify input file exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Ensure output directory exists
        Directory.CreateDirectory(outputFolder);

        // Load PDF inside a using block for deterministic disposal
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Apply custom font substitution: Times New Roman → Calibri
            // In recent Aspose.Pdf versions use FontRepository.Substitutions.Add with SimpleFontSubstitution
            FontRepository.Substitutions.Add(new SimpleFontSubstitution("Times New Roman", "Calibri"));

            // Iterate pages using 1‑based indexing (Aspose.Pdf requirement)
            for (int pageNumber = 1; pageNumber <= pdfDoc.Pages.Count; pageNumber++)
            {
                // Create a BMP device with desired resolution (e.g., 150 DPI)
                BmpDevice bmpDevice = new BmpDevice(new Resolution(150));

                string bmpPath = Path.Combine(outputFolder, $"page_{pageNumber}.bmp");
                using (FileStream fs = new FileStream(bmpPath, FileMode.Create, FileAccess.Write))
                {
                    // Convert the specific page to BMP and write to the file stream
                    bmpDevice.Process(pdfDoc.Pages[pageNumber], fs);
                }
                Console.WriteLine($"Saved page {pageNumber} as BMP to '{bmpPath}'.");
            }
        }

        Console.WriteLine("PDF to BMP conversion completed.");
    }
}
