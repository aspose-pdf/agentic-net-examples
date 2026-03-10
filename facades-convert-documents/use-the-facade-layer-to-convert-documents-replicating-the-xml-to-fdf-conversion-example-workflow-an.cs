using System;
using System.IO;
using Aspose.Pdf;               // For PdfException
using Aspose.Pdf.Facades;      // Facade layer containing FormDataConverter

class Program
{
    static void Main()
    {
        const string xmlInputPath = "input.xml";
        const string fdfOutputPath = "output.fdf";

        // Verify that the source XML file exists
        if (!File.Exists(xmlInputPath))
        {
            Console.Error.WriteLine($"Error: XML file not found at '{xmlInputPath}'.");
            return;
        }

        try
        {
            // Open source XML stream and destination FDF stream inside using blocks
            // to guarantee deterministic disposal of resources.
            using (FileStream xmlStream = File.OpenRead(xmlInputPath))
            using (FileStream fdfStream = new FileStream(fdfOutputPath, FileMode.Create, FileAccess.Write))
            {
                // Perform the conversion using the facade method.
                // This static method does not require an instance of FormDataConverter.
                FormDataConverter.ConvertXmlToFdf(xmlStream, fdfStream);
            }

            Console.WriteLine($"Conversion completed successfully. FDF saved to '{fdfOutputPath}'.");
        }
        catch (PdfException pdfEx)          // Handles Aspose.Pdf specific errors
        {
            Console.Error.WriteLine($"Aspose.Pdf error: {pdfEx.Message}");
        }
        catch (Exception ex)               // Handles any other unexpected errors
        {
            Console.Error.WriteLine($"Unexpected error: {ex.Message}");
        }
    }
}