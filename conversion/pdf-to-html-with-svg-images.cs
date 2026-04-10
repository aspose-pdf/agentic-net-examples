using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Input PDF file path (relative to the executable folder)
        const string pdfPath = "input.pdf";
        // Output HTML file path
        const string htmlPath = "output.html";
        // Folder where generated SVG images will be saved
        const string svgFolder = "SvgImages";

        // Ensure the SVG folder exists
        Directory.CreateDirectory(svgFolder);

        // Ensure a valid PDF exists – create a sample one if necessary.
        if (!File.Exists(pdfPath) || new FileInfo(pdfPath).Length == 0)
        {
            CreateOrWriteSamplePdf(pdfPath);
            Console.WriteLine($"Sample PDF ensured at '{pdfPath}'.");
        }

        // Load the PDF document
        using (Document pdfDocument = new Document(pdfPath))
        {
            // Initialize HTML save options
            HtmlSaveOptions htmlOptions = new HtmlSaveOptions
            {
                // Specify the folder for SVG images (Aspose.Pdf will place each image as an SVG file here)
                SpecialFolderForSvgImages = svgFolder
            };

            // Save the PDF as HTML; SVG images will be written to the specified folder.
            // Guard the Save call because on non‑Windows platforms Aspose.Pdf may require libgdiplus (GDI+).
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                pdfDocument.Save(htmlPath, htmlOptions);
            }
            else
            {
                try
                {
                    pdfDocument.Save(htmlPath, htmlOptions);
                }
                catch (TypeInitializationException ex) when (ContainsDllNotFound(ex))
                {
                    Console.WriteLine("Warning: GDI+ (libgdiplus) is not available on this platform. HTML conversion was skipped.");
                }
            }
        }

        Console.WriteLine($"PDF conversion attempt finished. SVG images (if any) are in '{svgFolder}'.");
    }

    /// <summary>
    /// Creates a very small PDF containing a single page with some text.
    /// If the environment lacks GDI+ (libgdiplus) the method falls back to writing a minimal PDF byte stream.
    /// </summary>
    /// <param name="outputPath">The file path where the PDF will be saved.</param>
    private static void CreateOrWriteSamplePdf(string outputPath)
    {
        // First try the normal Aspose.Pdf way (works on Windows and on platforms where GDI+ is present).
        try
        {
            Document doc = new Document();
            Page page = doc.Pages.Add();
            page.Paragraphs.Add(new TextFragment("This is a sample PDF generated automatically because 'input.pdf' was not found."));
            doc.Save(outputPath);
            return; // success – exit the method.
        }
        catch (TypeInitializationException ex) when (ContainsDllNotFound(ex))
        {
            Console.WriteLine("Warning: GDI+ (libgdiplus) is not available – falling back to a raw PDF stream.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Unexpected error while creating sample PDF: {ex.Message}\nFalling back to raw PDF stream.");
        }

        // Fallback: write a minimal valid PDF (ASCII) that displays "Hello World".
        const string minimalPdf = "%PDF-1.4\n" +
                                 "1 0 obj\n<< /Type /Catalog /Pages 2 0 R >>\nendobj\n" +
                                 "2 0 obj\n<< /Type /Pages /Kids [3 0 R] /Count 1 >>\nendobj\n" +
                                 "3 0 obj\n<< /Type /Page /Parent 2 0 R /MediaBox [0 0 612 792] /Contents 4 0 R /Resources << /Font << /F1 5 0 R >> >> >>\nendobj\n" +
                                 "4 0 obj\n<< /Length 55 >>\nstream\nBT /F1 24 Tf 100 700 Td (Hello World) Tj ET\nendstream\nendobj\n" +
                                 "5 0 obj\n<< /Type /Font /Subtype /Type1 /BaseFont /Helvetica >>\nendobj\n" +
                                 "xref\n0 6\n0000000000 65535 f \n0000000010 00000 n \n0000000060 00000 n \n0000000117 00000 n \n0000000210 00000 n \n0000000300 00000 n \n" +
                                 "trailer\n<< /Size 6 /Root 1 0 R >>\nstartxref\n357\n%%EOF";
        File.WriteAllText(outputPath, minimalPdf, Encoding.ASCII);
    }

    /// <summary>
    /// Walks the exception chain to determine whether a DllNotFoundException (e.g., missing libgdiplus) is present.
    /// </summary>
    private static bool ContainsDllNotFound(Exception? ex)
    {
        while (ex != null)
        {
            if (ex is DllNotFoundException)
                return true;
            ex = ex.InnerException;
        }
        return false;
    }
}
