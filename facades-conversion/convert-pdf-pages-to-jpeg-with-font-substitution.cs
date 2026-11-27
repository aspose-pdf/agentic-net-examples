using System;
using System.IO;
using System.Drawing.Imaging;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Devices;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string pdfPath   = "input.pdf";
        const string outputDir = "Images";

        // Ensure the output directory exists
        Directory.CreateDirectory(outputDir);

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF file not found: {pdfPath}");
            return;
        }

        try
        {
            // Register a font substitution (replace missing Times New Roman)
            // The first argument is the missing font name, the second is the substitute font name.
            FontRepository.Substitutions.Add(new SimpleFontSubstitution("TimesNewRomanPSMT", "Times New Roman"));

            using (PdfConverter converter = new PdfConverter())
            {
                // Bind the PDF document
                converter.BindPdf(pdfPath);

                // Configure conversion settings
                converter.StartPage   = 1;                     // first page to convert
                converter.EndPage     = 3;                     // last page to convert
                converter.Resolution  = new Resolution(300);   // 300 DPI
                // (optional) you can set the coordinate type if needed
                // converter.CoordinateType = PageCoordinateType.CropBox;

                // Prepare the conversion
                converter.DoConvert();

                // Export each page as a JPEG image
                int pageNumber = converter.StartPage;
                while (converter.HasNextImage())
                {
                    string outPath = Path.Combine(outputDir, $"Page_{pageNumber}.jpg");
                    converter.GetNextImage(outPath, ImageFormat.Jpeg);
                    Console.WriteLine($"Saved page {pageNumber} as JPEG to {outPath}");
                    pageNumber++;
                }

                // Release internal resources
                converter.Close();
            }
        }
        // Image conversion relies on GDI+ and is Windows‑only; handle platform issues gracefully
        catch (TypeInitializationException)
        {
            Console.WriteLine("Image conversion requires Windows GDI+. Skipped on this platform.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
