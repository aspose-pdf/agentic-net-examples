using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputDocx = "output.docx";
        const string imagesDir = "extracted_images";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        Directory.CreateDirectory(imagesDir);

        using (Document pdfDoc = new Document(inputPdf))
        {
            // Convert PDF to DOCX
            var docOptions = new DocSaveOptions
            {
                Format = DocSaveOptions.DocFormat.DocX
            };
            pdfDoc.Save(outputDocx, docOptions);
            Console.WriteLine($"PDF converted to DOCX: {outputDocx}");

            // Extract embedded images
            int imgCounter = 1;
            foreach (Page page in pdfDoc.Pages)
            {
                foreach (XImage img in page.Resources.Images)
                {
                    // Use a generic .png extension if original name is unavailable
                    string imgPath = Path.Combine(imagesDir, $"image_{imgCounter}.png");
                    // XImage.Save expects a Stream, not a file path string
                    using (FileStream fs = new FileStream(imgPath, FileMode.Create, FileAccess.Write))
                    {
                        img.Save(fs);
                    }
                    Console.WriteLine($"Saved image {imgCounter} to {imgPath}");
                    imgCounter++;
                }
            }
        }
    }
}
