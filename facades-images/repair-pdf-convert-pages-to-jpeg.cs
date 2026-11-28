using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Devices;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Step 1: Bind (repair) the PDF using PdfFileMend.
        // PdfFileMend does NOT implement IDisposable, so do NOT wrap it in a using block.
        PdfFileMend mend = new PdfFileMend();
        mend.BindPdf(inputPdfPath);

        // Save the repaired PDF to a temporary file for further processing.
        string repairedPdfPath = Path.Combine(Path.GetTempPath(), $"repaired_{Guid.NewGuid()}.pdf");
        mend.Save(repairedPdfPath);

        // Step 2: Load the repaired PDF and convert pages 2‑4 to separate JPG images.
        using (Document doc = new Document(repairedPdfPath))
        {
            // Configure the JPEG device (resolution + quality).
            var jpegDevice = new JpegDevice(new Resolution(150), 90);

            for (int pageNumber = 2; pageNumber <= 4; pageNumber++)
            {
                if (pageNumber > doc.Pages.Count)
                {
                    Console.WriteLine($"Page {pageNumber} does not exist in the document.");
                    continue;
                }

                string outputImagePath = $"page_{pageNumber}.jpg";
                using (FileStream outStream = new FileStream(outputImagePath, FileMode.Create, FileAccess.Write))
                {
                    jpegDevice.Process(doc.Pages[pageNumber], outStream);
                }
                Console.WriteLine($"Saved page {pageNumber} as {outputImagePath}");
            }
        }

        // Clean up the temporary repaired PDF.
        try
        {
            File.Delete(repairedPdfPath);
        }
        catch
        {
            // If deletion fails, ignore – the OS will clean up temp files eventually.
        }
    }
}
