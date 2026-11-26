using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "presentation.pdf";
        const string outputPath = "presentation_progress.pptx";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF document
            using (Document pdfDoc = new Document(inputPath))
            {
                // PPTX save options (type is in Aspose.Pdf namespace)
                var pptxOptions = new PptxSaveOptions();

                // Assign a custom progress handler via the CustomProgressHandler property
                pptxOptions.CustomProgressHandler = info =>
                {
                    // Report total conversion progress; other event types can be handled as needed
                    if (info.EventType == ProgressEventType.TotalProgress)
                    {
                        Console.WriteLine($"Conversion progress: {info.Value}%");
                    }
                };

                // Convert and save to PPTX with progress reporting
                pdfDoc.Save(outputPath, pptxOptions);
            }

            Console.WriteLine($"Conversion completed: {outputPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during conversion: {ex.Message}");
        }
    }
}
