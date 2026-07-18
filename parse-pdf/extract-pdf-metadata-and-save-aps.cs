using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputAps = "vector_output.aps";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        try
        {
            // Load the PDF document
            using (Document pdfDoc = new Document(inputPdf))
            {
                // Retrieve standard metadata
                string author = pdfDoc.Info.Author ?? "(none)";
                string title  = pdfDoc.Info.Title  ?? "(none)";

                Console.WriteLine($"Author: {author}");
                Console.WriteLine($"Title : {title}");

                // Export vector representation (APS format) for documentation
                ApsSaveOptions apsOptions = new ApsSaveOptions();
                // Optional: extract only OCR sublayer if needed
                // apsOptions.ExtractOcrSublayerOnly = false;

                pdfDoc.Save(outputAps, apsOptions);
                Console.WriteLine($"Vector data saved to: {outputAps}");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}