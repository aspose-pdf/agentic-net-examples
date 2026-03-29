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
            string fileNameWithoutExt = Path.GetFileNameWithoutExtension(pdfPath);
            string outputFile = $"{fileNameWithoutExt}_annotated.pdf";

            using (Document doc = new Document(pdfPath))
            {
                if (doc.Pages.Count > 0)
                {
                    Page page = doc.Pages[1];
                    Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 700);
                    SquareAnnotation square = new SquareAnnotation(page, rect)
                    {
                        Color = Aspose.Pdf.Color.Red,
                        Title = "Red Rectangle",
                        Contents = "Batch added rectangle annotation"
                    };
                    page.Annotations.Add(square);
                }

                doc.Save(outputFile);
                Console.WriteLine($"Annotated PDF saved: {outputFile}");
            }
        }
    }
}