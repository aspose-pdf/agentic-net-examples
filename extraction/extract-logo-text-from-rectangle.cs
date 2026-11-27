using System;
using System.IO;
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
        using (Document doc = new Document(inputPdfPath))
        {
            // Define the rectangle that covers the logo area (coordinates in points)
            // Adjust llx, lly, urx, ury as needed for the specific PDF
            Aspose.Pdf.Rectangle logoRect = new Aspose.Pdf.Rectangle(
                llx: 50,   // lower‑left X
                lly: 750,  // lower‑left Y
                urx: 200,  // upper‑right X
                ury: 800   // upper‑right Y
            );

            // Configure the TextAbsorber to limit extraction to the defined rectangle
            TextAbsorber absorber = new TextAbsorber();
            absorber.TextSearchOptions = new TextSearchOptions(logoRect);

            // Apply the absorber to the first page (1‑based indexing)
            Page firstPage = doc.Pages[1];
            firstPage.Accept(absorber);

            // Retrieve the extracted text
            string extractedText = absorber.Text ?? string.Empty;

            // Save the extracted text to a plain‑text file
            File.WriteAllText(outputTxtPath, extractedText);
        }

        Console.WriteLine($"Logo text extracted to '{outputTxtPath}'.");
    }
}
