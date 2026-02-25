using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;   // Facade namespace included as requested

class Program
{
    static void Main()
    {
        // Paths – adjust as needed
        const string mdInputPath   = "input.md";          // Markdown source
        const string pdfOutputPath = "intermediate.pdf"; // PDF generated from MD (may be PDF/A)
        const string finalPdfPath  = "final.pdf";        // Standard PDF output

        // Verify the Markdown file exists
        if (!File.Exists(mdInputPath))
        {
            Console.Error.WriteLine($"Markdown file not found: {mdInputPath}");
            return;
        }

        // -----------------------------------------------------------------
        // Step 1: Convert Markdown (MD) to PDF using MdLoadOptions.
        // -----------------------------------------------------------------
        MdLoadOptions mdLoadOptions = new MdLoadOptions();

        using (Document mdDoc = new Document(mdInputPath, mdLoadOptions))
        {
            // Save as PDF. No SaveOptions needed because the target format is PDF.
            mdDoc.Save(pdfOutputPath);
            Console.WriteLine($"Markdown converted to PDF: {pdfOutputPath}");
        }

        // -----------------------------------------------------------------
        // Step 2: Convert the resulting PDF/A (if the generated PDF is PDF/A)
        //         to a regular PDF. In Aspose.Pdf a PDF/A document can be saved
        //         directly as a normal PDF – no special conversion call is required.
        // -----------------------------------------------------------------
        if (!File.Exists(pdfOutputPath))
        {
            Console.Error.WriteLine($"Intermediate PDF not found: {pdfOutputPath}");
            return;
        }

        using (Document pdfaDoc = new Document(pdfOutputPath))
        {
            // Simply save the document again; this strips any PDF/A compliance
            // markers and produces a standard PDF file.
            pdfaDoc.Save(finalPdfPath);
            Console.WriteLine($"PDF/A converted to standard PDF: {finalPdfPath}");
        }
    }
}