using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.docx";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the source PDF
            using (Aspose.Pdf.Document doc = new Aspose.Pdf.Document(inputPath))
            {
                // Prepare DOCX save options (standard recognition mode is default)
                Aspose.Pdf.DocSaveOptions saveOptions = new Aspose.Pdf.DocSaveOptions();
                // Uncomment the following line if an explicit mode is required:
                // saveOptions.RecognitionMode = Aspose.Pdf.DocSaveOptions.RecognitionMode.Standard;

                // Save as DOCX
                doc.Save(outputPath, saveOptions);
            }

            Console.WriteLine($"PDF successfully converted to DOCX: '{outputPath}'");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during conversion: {ex.Message}");
        }
    }
}