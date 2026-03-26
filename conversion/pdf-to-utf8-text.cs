using System;
using System.IO;
using System.Text;
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

        // Document implements IDisposable, so we can use a using block.
        using (Document doc = new Document(inputPath))
        {
            // TextDevice does NOT implement IDisposable – instantiate it directly.
            TextDevice device = new TextDevice(Encoding.UTF8);

            using (MemoryStream ms = new MemoryStream())
            {
                for (int i = 1; i <= doc.Pages.Count; i++)
                {
                    device.Process(doc.Pages[i], ms);
                    // Add a line break between pages to preserve layout.
                    ms.Write(Encoding.UTF8.GetBytes(Environment.NewLine));
                }

                // Write the extracted text preserving UTF‑8 encoding and line breaks.
                File.WriteAllBytes(outputPath, ms.ToArray());
            }
        }

        Console.WriteLine($"Text extracted to '{outputPath}'.");
    }
}
