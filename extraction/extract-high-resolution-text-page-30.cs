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

        // Load the PDF inside a using block for deterministic disposal.
        using (Document doc = new Document(inputPdfPath))
        {
            // Ensure the document has at least 30 pages (Aspose.Pdf uses 1‑based indexing).
            if (doc.Pages.Count < 30)
            {
                Console.Error.WriteLine("The document does not contain page 30.");
                return;
            }

            // Configure extraction options: high scale factor.
            // The constructor now requires a TextFormattingMode argument.
            TextExtractionOptions extractionOpts = new TextExtractionOptions(TextExtractionOptions.TextFormattingMode.Pure)
            {
                ScaleFactor = 10 // increase resolution for better accuracy
                // Note: ColumnLayoutMode is not available in the current Aspose.Pdf version.
                // The default layout treats the page as a single wide column, which satisfies the requirement.
            };

            // Create a TextAbsorber with the configured options.
            TextAbsorber absorber = new TextAbsorber
            {
                ExtractionOptions = extractionOpts
            };

            // Extract text only from page 30.
            doc.Pages[30].Accept(absorber);

            // Write the extracted text to a plain‑text file.
            File.WriteAllText(outputTxtPath, absorber.Text);
        }

        Console.WriteLine($"Text from page 30 saved to '{outputTxtPath}'.");
    }
}
