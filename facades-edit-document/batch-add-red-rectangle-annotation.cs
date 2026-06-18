using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

namespace BatchAddRectangleAnnotation
{
    class Program
    {
        static void Main()
        {
            // Ensure the Input folder exists
            string inputFolder = "Input";
            Directory.CreateDirectory(inputFolder);

            // Create sample PDF files (self‑contained example)
            for (int i = 1; i <= 3; i++)
            {
                string samplePath = Path.Combine(inputFolder, $"sample{i}.pdf");
                using (Document sampleDoc = new Document())
                {
                    sampleDoc.Pages.Add();
                    sampleDoc.Save(samplePath);
                }
            }

            // Get PDF files from the Input folder (limit to 4 files due to evaluation mode)
            string[] pdfFiles = Directory.GetFiles(inputFolder, "*.pdf");
            int maxFiles = Math.Min(pdfFiles.Length, 4);
            for (int i = 0; i < maxFiles; i++)
            {
                string pdfPath = pdfFiles[i];
                using (Document doc = new Document(pdfPath))
                {
                    int pageCount = doc.Pages.Count;
                    for (int pageIndex = 1; pageIndex <= pageCount; pageIndex++)
                    {
                        Page page = doc.Pages[pageIndex];
                        Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100f, 100f, 200f, 200f);
                        SquareAnnotation square = new SquareAnnotation(page, rect);
                        square.Color = Aspose.Pdf.Color.Red;
                        page.Annotations.Add(square);
                    }
                    doc.Save(pdfPath);
                }
            }
        }
    }
}
