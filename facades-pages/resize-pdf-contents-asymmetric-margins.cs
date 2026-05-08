using System;
using System.IO;
using System.Runtime.InteropServices;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Text; // Added for TextFragment

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        // Ensure the input file exists – create a simple PDF if it does not.
        if (!File.Exists(inputPath))
        {
            using (Document doc = new Document())
            {
                Page page = doc.Pages.Add();
                page.Paragraphs.Add(new TextFragment("Sample content for resizing"));

                // Document.Save internally uses GDI+ (libgdiplus) on non‑Windows platforms.
                // Guard the call with an OS check and provide a minimal PDF fallback when GDI+ is unavailable.
                if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                {
                    doc.Save(inputPath);
                }
                else
                {
                    // Minimal PDF content (valid PDF 1.4) as a byte array fallback.
                    // This avoids the GDI+ dependency while still giving a usable PDF for the resize operation.
                    byte[] minimalPdf = System.Text.Encoding.ASCII.GetBytes(
                        "%PDF-1.4\n" +
                        "1 0 obj << /Type /Catalog /Pages 2 0 R >> endobj\n" +
                        "2 0 obj << /Type /Pages /Count 1 /Kids [3 0 R] >> endobj\n" +
                        "3 0 obj << /Type /Page /Parent 2 0 R /MediaBox [0 0 612 792] /Contents 4 0 R >> endobj\n" +
                        "4 0 obj << /Length 44 >> stream\n" +
                        "BT /F1 24 Tf 100 700 Td (Sample content for resizing) Tj ET\n" +
                        "endstream endobj\n" +
                        "xref\n0 5\n0000000000 65535 f \n0000000010 00000 n \n0000000060 00000 n \n0000000117 00000 n \n0000000210 00000 n \ntrailer << /Size 5 /Root 1 0 R >>\nstartxref\n277\n%%EOF");
                    File.WriteAllBytes(inputPath, minimalPdf);
                }
                Console.WriteLine($"Created placeholder PDF at '{inputPath}'.");
            }
        }

        // PdfFileEditor does not implement IDisposable, so no using block is needed.
        PdfFileEditor fileEditor = new PdfFileEditor();

        // Define asymmetric margins:
        // Left 5% of page width, Right 15% of page width,
        // Top 10% of page height, Bottom 10% of page height.
        // Content width and height are set to auto (null) so they are calculated
        // from the remaining space after margins are applied.
        PdfFileEditor.ContentsResizeParameters parameters = new PdfFileEditor.ContentsResizeParameters(
            PdfFileEditor.ContentsResizeValue.Percents(5),   // left margin
            null,                                            // auto content width
            PdfFileEditor.ContentsResizeValue.Percents(15),  // right margin
            PdfFileEditor.ContentsResizeValue.Percents(10),  // top margin
            null,                                            // auto content height
            PdfFileEditor.ContentsResizeValue.Percents(10)   // bottom margin
        );

        // Resize all pages (pages = null) using the asymmetric margins.
        bool success = fileEditor.ResizeContents(inputPath, outputPath, null, parameters);

        Console.WriteLine(success
            ? $"Resizing completed. Output saved to '{outputPath}'."
            : "Resizing failed.");
    }
}
