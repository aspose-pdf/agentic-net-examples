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

        using (Document doc = new Document(inputPath))
        using (FileStream outStream = new FileStream(outputPath, FileMode.Create, FileAccess.Write))
        {
            // Use TextDevice to extract text page‑by‑page (TextSaveOptions does not exist)
            TextDevice textDevice = new TextDevice();
            for (int pageNum = 1; pageNum <= doc.Pages.Count; pageNum++)
            {
                textDevice.Process(doc.Pages[pageNum], outStream);
            }
        }

        Console.WriteLine($"PDF successfully converted to text: {outputPath}");
    }
}