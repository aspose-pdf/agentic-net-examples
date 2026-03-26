using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices;

class Program
{
    static void Main()
    {
        const string inputFolder = "input-pdfs";
        const string outputFolder = "output-images";

        if (!Directory.Exists(inputFolder))
        {
            Console.Error.WriteLine($"Input folder not found: {inputFolder}");
            return;
        }

        Directory.CreateDirectory(outputFolder);
        string[] pdfFiles = Directory.GetFiles(inputFolder, "*.pdf");

        foreach (string pdfPath in pdfFiles)
        {
            string baseName = Path.GetFileNameWithoutExtension(pdfPath);
            using (Document doc = new Document(pdfPath))
            {
                // JpegDevice does NOT implement IDisposable – instantiate directly.
                // Resolution is read‑only; set it via constructor. Quality is also set via constructor.
                var jpegDevice = new JpegDevice(new Resolution(150), 90);

                for (int pageNum = 1; pageNum <= doc.Pages.Count; pageNum++)
                {
                    string outPath = Path.Combine(outputFolder, $"{baseName}_page{pageNum}.jpg");
                    // JpegDevice uses Process(page, stream) – open a FileStream for the output file.
                    using (FileStream outStream = new FileStream(outPath, FileMode.Create, FileAccess.Write))
                    {
                        jpegDevice.Process(doc.Pages[pageNum], outStream);
                    }
                }
            }
        }

        Console.WriteLine("Batch conversion completed.");
    }
}
