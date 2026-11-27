using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Devices;
using Aspose.Pdf.Text; // for SimpleFontSubstitution

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputDir = "JpegPages";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"File not found: {inputPdfPath}");
            return;
        }

        Directory.CreateDirectory(outputDir);

        try
        {
            // Load the PDF document
            using (Document pdfDoc = new Document(inputPdfPath))
            {
                // Register font substitutions (original font name -> substitute font name)
                FontRepository.Substitutions.Add(new SimpleFontSubstitution("Times New Roman", "Arial"));
                FontRepository.Substitutions.Add(new SimpleFontSubstitution("Helvetica", "Arial"));

                // PdfConverter implements IDisposable – wrap in using for deterministic disposal
                using (PdfConverter converter = new PdfConverter())
                {
                    // Bind the already‑loaded document (with font substitutions applied)
                    converter.BindPdf(pdfDoc);

                    // Set the page range and resolution (Resolution is an Aspose.Pdf.Devices.Resolution object)
                    converter.StartPage = 3;
                    converter.EndPage = 6;
                    converter.Resolution = new Resolution(72);

                    // Perform the conversion – this prepares the internal image iterator
                    converter.DoConvert();

                    int currentPage = converter.StartPage;
                    while (converter.HasNextImage())
                    {
                        string outPath = Path.Combine(outputDir, $"page_{currentPage}.jpg");
                        // GetNextImage saves the image; the format is inferred from the file extension
                        converter.GetNextImage(outPath);
                        Console.WriteLine($"Saved page {currentPage} as JPEG to '{outPath}'.");
                        currentPage++;
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
