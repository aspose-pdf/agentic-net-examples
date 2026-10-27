using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "stamped_output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        Document doc = new Document(inputPath);

        // Create a HeaderFooter object that will act as the header
        HeaderFooter header = new HeaderFooter();

        // Use the {file_name} placeholder – Aspose.Pdf replaces it with the actual file name when rendering.
        // If the placeholder is not supported in the used version, you can replace it manually:
        // string fileName = Path.GetFileName(inputPath);
        // TextFragment tf = new TextFragment(fileName);
        TextFragment tf = new TextFragment("{file_name}");
        tf.TextState.Font = FontRepository.FindFont("Arial");
        tf.TextState.FontSize = 12;
        tf.TextState.ForegroundColor = Aspose.Pdf.Color.Black;
        header.Paragraphs.Add(tf);

        // Assign the header to every page in the document
        foreach (Page page in doc.Pages)
        {
            page.Header = header;
        }

        // Save the modified PDF
        doc.Save(outputPath);

        Console.WriteLine($"Stamped PDF saved to '{outputPath}'.");
    }
}
