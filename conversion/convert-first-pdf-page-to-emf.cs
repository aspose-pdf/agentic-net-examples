using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputEmf = "page1.emf";

        // Verify input file exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        try
        {
            // Load the PDF document inside a using block for deterministic disposal
            using (Document pdfDoc = new Document(inputPdf))
            {
                // Ensure the document has at least one page (Aspose.Pdf uses 1‑based indexing)
                if (pdfDoc.Pages.Count < 1)
                {
                    Console.Error.WriteLine("The PDF contains no pages.");
                    return;
                }

                // Create an EMF device with a desired resolution (e.g., 300 DPI)
                Resolution resolution = new Resolution(300);
                EmfDevice emfDevice = new EmfDevice(resolution);

                // Convert the first page (index 1) to EMF using the device
                emfDevice.Process(pdfDoc.Pages[1], outputEmf);

                Console.WriteLine($"First page saved as EMF image: {outputEmf}");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
