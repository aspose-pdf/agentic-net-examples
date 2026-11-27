using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputResultPath = "result.txt";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"File not found: {inputPdfPath}");
            return;
        }

        try
        {
            // Load the PDF document inside a using block for deterministic disposal
            using (Document pdfDoc = new Document(inputPdfPath))
            {
                // Extract all text from the document using TextAbsorber
                TextAbsorber absorber = new TextAbsorber();
                pdfDoc.Pages.Accept(absorber);
                string extractedText = absorber.Text ?? string.Empty;

                // Write the extracted text into a MemoryStream
                using (MemoryStream ms = new MemoryStream())
                {
                    using (StreamWriter writer = new StreamWriter(ms))
                    {
                        writer.Write(extractedText);
                        writer.Flush(); // Ensure all data is written to the stream
                    }

                    // Check the length of the MemoryStream (in bytes)
                    long streamLength = ms.Length;

                    // Write the length result to an output file
                    File.WriteAllText(outputResultPath, $"Extracted text length (bytes): {streamLength}");
                }
            }

            Console.WriteLine($"Result written to '{outputResultPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}