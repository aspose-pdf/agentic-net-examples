using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        try
        {
            // Paths – adjust as needed
            const string texPath = "input.tex";
            const string tempPdfPath = "temp.pdf";
            const string outputPdfPath = "output.pdf";

            // Verify the TeX source exists
            if (!File.Exists(texPath))
            {
                Console.Error.WriteLine($"TeX file not found: {texPath}");
                return;
            }

            // Load the TeX file and convert it to a PDF document
            var texOptions = new TeXLoadOptions();
            Document pdfDoc = new Document(texPath, texOptions);

            // Save the intermediate PDF (uses the document‑save rule)
            pdfDoc.Save(tempPdfPath);

            // Open the intermediate PDF with PdfFileInfo to edit metadata
            using (var pdfInfo = new PdfFileInfo(tempPdfPath))
            {
                // Set desired metadata properties
                pdfInfo.Title = "Sample PDF generated from TeX";
                pdfInfo.Author = "John Doe";
                pdfInfo.Subject = "Demonstration of metadata editing via Aspose.Pdf.Facades";
                pdfInfo.Keywords = "Aspose.Pdf, TeX, PDF, Metadata";

                // PDF date strings must start with "D:" followed by "yyyyMMddHHmmss"
                string pdfDate = "D:" + DateTime.Now.ToString("yyyyMMddHHmmss");
                pdfInfo.CreationDate = pdfDate;
                pdfInfo.ModDate = pdfDate;

                // Save the final PDF with updated metadata
                pdfInfo.Save(outputPdfPath);
            }

            // Clean up the temporary file
            if (File.Exists(tempPdfPath))
            {
                File.Delete(tempPdfPath);
            }

            Console.WriteLine($"PDF created with metadata: {outputPdfPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}
