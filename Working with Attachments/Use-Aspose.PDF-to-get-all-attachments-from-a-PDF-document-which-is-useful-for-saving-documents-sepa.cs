using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdfPath   = "input.pdf";          // source PDF with attachments
        const string outputPdfPath  = "output.pdf";         // copy of the PDF (saved as PDF)
        const string attachmentsDir = "Attachments";       // folder to store extracted files

        // Verify input file exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Ensure the attachments directory exists
        Directory.CreateDirectory(attachmentsDir);

        try
        {
            // ---------- Extract all attachments ----------
            using (PdfExtractor extractor = new PdfExtractor())
            {
                // Bind the PDF file to the extractor facade
                extractor.BindPdf(inputPdfPath);

                // Prepare extraction of attachments
                extractor.ExtractAttachment();

                // Write each attachment to the specified folder
                // GetAttachment(string) stores all attachments in the folder
                extractor.GetAttachment(attachmentsDir);
            }

            // ---------- Save (copy) the PDF ----------
            // Load the original PDF and save it to a new file.
            // This demonstrates the standard PDF save operation.
            using (Document doc = new Document(inputPdfPath))
            {
                doc.Save(outputPdfPath); // saves as PDF regardless of extension
            }

            Console.WriteLine("Attachments extracted to: " + Path.GetFullPath(attachmentsDir));
            Console.WriteLine("PDF saved to: " + Path.GetFullPath(outputPdfPath));
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}