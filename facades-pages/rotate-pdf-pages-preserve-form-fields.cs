using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "rotated_output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF document. The Document class preserves form fields and their values.
            Document pdfDocument = new Document(inputPath);

            // Rotate each page 90 degrees clockwise.
            // The Rotation enum values are: on0, on90, on180, on270.
            for (int i = 1; i <= pdfDocument.Pages.Count; i++)
            {
                pdfDocument.Pages[i].Rotate = Rotation.on90;
            }

            // Save the rotated PDF. Form fields remain intact and retain their data.
            pdfDocument.Save(outputPath);

            Console.WriteLine($"Pages rotated successfully. Output saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
