using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string dicomPath = "image.dcm";
        const string outputPdf = "output.pdf";

        if (!File.Exists(dicomPath))
        {
            Console.Error.WriteLine($"DICOM file not found: {dicomPath}");
            return;
        }

        try
        {
            // Document must be disposed deterministically
            using (Document doc = new Document())
            {
                // Add a new page to the document
                Page page = doc.Pages.Add();

                // Create an Aspose.Pdf.Image and specify the DICOM file type
                Aspose.Pdf.Image img = new Aspose.Pdf.Image();
                img.File = dicomPath;
                img.FileType = ImageFileType.Dicom; // important: set the file type explicitly

                // Add the image to the page's paragraph collection
                page.Paragraphs.Add(img);

                // Save the document as PDF (default format)
                doc.Save(outputPdf);
            }

            Console.WriteLine($"DICOM image successfully converted to PDF: {outputPdf}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during conversion: {ex.Message}");
        }
    }
}