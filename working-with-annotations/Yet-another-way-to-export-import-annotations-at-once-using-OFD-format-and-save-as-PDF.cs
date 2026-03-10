using System;
using System.IO;
using Aspose.Pdf; // Core Aspose.Pdf namespace

class Program
{
    static void Main()
    {
        // Paths for source OFD, intermediate PDF, XFDF, and final PDF
        const string ofdPath = "input.ofd";
        const string tempPdfPath = "temp.pdf";
        const string xfdfPath = "annotations.xfdf";
        const string finalPdfPath = "output.pdf";

        // Verify the OFD source exists
        if (!File.Exists(ofdPath))
        {
            Console.Error.WriteLine($"Source OFD not found: {ofdPath}");
            return;
        }

        // ------------------------------------------------------------
        // 1. Load the OFD document (input‑only format) using OfdLoadOptions
        // ------------------------------------------------------------
        using (Document ofdDoc = new Document(ofdPath, new OfdLoadOptions()))
        {
            // --------------------------------------------------------
            // 2. Export all annotations from the OFD to an XFDF file
            // --------------------------------------------------------
            ofdDoc.ExportAnnotationsToXfdf(xfdfPath);

            // --------------------------------------------------------
            // 3. Save the OFD as a PDF (intermediate file)
            // --------------------------------------------------------
            ofdDoc.Save(tempPdfPath); // No SaveOptions needed – PDF is the default format
        }

        // ------------------------------------------------------------
        // 4. Load the intermediate PDF and import the previously saved XFDF
        // ------------------------------------------------------------
        using (Document pdfDoc = new Document(tempPdfPath))
        {
            // Import annotations from the XFDF file into the PDF document
            pdfDoc.ImportAnnotationsFromXfdf(xfdfPath);

            // --------------------------------------------------------
            // 5. Save the final PDF that now contains the imported annotations
            // --------------------------------------------------------
            pdfDoc.Save(finalPdfPath);
        }

        // Optional cleanup of temporary files
        try { File.Delete(tempPdfPath); } catch { /* ignore */ }
        try { File.Delete(xfdfPath); } catch { /* ignore */ }

        Console.WriteLine($"Annotations exported from OFD and imported into PDF saved as '{finalPdfPath}'.");
    }
}