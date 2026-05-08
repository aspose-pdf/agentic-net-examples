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

        // Create a new PDF document (initially empty)
        using (Document doc = new Document())
        {
            // Add a page to the document (the document starts with zero pages)
            Page page = doc.Pages.Add();

            // Create a text paragraph
            TextParagraph paragraph = new TextParagraph
            {
                // Define the rectangle where the paragraph will be placed
                Rectangle = new Aspose.Pdf.Rectangle(100, 600, 400, 700)
            };

            // Enable word wrapping
            paragraph.FormattingOptions.WrapMode = TextFormattingOptions.WordWrapMode.ByWords;

            // Add lines of text
            paragraph.AppendLine("The quick brown fox jumps over the lazy dog.");
            paragraph.AppendLine("Second line of the paragraph.");
            paragraph.AppendLine("Third line.");

            // Append the paragraph to the page using TextBuilder
            TextBuilder builder = new TextBuilder(page);
            builder.AppendParagraph(paragraph);

            // Guard PDF/A conversion and saving on non‑Windows platforms (libgdiplus may be missing)
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                doc.Convert(logPath, PdfFormat.PDF_A_1B, ConvertErrorAction.Delete);
                doc.Save(outputPath);
            }
            else
            {
                Console.WriteLine("Running on a non‑Windows platform; PDF/A conversion and save may require libgdiplus.");
                try
                {
                    doc.Convert(logPath, PdfFormat.PDF_A_1B, ConvertErrorAction.Delete);
                }
                catch (TypeInitializationException ex) when (ContainsDllNotFound(ex))
                {
                    Console.WriteLine("PDF/A conversion skipped – missing GDI+ (libgdiplus).");
                }

                try
                {
                    doc.Save(outputPath);
                }
                catch (TypeInitializationException ex) when (ContainsDllNotFound(ex))
                {
                    Console.WriteLine("Document save skipped – missing GDI+ (libgdiplus).");
                }
            }
        }

        Console.WriteLine($"PDF/A‑1b process completed. Output path: '{outputPath}'.");
    }

    // Helper to detect a nested DllNotFoundException (e.g., missing libgdiplus)
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