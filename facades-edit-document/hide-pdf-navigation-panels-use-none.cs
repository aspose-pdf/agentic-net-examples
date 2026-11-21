using System;
using System.IO;
using Aspose.Pdf;
using Microsoft.CSharp.RuntimeBinder;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF document
            Document pdfDocument = new Document(inputPath);

            // Attempt to set viewer preferences. The ViewerPreferences property
            // exists only in newer versions of Aspose.Pdf. To keep the code
            // compatible with older versions we access it via dynamic and handle
            // the possible RuntimeBinderException.
            dynamic doc = pdfDocument;
            try
            {
                doc.ViewerPreferences.PageMode = PageMode.UseNone;
            }
            catch (RuntimeBinderException)
            {
                Console.WriteLine("ViewerPreferences property is not supported by the current Aspose.Pdf library version. Skipping this setting.");
            }

            // Save the updated PDF
            pdfDocument.Save(outputPath);
            Console.WriteLine($"Viewer preference applied (if supported) and saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
