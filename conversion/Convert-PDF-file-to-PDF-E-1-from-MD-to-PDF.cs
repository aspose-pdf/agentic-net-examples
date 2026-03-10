using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string mdInputPath = "input.md";
        const string pdfeOutputPath = "output_pdfe1.pdf";

        if (!File.Exists(mdInputPath))
        {
            Console.Error.WriteLine($"Input file not found: {mdInputPath}");
            return;
        }

        // Load the Markdown file using MdLoadOptions
        MdLoadOptions mdLoadOptions = new MdLoadOptions();

        // Wrap the Document in a using block for proper disposal
        using (Document doc = new Document(mdInputPath, mdLoadOptions))
        {
            // Convert the document to PDF/E‑1 format
            // PdfFormat.PDF_E_1 is the enum value representing PDF/E‑1
            PdfFormatConversionOptions conversionOptions = new PdfFormatConversionOptions(
                PdfFormat.PDF_E_1,               // target format
                ConvertErrorAction.Delete);      // action for objects that cannot be converted

            doc.Convert(conversionOptions);

            // Save the converted document
            doc.Save(pdfeOutputPath);
        }

        Console.WriteLine($"Markdown converted to PDF/E‑1: {pdfeOutputPath}");
    }
}
