using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.txt";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document and extract its text using TextDevice (TextSaveOptions does not exist)
        using (Document pdfDoc = new Document(inputPath))
        using (FileStream outStream = new FileStream(outputPath, FileMode.Create, FileAccess.Write))
        {
            TextDevice txtDevice = new TextDevice();
            for (int pageNum = 1; pageNum <= pdfDoc.Pages.Count; pageNum++)
            {
                txtDevice.Process(pdfDoc.Pages[pageNum], outStream);
            }
        }

        Console.WriteLine($"PDF successfully converted to text: {outputPath}");
    }
}