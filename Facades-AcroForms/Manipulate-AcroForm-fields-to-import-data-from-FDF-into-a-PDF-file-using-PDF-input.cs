using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main(string[] args)
    {
        // Expected arguments: <input.pdf> <data.fdf> [output.pdf]
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: program <input.pdf> <data.fdf> [output.pdf]");
            return;
        }

        string pdfPath = args[0];
        string fdfPath = args[1];
        string outputPath = args.Length >= 3
            ? args[2]
            : Path.Combine(Path.GetDirectoryName(pdfPath) ?? "", "output.pdf");

        // Validate file existence
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"Error: PDF file not found: {pdfPath}");
            return;
        }

        if (!File.Exists(fdfPath))
        {
            Console.Error.WriteLine($"Error: FDF file not found: {fdfPath}");
            return;
        }

        try
        {
            // Load the existing PDF document
            Document pdfDocument = new Document(pdfPath);

            // Import field values from the FDF file
            using (FileStream fdfStream = File.OpenRead(fdfPath))
            {
                FdfReader.ReadAnnotations(fdfStream, pdfDocument);
            }

            // Save the updated PDF
            pdfDocument.Save(outputPath);
            Console.WriteLine($"PDF saved with imported data to: {outputPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}