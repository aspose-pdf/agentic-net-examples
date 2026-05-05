using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using System.Drawing.Imaging; // for ImageFormat

class PdfToPngOddPages
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";          // source PDF
        const string outputFolder = "OddPageImages";      // folder for PNG files

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Ensure output directory exists
        Directory.CreateDirectory(outputFolder);

        // Determine odd‑numbered pages
        int[] oddPages;
        using (Document doc = new Document(inputPdfPath))
        {
            int pageCount = doc.Pages.Count;
            int oddCount = (pageCount + 1) / 2; // number of odd pages
            oddPages = new int[oddCount];
            int idx = 0;
            for (int i = 1; i <= pageCount; i += 2)
                oddPages[idx++] = i;
        }

        // Extract odd pages to a temporary PDF
        string tempPdfPath = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString() + ".pdf");
        try
        {
            PdfFileEditor editor = new PdfFileEditor();
            editor.Extract(inputPdfPath, oddPages, tempPdfPath);

            // Convert each page of the temporary PDF to PNG
            PdfConverter converter = new PdfConverter();
            converter.BindPdf(tempPdfPath);
            converter.DoConvert();

            int imageIndex = 1; // starts at 1 for the first odd page
            while (converter.HasNextImage())
            {
                string outputFile = Path.Combine(outputFolder, $"page_{imageIndex}_odd.png");
                converter.GetNextImage(outputFile, ImageFormat.Png);
                imageIndex++;
            }

            converter.Close(); // release resources
        }
        finally
        {
            // Clean up temporary file
            if (File.Exists(tempPdfPath))
                File.Delete(tempPdfPath);
        }

        Console.WriteLine("Odd‑page PNG conversion completed.");
    }
}