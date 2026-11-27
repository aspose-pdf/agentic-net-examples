using System;
using System.IO;
using System.Text;
using Aspose.Pdf;
using Aspose.Pdf.Devices;
using Aspose.Pdf.Text;

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
        using (Document doc = new Document(inputPath))
        {
            StringBuilder allText = new StringBuilder();

            // Iterate pages using 1‑based indexing (Aspose.Pdf requirement)
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                // Prepare a memory stream to capture the text extracted by TextDevice
                using (MemoryStream ms = new MemoryStream())
                {
                    // Configure extraction to Pure mode (no formatting, just raw characters)
                    TextExtractionOptions extractOpts = new TextExtractionOptions(TextExtractionOptions.TextFormattingMode.Pure);

                    // TextDevice is created with extraction options; then Process writes the page text to the stream.
                    TextDevice td = new TextDevice(extractOpts);
                    td.Process(doc.Pages[i], ms);

                    // Read the extracted text from the memory stream
                    ms.Position = 0;
                    using (StreamReader reader = new StreamReader(ms))
                    {
                        string pageText = reader.ReadToEnd();

                        // Remove line‑break characters to produce a single line per page
                        string cleaned = pageText
                            .Replace("\r\n", " ")
                            .Replace("\n", " ")
                            .Replace("\r", " ")
                            .Trim();

                        allText.AppendLine(cleaned);
                    }
                }
            }

            // Write the accumulated cleaned text to the output file
            File.WriteAllText(outputPath, allText.ToString());
        }

        Console.WriteLine($"Text extracted and saved to '{outputPath}'.");
    }
}
