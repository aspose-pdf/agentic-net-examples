using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices;
using Aspose.Pdf.Text;

class PdfToJpegWithFontSubstitution
{
    static void Main()
    {
        // Input PDF, output folder and fallback font name
        const string inputPdfPath = "input.pdf";
        const string outputFolder = "OutputImages";
        const string fallbackFontName = "Arial";

        // Ensure output directory exists
        Directory.CreateDirectory(outputFolder);

        // If the input PDF does not exist, create a simple one for demonstration purposes
        if (!File.Exists(inputPdfPath))
        {
            using (Document sampleDoc = new Document())
            {
                Page page = sampleDoc.Pages.Add();
                // Add a paragraph with some text using the fallback font
                page.Paragraphs.Add(new TextFragment("Sample PDF created because 'input.pdf' was missing.")
                {
                    TextState = { Font = FontRepository.FindFont(fallbackFontName) }
                });
                sampleDoc.Save(inputPdfPath);
            }
        }

        // Load the source PDF
        using (Document srcDoc = new Document(inputPdfPath))
        {
            // Register a font substitution for any missing fonts
            FontRepository.Substitutions.Add(new SimpleFontSubstitution("MissingFont", fallbackFontName));

            // Iterate through each page and convert it to a JPEG image
            int pageNumber = 1;
            foreach (Page page in srcDoc.Pages)
            {
                string outputImagePath = Path.Combine(outputFolder, $"page_{pageNumber}.jpg");

                // Use Aspose's JpegDevice (does not rely on System.Drawing and avoids CA1416 warnings)
                // Quality is supplied via the constructor overload, not a settable property.
                JpegDevice jpegDevice = new JpegDevice(new Resolution(300), 90);

                using (MemoryStream imageStream = new MemoryStream())
                {
                    jpegDevice.Process(page, imageStream);
                    File.WriteAllBytes(outputImagePath, imageStream.ToArray());
                }

                pageNumber++;
            }
        }

        Console.WriteLine("Conversion completed.");
    }
}
