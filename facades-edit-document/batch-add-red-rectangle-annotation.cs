using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        string inputFolder = "Input";
        if (!Directory.Exists(inputFolder))
        {
            Console.Error.WriteLine($"Folder not found: {inputFolder}");
            return;
        }

        string[] pdfFiles = Directory.GetFiles(inputFolder, "*.pdf");
        foreach (string pdfPath in pdfFiles)
        {
            string fileName = Path.GetFileNameWithoutExtension(pdfPath);
            string outputFile = $"{fileName}_annotated.pdf";

            using (Document doc = new Document(pdfPath))
            {
                if (doc.Pages.Count == 0)
                {
                    Console.WriteLine($"No pages in {pdfPath}, skipping.");
                    continue;
                }

                Page page = doc.Pages[1];
                Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);
                SquareAnnotation square = new SquareAnnotation(page, rect);
                square.Color = Aspose.Pdf.Color.Red;
                page.Annotations.Add(square);

                doc.Save(outputFile);
                Console.WriteLine($"Annotated PDF saved as {outputFile}");
            }
        }
    }
}