using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        string inputFolder = "input-pdfs";
        string outputFolder = "output-pptx";

        if (!Directory.Exists(inputFolder))
        {
            Console.Error.WriteLine($"Input folder not found: {inputFolder}");
            return;
        }

        Directory.CreateDirectory(outputFolder);

        string[] pdfFiles = Directory.GetFiles(inputFolder, "*.pdf");
        foreach (string pdfPath in pdfFiles)
        {
            string fileName = Path.GetFileNameWithoutExtension(pdfPath);
            string pptxPath = Path.Combine(outputFolder, fileName + ".pptx");

            using (Document pdfDoc = new Document(pdfPath))
            {
                PptxSaveOptions options = new PptxSaveOptions
                {
                    SlidesAsImages = true
                };
                pdfDoc.Save(pptxPath, options);
            }

            Console.WriteLine($"Converted: {pdfPath} -> {pptxPath}");
        }
    }
}