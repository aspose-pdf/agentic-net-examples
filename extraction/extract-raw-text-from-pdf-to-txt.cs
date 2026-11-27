using System;
using System.IO;
using System.Text;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputTxtPath = "output.txt";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"File not found: {inputPdfPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            var sb = new StringBuilder();

            // Process each page with TextAbsorber using Raw formatting mode and concatenate the results
            foreach (Page page in pdfDoc.Pages)
            {
                var absorber = new TextAbsorber();
                absorber.ExtractionOptions = new TextExtractionOptions(TextExtractionOptions.TextFormattingMode.Raw);
                page.Accept(absorber);
                sb.Append(absorber.Text);
            }

            // Write the concatenated text to the output file using UTF‑8 encoding
            File.WriteAllText(outputTxtPath, sb.ToString(), Encoding.UTF8);
        }

        Console.WriteLine($"Text extracted to '{outputTxtPath}'.");
    }
}
