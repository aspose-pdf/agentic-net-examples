using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input and output PDF file paths
        const string inputPdf  = "input.pdf";
        const string outputPdf = "output.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Use PdfFileInfo facade to modify document properties.
        // The facade implements IDisposable, so wrap it in a using block.
        using (PdfFileInfo pdfInfo = new PdfFileInfo())
        {
            // Bind the existing PDF file.
            pdfInfo.BindPdf(inputPdf);

            // Modify standard document properties.
            pdfInfo.Title   = "Updated Document Title";
            pdfInfo.Author  = "John Doe";
            pdfInfo.Subject = "Demonstration of property modification";
            pdfInfo.Keywords = "Aspose.Pdf, Facade, Metadata";

            // Set custom metadata entries.
            pdfInfo.SetMetaInfo("Company", "Acme Corp");
            pdfInfo.SetMetaInfo("Project", "PDF Property Update");

            // Save the updated PDF to a new file.
            // SaveNewInfo writes only the changed info and preserves the rest of the document.
            pdfInfo.SaveNewInfo(outputPdf);
        }

        Console.WriteLine($"PDF properties updated and saved to '{outputPdf}'.");
    }
}