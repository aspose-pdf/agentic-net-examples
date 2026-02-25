using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Input and output PDF file paths
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        // Verify that the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: File not found – {inputPath}");
            return;
        }

        // Open the PDF document, then save it to a new file.
        // The using block ensures deterministic disposal of the Document object.
        using (Document doc = new Document(inputPath))
        {
            doc.Save(outputPath); // Simple copy / conversion to PDF format
        }

        Console.WriteLine($"PDF conversion completed. Output saved to '{outputPath}'.");
    }
}