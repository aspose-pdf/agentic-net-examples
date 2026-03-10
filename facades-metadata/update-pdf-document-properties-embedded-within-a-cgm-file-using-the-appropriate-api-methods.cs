using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input CGM file and intermediate/output PDF files
        const string cgmPath   = "input.cgm";
        const string tempPdf   = "temp.pdf";      // intermediate PDF generated from CGM
        const string outputPdf = "updated.pdf";   // final PDF with updated properties

        // Resolve to absolute file system paths – Aspose.Pdf.Facades.PdfProducer expects a valid file URI.
        string cgmFullPath   = Path.GetFullPath(cgmPath);
        string tempPdfFull   = Path.GetFullPath(tempPdf);
        string outputPdfFull = Path.GetFullPath(outputPdf);

        // Verify that the source CGM file exists before attempting conversion.
        if (!File.Exists(cgmFullPath))
        {
            Console.WriteLine($"Source CGM file not found: {cgmFullPath}");
            return;
        }

        // 1. Convert CGM to PDF using the Facades API.
        // PdfProducer.Produce handles the conversion; ImportFormat.Cgm specifies the source format.
        // Use absolute paths to avoid UriFormatException.
        PdfProducer.Produce(cgmFullPath, ImportFormat.Cgm, tempPdfFull);

        // 2. Open the generated PDF with PdfFileInfo to modify its metadata.
        using (PdfFileInfo pdfInfo = new PdfFileInfo(tempPdfFull))
        {
            // Update standard document properties
            pdfInfo.Title    = "New Document Title";
            pdfInfo.Author   = "John Doe";
            pdfInfo.Subject  = "Sample Subject";
            pdfInfo.Keywords = "keyword1, keyword2";

            // Add a custom metadata entry
            pdfInfo.SetMetaInfo("CustomProperty", "CustomValue");

            // 3. Save the updated information to a new PDF file.
            // SaveNewInfo writes only the changed metadata, preserving the rest of the document.
            bool success = pdfInfo.SaveNewInfo(outputPdfFull);
            Console.WriteLine(success
                ? $"PDF properties updated successfully: {outputPdfFull}"
                : "Failed to update PDF properties.");
        }

        // Optional: clean up the intermediate file if no longer needed
        // File.Delete(tempPdfFull);
    }
}
