using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class PdfTransformation
{
    static void Main()
    {
        const string inputPdf = "source.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // -----------------------------------------------------------------
        // 1. Load the source PDF (using the Document constructor – load rule)
        // -----------------------------------------------------------------
        using (Document srcDoc = new Document(inputPdf))
        {
            // -----------------------------------------------------------------
            // 2. Save a plain copy (PDF format – no special SaveOptions needed)
            // -----------------------------------------------------------------
            string copyPath = "output_copy.pdf";
            srcDoc.Save(copyPath);
            Console.WriteLine($"Plain copy saved to '{copyPath}'.");

            // -----------------------------------------------------------------
            // 3. Convert to PDF/A‑1B (maintains content fidelity for archival)
            // -----------------------------------------------------------------
            string pdfAPath = "output_pdfa.pdf";
            var pdfAOptions = new PdfFormatConversionOptions(PdfFormat.PDF_A_1B, ConvertErrorAction.Delete);
            // Convert modifies the document in place
            srcDoc.Convert(pdfAOptions);
            srcDoc.Save(pdfAPath);
            Console.WriteLine($"PDF/A‑1B saved to '{pdfAPath}'.");

            // -----------------------------------------------------------------
            // 4. Convert to PDF/X‑3 (for printing workflows)
            // -----------------------------------------------------------------
            // Reload original document to avoid cumulative conversions
            srcDoc.Dispose(); // ensure previous instance is released
        }

        // Reload original document for the next conversion
        using (Document srcDoc = new Document(inputPdf))
        {
            string pdfXPath = "output_pdfx.pdf";
            var pdfXOptions = new PdfFormatConversionOptions(PdfFormat.PDF_X_3, ConvertErrorAction.Delete);
            srcDoc.Convert(pdfXOptions);
            srcDoc.Save(pdfXPath);
            Console.WriteLine($"PDF/X‑3 saved to '{pdfXPath}'.");
        }

        // -----------------------------------------------------------------
        // 5. Use PdfFileEditor (Facade) to split the source PDF into
        //    single‑page PDFs. This demonstrates Facades usage while keeping
        //    the original content unchanged.
        // -----------------------------------------------------------------
        var editor = new PdfFileEditor();
        string pageTemplate = "page_%NUM%.pdf"; // %NUM% will be replaced by page number
        editor.SplitToPages(inputPdf, pageTemplate);
        Console.WriteLine("Document split into individual pages using PdfFileEditor.");

        // -----------------------------------------------------------------
        // 6. (Optional) Print the original PDF using PdfViewer.
        //    Demonstrates another Facade class without altering the file.
        // -----------------------------------------------------------------
        var viewer = new PdfViewer();
        try
        {
            viewer.BindPdf(inputPdf);
            viewer.PrintDocument(); // prints using default printer
            Console.WriteLine("Print job sent via PdfViewer.");
        }
        finally
        {
            viewer.Close();
        }
    }
}