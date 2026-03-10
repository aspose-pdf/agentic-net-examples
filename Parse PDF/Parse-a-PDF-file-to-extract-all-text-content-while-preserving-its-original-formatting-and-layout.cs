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
        const string inputPdfPath  = "input.pdf";   // source PDF
        const string outputTxtPath = "extracted.txt"; // destination text file

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"File not found: {inputPdfPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Configure text extraction to preserve layout (position‑based)
            var extractionOptions = new TextExtractionOptions(TextExtractionOptions.TextFormattingMode.Flatten);
            var textDevice = new TextDevice(extractionOptions);

            // Accumulate extracted text from all pages
            using (MemoryStream ms = new MemoryStream())
            {
                foreach (Page page in pdfDoc.Pages)
                {
                    // Process each page and write its text to the memory stream
                    textDevice.Process(page, ms);
                    // Add a line break between pages for readability
                    ms.Write(Encoding.Unicode.GetBytes(Environment.NewLine));
                }

                // Convert the accumulated bytes to a string using Unicode encoding
                string extractedText = Encoding.Unicode.GetString(ms.ToArray());

                // Write the extracted text to the output file
                File.WriteAllText(outputTxtPath, extractedText, Encoding.Unicode);
                Console.WriteLine($"Text extracted to '{outputTxtPath}'.");
            }
        }
    }
}