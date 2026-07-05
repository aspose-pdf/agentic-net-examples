using System;
using System.IO;
using System.Threading.Tasks;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    // Asynchronous entry point
    static async Task Main(string[] args)
    {
        const string xlsxPath = "input.xlsx";          // Source XLSX file
        const string outputPdfPath = "filled_output.pdf"; // Final PDF file

        // ------------------------------------------------------------
        // 1. Read the XLSX source asynchronously into memory.
        // ------------------------------------------------------------
        if (!File.Exists(xlsxPath))
        {
            Console.Error.WriteLine($"Error: XLSX file not found at '{xlsxPath}'.");
            return;
        }

        // Asynchronously read the entire XLSX file into a byte array.
        byte[] xlsxBytes = await File.ReadAllBytesAsync(xlsxPath);
        Console.WriteLine($"Read XLSX file ({xlsxBytes.Length} bytes) asynchronously.");

        // ------------------------------------------------------------
        // 2. Create a PDF document (placeholder logic).
        // ------------------------------------------------------------
        // In a real scenario you would use Aspose.Cells to convert the XLSX
        // to PDF or extract data and fill an existing PDF. For the purpose of
        // this example we simply create a PDF that reports the size of the
        // XLSX file we just read.
        var pdfDocument = new Document();
        var page = pdfDocument.Pages.Add();
        var tf = new TextFragment($"XLSX file size: {xlsxBytes.Length} bytes");
        page.Paragraphs.Add(tf);

        // ------------------------------------------------------------
        // 3. Save the PDF to a MemoryStream first.
        // ------------------------------------------------------------
        await using var pdfStream = new MemoryStream();
        pdfDocument.Save(pdfStream);
        pdfStream.Position = 0; // Reset for reading.

        // ------------------------------------------------------------
        // 4. Write the filled PDF to disk asynchronously.
        // ------------------------------------------------------------
        await using var fileStream = new FileStream(
            outputPdfPath,
            FileMode.Create,
            FileAccess.Write,
            FileShare.None,
            bufferSize: 81920,
            useAsync: true);

        await pdfStream.CopyToAsync(fileStream);
        Console.WriteLine($"PDF successfully written to '{outputPdfPath}' using asynchronous I/O.");
    }
}
