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
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal.
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            StringBuilder allText = new StringBuilder();

            // Pages are 1‑based in Aspose.Pdf.
            for (int pageIndex = 1; pageIndex <= pdfDoc.Pages.Count; pageIndex++)
            {
                Page page = pdfDoc.Pages[pageIndex];

                // Use TextAbsorber (core API) to extract the text of the page.
                TextAbsorber absorber = new TextAbsorber();
                absorber.Visit(page);
                allText.Append(absorber.Text);
            }

            // Write the concatenated text to the output file.
            File.WriteAllText(outputTxtPath, allText.ToString(), Encoding.UTF8);
        }

        Console.WriteLine($"Text extracted and saved to '{outputTxtPath}'.");
    }
}