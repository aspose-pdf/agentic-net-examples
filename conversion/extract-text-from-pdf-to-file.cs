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
            // Load the PDF document
            using (Document doc = new Document(inputPath))
            {
                // Create a TextDevice to extract text page‑by‑page
                using (FileStream outStream = new FileStream(outputPath, FileMode.Create, FileAccess.Write))
                {
                    TextDevice textDevice = new TextDevice();
                    for (int pageNumber = 1; pageNumber <= doc.Pages.Count; pageNumber++)
                    {
                        textDevice.Process(doc.Pages[pageNumber], outStream);
                    }
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
