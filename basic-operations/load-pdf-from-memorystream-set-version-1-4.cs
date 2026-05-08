using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Load PDF bytes. If "input.pdf" does not exist, create a simple PDF in memory.
        byte[] pdfBytes;
        const string inputPath = "input.pdf";
        if (File.Exists(inputPath))
        {
            pdfBytes = File.ReadAllBytes(inputPath);
        }
        else
        {
            // Create a minimal PDF (one blank page) and obtain its bytes.
            using (var tempDoc = new Document())
            {
                tempDoc.Pages.Add();
                using (var tempMs = new MemoryStream())
                {
                    tempDoc.Save(tempMs);
                    pdfBytes = tempMs.ToArray();
                }
            }
        }

        // Load the PDF from a memory stream
        using (MemoryStream ms = new MemoryStream(pdfBytes))
        using (Document doc = new Document(ms))
        {
            // Change the PDF version to 1.4 using Document.Convert()
            // A temporary log file is required by the Convert method.
            string logPath = Path.Combine(Path.GetTempPath(), "convert_log.xml");
            doc.Convert(logPath, PdfFormat.v_1_4, ConvertErrorAction.Delete);

            // Save the document to the file system (PDF format)
            const string outputPath = "output.pdf";
            doc.Save(outputPath);
            Console.WriteLine($"PDF saved as '{outputPath}' with version 1.4.");
        }
    }
}
