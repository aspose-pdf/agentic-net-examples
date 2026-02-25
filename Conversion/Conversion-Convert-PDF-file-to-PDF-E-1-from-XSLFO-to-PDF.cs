using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string xslFoPath = "input.xslfo";
        const string outputPdfE1Path = "output_pdfe1.pdf";

        if (!File.Exists(xslFoPath))
        {
            Console.Error.WriteLine($"XSL-FO file not found: {xslFoPath}");
            return;
        }

        try
        {
            // Load the XSL-FO file
            XslFoLoadOptions loadOptions = new XslFoLoadOptions();

            using (Document doc = new Document(xslFoPath, loadOptions))
            {
                // Convert the document to PDF/E-1 format
                // Log conversion details to a text file
                doc.Convert("conversion_log.txt", PdfFormat.PDF_E_1, ConvertErrorAction.Delete);

                // Save the converted PDF/E-1 document
                doc.Save(outputPdfE1Path);
            }

            Console.WriteLine($"Conversion completed: {outputPdfE1Path}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}