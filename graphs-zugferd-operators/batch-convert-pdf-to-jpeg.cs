using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices;

class Program
{
    static void Main()
    {
        string dataDir = Directory.GetCurrentDirectory();

        CreateSamplePdf(Path.Combine(dataDir, "sample1.pdf"));
        CreateSamplePdf(Path.Combine(dataDir, "sample2.pdf"));

        string[] pdfFiles = Directory.GetFiles(dataDir, "*.pdf");
        int maxFiles = Math.Min(pdfFiles.Length, 4);
        for (int i = 0; i < maxFiles; i++)
        {
            string pdfPath = pdfFiles[i];
            using (Document pdfDocument = new Document(pdfPath))
            {
                Resolution resolution = new Resolution(150);
                JpegDevice jpegDevice = new JpegDevice(resolution);

                int pageCount = Math.Min(pdfDocument.Pages.Count, 4);
                for (int pageNumber = 1; pageNumber <= pageCount; pageNumber++)
                {
                    string fileNameWithoutExt = Path.GetFileNameWithoutExtension(pdfPath);
                    string jpegPath = Path.Combine(dataDir, $"{fileNameWithoutExt}_page{pageNumber}.jpeg");
                    using (FileStream jpegStream = new FileStream(jpegPath, FileMode.Create))
                    {
                        jpegDevice.Process(pdfDocument.Pages[pageNumber], jpegStream);
                    }
                }
            }
        }
    }

    private static void CreateSamplePdf(string outputPath)
    {
        using (Document doc = new Document())
        {
            doc.Pages.Add();
            Aspose.Pdf.Text.TextFragment fragment = new Aspose.Pdf.Text.TextFragment("Sample PDF");
            doc.Pages[1].Paragraphs.Add(fragment);
            doc.Save(outputPath);
        }
    }
}