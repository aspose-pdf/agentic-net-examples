using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "minutes.pdf";
        const string outputPath = "minutes_progress.pptx";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            using (Document pdfDoc = new Document(inputPath))
            {
                // Configure PPTX save options with a custom progress handler
                var pptxOptions = new PptxSaveOptions();

                // The CustomProgressHandler delegate receives a ProgressEventHandlerInfo object.
                // Use it to report conversion progress (e.g., TotalProgress events).
                pptxOptions.CustomProgressHandler = info =>
                {
                    if (info.EventType == ProgressEventType.TotalProgress)
                    {
                        Console.WriteLine($"Conversion progress: {info.Value}%");
                    }
                };

                // Save the PDF as PPTX while reporting progress
                pdfDoc.Save(outputPath, pptxOptions);
            }

            Console.WriteLine($"Conversion completed: {outputPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
