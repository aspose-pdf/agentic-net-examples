using System;
using System.IO;
using System.Text;
using Aspose.Pdf;
using Aspose.Pdf.Devices;

class Program
{
    static void Main()
    {
        const string inputPdfPath  = "input.pdf";
        const string outputTxtPath = "first_page.txt";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Create a TextDevice that outputs UTF‑8 encoded text
            TextDevice textDevice = new TextDevice(Encoding.UTF8);

            // Process the first page (1‑based indexing) and write directly to the text file
            textDevice.Process(pdfDoc.Pages[1], outputTxtPath);
        }

        Console.WriteLine($"Extracted text saved to '{outputTxtPath}'.");
    }
}