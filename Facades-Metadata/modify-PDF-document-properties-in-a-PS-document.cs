using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPs   = "input.ps";   // Source PostScript file
        const string tempPdf   = "temp.pdf";   // Intermediate PDF file
        const string outputPdf = "output.pdf"; // Final PDF with updated properties

        if (!File.Exists(inputPs))
        {
            Console.Error.WriteLine($"File not found: {inputPs}");
            return;
        }

        // 1. Convert PS to PDF using core API (PsLoadOptions is input‑only)
        using (Document doc = new Document(inputPs, new PsLoadOptions()))
        {
            // Save as PDF (no SaveOptions needed for PDF output)
            doc.Save(tempPdf);
        }

        // 2. Modify PDF metadata using the Facade API (PdfFileInfo)
        using (PdfFileInfo pdfInfo = new PdfFileInfo())
        {
            // Bind the intermediate PDF file
            pdfInfo.BindPdf(tempPdf);

            // Set desired document properties
            pdfInfo.Title    = "Updated Title";
            pdfInfo.Author   = "John Doe";
            pdfInfo.Subject  = "Sample Subject";
            pdfInfo.Keywords = "Aspose, PDF, PS conversion";

            // Save the updated information to the final output file
            pdfInfo.SaveNewInfo(outputPdf);
        }

        // Clean up the temporary PDF file
        if (File.Exists(tempPdf))
        {
            File.Delete(tempPdf);
        }

        Console.WriteLine($"PDF with updated properties saved to '{outputPdf}'.");
    }
}