using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string cgmPath = "input.cgm";
        const string pdfPath = "intermediate.pdf";
        const string docxPath = "output.docx";

        if (!File.Exists(cgmPath))
        {
            Console.Error.WriteLine($"CGM file not found: {cgmPath}");
            return;
        }

        try
        {
            // Convert CGM to PDF using PdfProducer (Facades API)
            PdfProducer.Produce(cgmPath, ImportFormat.Cgm, pdfPath);

            // Load the generated PDF
            using (Document pdfDoc = new Document(pdfPath))
            {
                // Prepare DOCX save options
                DocSaveOptions docxOptions = new DocSaveOptions
                {
                    Format = DocSaveOptions.DocFormat.DocX
                };

                // Save as DOCX
                pdfDoc.Save(docxPath, docxOptions);
            }

            Console.WriteLine($"Conversion successful: '{docxPath}'");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during conversion: {ex.Message}");
        }
    }
}