using System;
using System.IO;
using System.Text;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Use a temporary folder so the example works on any machine
        string tempFolder = Path.GetTempPath();
        string inputPdfPath   = Path.Combine(tempFolder, "input.pdf");
        string outputPdfPath  = Path.Combine(tempFolder, "output_zugferd.pdf");
        string zugferdPath    = Path.Combine(tempFolder, "invoice.xml");
        string validationLog  = Path.Combine(tempFolder, "validation_log.txt");
        string conversionLog  = Path.Combine(tempFolder, "conversion_log.txt");

        // ---------------------------------------------------------------------
        // 1. Ensure a source PDF exists – create a simple PDF if it does not.
        // ---------------------------------------------------------------------
        if (!File.Exists(inputPdfPath))
        {
            using (Document srcDoc = new Document())
            {
                Page page = srcDoc.Pages.Add();
                page.Paragraphs.Add(new TextFragment("Sample PDF document – will be converted to PDF/A before ZUGFeRD attachment."));
                srcDoc.Save(inputPdfPath);
            }
        }

        // ---------------------------------------------------------------------
        // 2. Ensure a ZUGFeRD XML file exists – create a minimal example.
        // ---------------------------------------------------------------------
        if (!File.Exists(zugferdPath))
        {
            string sampleXml = "<?xml version=\"1.0\" encoding=\"UTF-8\"?>\n<Invoice>\n  <ID>12345</ID>\n  <IssueDate>2023-01-01</IssueDate>\n</Invoice>";
            File.WriteAllText(zugferdPath, sampleXml, Encoding.UTF8);
        }

        // ---------------------------------------------------------------------
        // 3. Load the PDF, convert it to PDF/A‑2U (required for compliance),
        //    embed the ZUGFeRD file, save, and finally validate.
        // ---------------------------------------------------------------------
        using (Document doc = new Document(inputPdfPath))
        {
            // Convert to PDF/A‑2U – this also creates a conversion log.
            doc.Convert(conversionLog, PdfFormat.PDF_A_2U, ConvertErrorAction.Delete);

            // Embed the ZUGFeRD XML file using a FileSpecification with a MemoryStream.
            var fileSpec = new FileSpecification("invoice.xml", "ZUGFeRD invoice attachment");
            fileSpec.Contents = new MemoryStream(File.ReadAllBytes(zugferdPath));
            doc.EmbeddedFiles.Add(fileSpec);

            // Save the resulting PDF.
            doc.Save(outputPdfPath);

            // Validate the saved PDF against PDF/A‑2U.
            bool isValid = doc.Validate(validationLog, PdfFormat.PDF_A_2U);

            Console.WriteLine($"Validation succeeded: {isValid}");
            Console.WriteLine($"Document IsPdfaCompliant: {doc.IsPdfaCompliant}");
            Console.WriteLine($"Output PDF saved to: {outputPdfPath}");
            Console.WriteLine($"Validation log saved to: {validationLog}");
        }
    }
}
