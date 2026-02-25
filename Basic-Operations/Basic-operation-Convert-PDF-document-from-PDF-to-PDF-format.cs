using System;
using System.IO;
using Aspose.Pdf; // Fully qualified types are used below to avoid ambiguity

class PdfToPdfConverter
{
    static void Main()
    {
        // Define input and output file paths
        string inputPath = "input.pdf";
        string outputPath = "output.pdf";

        // Ensure the input file exists to avoid DirectoryNotFoundException
        if (!File.Exists(inputPath))
        {
            Console.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Open the source PDF for reading and the destination PDF for writing
        using (FileStream inputStream = new FileStream(inputPath, FileMode.Open, FileAccess.Read))
        using (FileStream outputStream = new FileStream(outputPath, FileMode.Create, FileAccess.Write))
        {
            // Load the PDF document using Aspose.Pdf.Document
            Aspose.Pdf.Document pdfDocument = new Aspose.Pdf.Document(inputStream);

            // Save the document to the output stream (PDF to PDF conversion)
            pdfDocument.Save(outputStream);
        }

        Console.WriteLine($"PDF successfully converted and saved to: {outputPath}");
    }
}