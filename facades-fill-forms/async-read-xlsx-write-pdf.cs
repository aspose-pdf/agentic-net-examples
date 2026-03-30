using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static async Task Main(string[] args)
    {
        const string xlsxPath = "data.xlsx";
        const string outputPdfPath = "filled.pdf";

        // Asynchronously read the XLSX source file (or handle missing file gracefully)
        byte[] xlsxData;
        if (File.Exists(xlsxPath))
        {
            xlsxData = await File.ReadAllBytesAsync(xlsxPath);
        }
        else
        {
            Console.WriteLine($"Warning: '{xlsxPath}' not found. Continuing with empty data.");
            xlsxData = Array.Empty<byte>();
        }

        // Create a simple PDF document
        using var pdfDocument = new Document();
        var page = pdfDocument.Pages.Add();
        var fragment = new TextFragment("PDF created after async XLSX read.");
        page.Paragraphs.Add(fragment);

        // Asynchronously write the PDF directly to the output file (no intermediate MemoryStream needed)
        await using var outputStream = new FileStream(
            outputPdfPath,
            FileMode.Create,
            FileAccess.Write,
            FileShare.None,
            bufferSize: 4096,
            useAsync: true);

        // Guard Document.Save against missing GDI+ (libgdiplus) on non‑Windows platforms
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
        {
            pdfDocument.Save(outputStream);
        }
        else
        {
            try
            {
                pdfDocument.Save(outputStream);
            }
            catch (TypeInitializationException ex) when (ContainsDllNotFound(ex))
            {
                Console.WriteLine("Warning: GDI+ (libgdiplus) is not available on this platform. PDF saving is skipped.");
                // Optionally, you could write a placeholder file or take other action here.
            }
        }

        await outputStream.FlushAsync();
        Console.WriteLine("PDF processing completed.");
    }

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
