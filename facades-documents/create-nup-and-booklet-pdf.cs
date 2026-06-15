using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf   = "input.pdf";          // source PDF
        const string nupPdf     = "temp_nup.pdf";       // intermediate N‑up PDF
        const string bookletPdf = "booklet_output.pdf"; // final booklet PDF

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        try
        {
            // 1. Create N‑up layout (e.g., 2 columns × 2 rows)
            PdfFileEditor editor = new PdfFileEditor();
            bool nupResult = editor.MakeNUp(inputPdf, nupPdf, 2, 2);
            if (!nupResult)
            {
                Console.Error.WriteLine("Failed to create N‑up layout.");
                return;
            }

            // 2. Convert the N‑up PDF into a booklet
            bool bookletResult = editor.MakeBooklet(nupPdf, bookletPdf);
            if (!bookletResult)
            {
                Console.Error.WriteLine("Failed to create booklet.");
                return;
            }

            Console.WriteLine($"Booklet created successfully: {bookletPdf}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
        finally
        {
            // Clean up the intermediate N‑up file
            try
            {
                if (File.Exists(nupPdf))
                    File.Delete(nupPdf);
            }
            catch { /* ignore cleanup errors */ }
        }
    }
}