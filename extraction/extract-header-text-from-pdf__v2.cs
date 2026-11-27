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
            // Define the rectangle that covers the header area.
            // Adjust the coordinates (llx, lly, urx, ury) as needed for your PDF.
            // Example assumes A4 size (595 x 842 points) with a header at the top.
            Aspose.Pdf.Rectangle headerRect = new Aspose.Pdf.Rectangle(0, 750, 595, 842);

            // Configure text search options to limit extraction to the header rectangle.
            // TextSearchOptions now requires a constructor argument (e.g., searchInAnnotations).
            TextSearchOptions searchOptions = new TextSearchOptions(true)
            {
                Rectangle = headerRect,
                LimitToPageBounds = true
            };

            // Set up the TextAbsorber with the search options
            TextAbsorber absorber = new TextAbsorber
            {
                TextSearchOptions = searchOptions
            };

            // Extract text from all pages within the defined rectangle
            doc.Pages.Accept(absorber);
            string headerText = absorber.Text ?? string.Empty;

            // Write the extracted header text to the output file
            File.WriteAllText(outputTxtPath, headerText);
        }

        Console.WriteLine($"Header text extracted to '{outputTxtPath}'.");
    }
}
