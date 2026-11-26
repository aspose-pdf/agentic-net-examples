using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf   = "source.pdf";
        const string attachFile = "attachment.txt";
        const string outputPdf  = "output_pdfa3a.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }

        if (!File.Exists(attachFile))
        {
            Console.Error.WriteLine($"Attachment file not found: {attachFile}");
            return;
        }

        try
        {
            // Load the source PDF
            using (Document doc = new Document(inputPdf))
            {
                // Embed the external file using the EmbeddedFiles collection
                var fileSpec = new FileSpecification(Path.GetFileName(attachFile), "Attached file");
                fileSpec.Contents = new MemoryStream(File.ReadAllBytes(attachFile));
                doc.EmbeddedFiles.Add(fileSpec);

                // Convert the document to PDF/A‑3a using PdfFormatConversionOptions
                var conversionOptions = new PdfFormatConversionOptions(PdfFormat.PDF_A_3A);
                doc.Convert(conversionOptions);

                // Save the converted PDF/A‑3a document (default SaveFormat is PDF)
                doc.Save(outputPdf);
            }

            Console.WriteLine($"PDF/A‑3a file saved to '{outputPdf}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
