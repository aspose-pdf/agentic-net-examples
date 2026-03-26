using System;
using System.IO;
using System.Reflection;
using Aspose.Pdf;
using Aspose.Pdf.Devices;

class Program
{
    static void Main()
    {
        // Determine the folder where the executable resides – this works both in VS and when the app is published.
        string baseFolder = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        const string pdfFileName = "input.pdf";

        // Build the full path to the PDF file.
        string pdfPath = Path.Combine(baseFolder, pdfFileName);
        if (!File.Exists(pdfPath))
        {
            Console.WriteLine($"PDF file not found: {pdfPath}");
            return;
        }

        try
        {
            // Load the PDF document.
            using (Document pdfDocument = new Document(pdfPath))
            {
                // Set the resolution for rasterised parts of the EMF (vector data stays vector).
                Resolution resolution = new Resolution(300);
                // EmfDevice does not implement IDisposable – do not wrap it in a using block.
                EmfDevice emfDevice = new EmfDevice(resolution);

                // Convert each page to an EMF image.
                for (int pageNumber = 1; pageNumber <= pdfDocument.Pages.Count; pageNumber++)
                {
                    string emfPath = Path.Combine(baseFolder, $"page{pageNumber}.emf");
                    using (FileStream emfStream = new FileStream(emfPath, FileMode.Create, FileAccess.Write))
                    {
                        emfDevice.Process(pdfDocument.Pages[pageNumber], emfStream);
                    }
                }
            }

            Console.WriteLine("PDF successfully converted to EMF files.");
        }
        catch (InvalidPdfFileFormatException ex)
        {
            Console.WriteLine($"Unable to open PDF – the file header is invalid: {ex.Message}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Unexpected error: {ex.Message}");
        }
    }
}
