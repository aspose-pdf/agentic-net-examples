using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string mhtPath = "input.mht";
        const string outputPdfPath = "output_pdfE1.pdf";

        if (!File.Exists(mhtPath))
        {
            Console.Error.WriteLine($"MHT file not found: {mhtPath}");
            return;
        }

        try
        {
            // Load the MHT file with default options
            MhtLoadOptions loadOptions = new MhtLoadOptions();

            using (Document doc = new Document(mhtPath, loadOptions))
            {
                // Prepare conversion options for PDF/E-1 format
                PdfFormatConversionOptions convOptions = new PdfFormatConversionOptions(
                    PdfFormat.PDF_E_1,          // target PDF/E-1 format
                    ConvertErrorAction.Delete   // action for objects that cannot be converted
                );

                // Perform the conversion
                bool converted = doc.Convert(convOptions);
                if (!converted)
                {
                    Console.Error.WriteLine("Conversion reported issues, but the document will still be saved.");
                }

                // Save the resulting PDF/E-1 document
                doc.Save(outputPdfPath);
            }

            Console.WriteLine($"MHT successfully converted to PDF/E-1: {outputPdfPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during conversion: {ex.Message}");
        }
    }
}