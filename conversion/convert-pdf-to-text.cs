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

        try
        {
            // Load the PDF document and create an output stream for the text file
            using (Document doc = new Document(inputPath))
            using (FileStream outStream = new FileStream(outputPath, FileMode.Create, FileAccess.Write))
            {
                // TextDevice extracts plain text from each page
                TextDevice textDevice = new TextDevice();

                for (int pageNum = 1; pageNum <= doc.Pages.Count; pageNum++)
                {
                    // Process each page and write its text to the output stream
                    textDevice.Process(doc.Pages[pageNum], outStream);
                }
            }

            Console.WriteLine($"PDF successfully converted to text: '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during conversion: {ex.Message}");
        }
    }
}
