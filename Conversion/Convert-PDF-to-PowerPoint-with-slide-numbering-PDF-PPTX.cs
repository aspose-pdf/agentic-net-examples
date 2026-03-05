using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string tempPdf = "temp_numbered.pdf";
        const string outputPptx = "output.pptx";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Load the original PDF and add page numbers.
        using (Document pdfDoc = new Document(inputPdf))
        {
            // PdfFileStamp adds page numbers; "#" is replaced by the actual number.
            PdfFileStamp stamp = new PdfFileStamp();
            stamp.BindPdf(pdfDoc);
            stamp.AddPageNumber("#");
            // Save the PDF with page numbers to a temporary file.
            pdfDoc.Save(tempPdf);
            stamp.Close();
        }

        // Convert the numbered PDF to PPTX.
        using (Document numberedDoc = new Document(tempPdf))
        {
            PptxSaveOptions pptxOptions = new PptxSaveOptions();
            // Save as PPTX; slide numbers are already embedded as part of the PDF content.
            numberedDoc.Save(outputPptx, pptxOptions);
        }

        // Optional: delete the temporary PDF.
        try { File.Delete(tempPdf); } catch { }

        Console.WriteLine($"Conversion complete. PPTX saved to '{outputPptx}'.");
    }
}