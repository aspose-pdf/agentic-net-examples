using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // -------------------------------------------------------------------
        // 1. Create a minimal input PDF so the example can run in an empty sandbox
        // -------------------------------------------------------------------
        const string inputPath = "input.pdf";
        using (Document seed = new Document())
        {
            seed.Pages.Add(); // add a blank page
            seed.Save(inputPath);
        }

        // -------------------------------------------------------------------
        // 2. Load the PDF bytes into a MemoryStream
        // -------------------------------------------------------------------
        byte[] pdfBytes = File.ReadAllBytes(inputPath);
        using (MemoryStream ms = new MemoryStream(pdfBytes))
        using (Document doc = new Document(ms))
        {
            // -------------------------------------------------------------------
            // 3. Change the PDF version to 1.4 using the core Document.Convert API
            // -------------------------------------------------------------------
            string logPath = Path.Combine(Path.GetTempPath(), "pdf_version_convert_log.xml");
            doc.Convert(logPath, PdfFormat.v_1_4, ConvertErrorAction.Delete);

            // -------------------------------------------------------------------
            // 4. Save the updated document to the file system
            // -------------------------------------------------------------------
            const string outputPath = "output.pdf";
            doc.Save(outputPath);
        }

        Console.WriteLine("PDF loaded from stream, version set to 1.4, and saved to file.");
    }
}
