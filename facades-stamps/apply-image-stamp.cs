using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        string inputDirectory = "input";
        string outputDirectory = "output";
        string stampImagePath = "stamp.png";

        Directory.CreateDirectory(inputDirectory);
        Directory.CreateDirectory(outputDirectory);

        // Ensure a stamp image exists; replace with a valid image path as needed.
        if (!File.Exists(stampImagePath))
        {
            Console.Error.WriteLine($"Stamp image not found: {stampImagePath}");
            return;
        }

        foreach (string pdfFilePath in Directory.GetFiles(inputDirectory, "*.pdf"))
        {
            string fileName = Path.GetFileName(pdfFilePath);
            string outputFilePath = Path.Combine(outputDirectory, fileName);

            PdfFileStamp pdfFileStamp = new PdfFileStamp();
            pdfFileStamp.InputFile = pdfFilePath;
            pdfFileStamp.OutputFile = outputFilePath;

            Stamp stamp = new Stamp();
            stamp.SetOrigin(100f, 500f);
            stamp.SetImageSize(100f, 100f);
            stamp.Opacity = 0.5f;
            stamp.IsBackground = true;
            stamp.BindImage(stampImagePath);

            pdfFileStamp.AddStamp(stamp);
            pdfFileStamp.Close();
        }

        Console.WriteLine("All PDFs have been stamped and saved to the output directory.");
    }
}