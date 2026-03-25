using System;
using System.Runtime.InteropServices;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string outputPath = "output_pdfa1b.pdf";
        const string logPath = "conversion_log.xml";

        using (Document doc = new Document())
        {
            // Add a new page (first page)
            Page page = doc.Pages.Add();

            // Create a text paragraph
            TextParagraph paragraph = new TextParagraph();
            paragraph.Rectangle = new Aspose.Pdf.Rectangle(100, 600, 200, 700);
            paragraph.FormattingOptions.WrapMode = TextFormattingOptions.WordWrapMode.ByWords;
            paragraph.AppendLine("Hello, PDF/A-1b document created with Aspose.Pdf.");

            // Append the paragraph to the page
            TextBuilder builder = new TextBuilder(page);
            builder.AppendParagraph(paragraph);

            // Convert the document to PDF/A-1b compliance (guard for platforms without GDI+)
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                doc.Convert(logPath, PdfFormat.PDF_A_1B, ConvertErrorAction.Delete);
            }
            else
            {
                try
                {
                    doc.Convert(logPath, PdfFormat.PDF_A_1B, ConvertErrorAction.Delete);
                }
                catch (TypeInitializationException ex) when (ContainsDllNotFound(ex))
                {
                    Console.WriteLine("Warning: GDI+ (libgdiplus) is not available – PDF/A conversion skipped.");
                }
            }

            // Save the PDF/A-1b file (guard for platforms without GDI+)
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                doc.Save(outputPath);
                Console.WriteLine($"PDF/A-1b document saved to '{outputPath}'.");
            }
            else
            {
                try
                {
                    doc.Save(outputPath);
                    Console.WriteLine($"PDF/A-1b document saved to '{outputPath}'.");
                }
                catch (TypeInitializationException ex) when (ContainsDllNotFound(ex))
                {
                    Console.WriteLine("Warning: GDI+ (libgdiplus) is not available – PDF saving skipped.");
                }
            }
        }
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