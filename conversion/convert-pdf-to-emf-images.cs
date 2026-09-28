using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputDir = "EMF_Output";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        Directory.CreateDirectory(outputDir);

        try
        {
            // Load PDF inside a using block for deterministic disposal
            using (Document pdfDoc = new Document(inputPath))
            {
                // EmfDevice does NOT implement IDisposable – instantiate once and reuse
                Resolution resolution = new Resolution(300);
                EmfDevice emfDevice = new EmfDevice(resolution);

                // Aspose.Pdf uses 1‑based page indexing
                for (int i = 1; i <= pdfDoc.Pages.Count; i++)
                {
                    string outPath = Path.Combine(outputDir, $"Page_{i}.emf");
                    // Process each page into a stream; the stream is disposed via using
                    using (FileStream outStream = new FileStream(outPath, FileMode.Create, FileAccess.Write))
                    {
                        emfDevice.Process(pdfDoc.Pages[i], outStream);
                    }
                }
            }

            Console.WriteLine($"PDF successfully converted to EMF images in '{outputDir}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
