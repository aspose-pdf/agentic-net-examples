using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input PDF and output paths
        const string inputPdfPath = "input.pdf";
        const string tempExtractPath = "extracted_pages.pdf";
        const string outputPdfPath = "rotated_output.pdf";

        // Pages to extract (1‑based indexing)
        int[] pagesToExtract = new int[] { 2, 4, 5 }; // adjust as needed

        // Verify input file exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        try
        {
            // -----------------------------------------------------------------
            // 1. Extract the selected pages using PdfFileEditor (Facades API)
            // -----------------------------------------------------------------
            // NOTE: In recent Aspose.PDF versions the Extract method signature is:
            // Extract(string sourceFile, int[] pageNumbers, string outputFile)
            PdfFileEditor fileEditor = new PdfFileEditor();
            fileEditor.Extract(inputPdfPath, pagesToExtract, tempExtractPath);

            // -----------------------------------------------------------------
            // 2. Rotate the extracted pages using the Document API (recommended)
            // -----------------------------------------------------------------
            Document extractedDoc = new Document(tempExtractPath);
            const Rotation rotation = Rotation.on90; // 90° clockwise

            foreach (Page page in extractedDoc.Pages)
            {
                page.Rotate = rotation;
            }

            // Save the rotated pages to the final output PDF
            extractedDoc.Save(outputPdfPath);

            // Optional: clean up the temporary extracted file
            if (File.Exists(tempExtractPath))
                File.Delete(tempExtractPath);

            Console.WriteLine($"Successfully created rotated PDF: {outputPdfPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
