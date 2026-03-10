using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string pclPath = "input.pcl";          // source PCL file
        const string tempPdfPath = "temp.pdf";       // intermediate PDF
        const string outputPdfPath = "output.pdf";   // final PDF with metadata

        if (!File.Exists(pclPath))
        {
            Console.Error.WriteLine($"File not found: {pclPath}");
            return;
        }

        // Load the PCL document and convert it to PDF.
        // PclLoadOptions is used because PCL is an input‑only format.
        using (Document doc = new Document(pclPath, new PclLoadOptions()))
        {
            // Save as PDF (no SaveOptions needed – default is PDF).
            doc.Save(tempPdfPath);
        }

        // Open the generated PDF with the PdfFileInfo facade to edit metadata.
        using (PdfFileInfo pdfInfo = new PdfFileInfo(tempPdfPath))
        {
            pdfInfo.Title    = "Sample Title";
            pdfInfo.Author   = "John Doe";
            pdfInfo.Subject  = "Sample Subject";
            pdfInfo.Keywords = "Aspose, PDF, Metadata";

            // Persist the updated metadata to the final output file.
            pdfInfo.SaveNewInfo(outputPdfPath);
        }

        // Clean up the intermediate file.
        try { File.Delete(tempPdfPath); } catch { }

        Console.WriteLine($"Metadata applied and saved to '{outputPdfPath}'.");
    }
}