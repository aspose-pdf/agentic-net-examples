using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // PdfFileInfo is a facade for PDF metadata manipulation.
        // It implements IDisposable, so wrap it in a using block.
        using (PdfFileInfo pdfInfo = new PdfFileInfo())
        {
            // Bind the existing PDF file.
            pdfInfo.BindPdf(inputPath);

            // Update standard metadata properties.
            pdfInfo.Title        = "Updated Document Title";
            pdfInfo.Author       = "Jane Smith";
            pdfInfo.Subject      = "Demonstration of metadata update";
            pdfInfo.Keywords     = "Aspose.Pdf, Metadata, C#";
            pdfInfo.Creator      = "MetadataUpdaterApp";
            // CreationDate and ModDate are string properties that expect PDF date format.
            string pdfDate = DateTime.Now.ToString("yyyyMMddHHmmss");
            pdfInfo.CreationDate = pdfDate;
            pdfInfo.ModDate      = pdfDate;

            // Persist the changes to a new file.
            pdfInfo.SaveNewInfo(outputPath);
        }

        Console.WriteLine($"Metadata successfully updated and saved to '{outputPath}'.");
    }
}
