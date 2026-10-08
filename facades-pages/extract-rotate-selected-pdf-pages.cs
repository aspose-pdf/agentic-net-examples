using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string sourcePdf = "source.pdf";
        const string finalPdf  = "selected_rotated.pdf";
        const string tempPdf   = "temp_extracted.pdf";

        // 1‑based page numbers to extract
        int[] pagesToExtract = new int[] { 2, 4, 5 };

        if (!File.Exists(sourcePdf))
        {
            Console.Error.WriteLine($"Source file not found: {sourcePdf}");
            return;
        }

        // -------------------------------------------------
        // Extract the selected pages to a temporary PDF file
        // -------------------------------------------------
        PdfFileEditor fileEditor = new PdfFileEditor();
        // NOTE: In the Aspose.Pdf version used, the Extract overload expects
        // (sourcePath, int[] pageNumbers, outputPath).
        fileEditor.Extract(sourcePdf, pagesToExtract, tempPdf);

        // -------------------------------------------------
        // Rotate each page in the extracted PDF
        // -------------------------------------------------
        // Load the temporary PDF as a Document to access page objects.
        Document doc = new Document(tempPdf);
        for (int i = 1; i <= doc.Pages.Count; i++)
        {
            // Rotate 90 degrees clockwise.
            doc.Pages[i].Rotate = Rotation.on90;
        }
        // Save the rotated pages back to the temporary file.
        doc.Save(tempPdf);

        // -------------------------------------------------
        // Rename the temporary file to the final output name
        // -------------------------------------------------
        if (File.Exists(finalPdf))
            File.Delete(finalPdf);
        File.Move(tempPdf, finalPdf);

        Console.WriteLine($"Created PDF with selected rotated pages: {finalPdf}");
    }
}
