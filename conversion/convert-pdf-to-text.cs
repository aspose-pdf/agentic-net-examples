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

        // Load the PDF document inside a using block for deterministic disposal
        using (Document pdfDoc = new Document(inputPath))
        {
            // TextDevice extracts text from pages. It writes directly to a stream.
            TextDevice textDevice = new TextDevice();

            using (FileStream outStream = new FileStream(outputPath, FileMode.Create, FileAccess.Write))
            {
                for (int pageNum = 1; pageNum <= pdfDoc.Pages.Count; pageNum++)
                {
                    // Process each page and write its text to the output stream
                    textDevice.Process(pdfDoc.Pages[pageNum], outStream);
                }
            }
        }

        Console.WriteLine($"PDF converted to text: '{outputPath}'");
    }
}
