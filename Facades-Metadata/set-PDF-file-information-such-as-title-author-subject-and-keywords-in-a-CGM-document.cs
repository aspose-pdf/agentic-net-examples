using System;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputCgm   = "input.cgm";   // source CGM file
        const string tempPdf    = "temp.pdf";    // intermediate PDF produced from CGM
        const string outputPdf  = "output.pdf"; // final PDF with metadata

        // -----------------------------------------------------------------
        // 1. Convert CGM to PDF using the static PdfProducer API.
        // -----------------------------------------------------------------
        try
        {
            // ImportFormat.Cgm is defined in Aspose.Pdf namespace.
            PdfProducer.Produce(inputCgm, ImportFormat.Cgm, tempPdf);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error converting CGM to PDF: {ex.Message}");
            return;
        }

        // -----------------------------------------------------------------
        // 2. Open the generated PDF with PdfFileInfo and set metadata.
        // -----------------------------------------------------------------
        try
        {
            // PdfFileInfo implements IDisposable – use a using block.
            using (PdfFileInfo pdfInfo = new PdfFileInfo(tempPdf))
            {
                // Set standard document properties.
                pdfInfo.Title    = "Sample Title";
                pdfInfo.Author   = "John Doe";
                pdfInfo.Subject  = "Sample Subject";
                pdfInfo.Keywords = "keyword1, keyword2";

                // Save the updated information to a new file.
                // SaveNewInfo writes only the changed metadata without re‑creating the whole PDF.
                pdfInfo.SaveNewInfo(outputPdf);
            }

            Console.WriteLine($"PDF with updated metadata saved to '{outputPdf}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error setting PDF metadata: {ex.Message}");
        }
    }
}