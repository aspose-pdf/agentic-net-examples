using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string headerImgPath = "header.png";
        const string outputPdfPath = "output.pdf";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }

        if (!File.Exists(headerImgPath))
        {
            Console.Error.WriteLine($"Header image not found: {headerImgPath}");
            return;
        }

        // Load the PDF document (binding with PdfFileStamp is not required for image stamps)
        Document doc;
        using (FileStream pdfStream = File.OpenRead(inputPdfPath))
        {
            doc = new Document(pdfStream);
        }

        // Create an ImageStamp from the header image stream
        ImageStamp headerStamp;
        using (FileStream imgStream = File.OpenRead(headerImgPath))
        {
            headerStamp = new ImageStamp(imgStream);
        }

        // Configure the stamp to act as a header (top margin = 5 points)
        headerStamp.HorizontalAlignment = HorizontalAlignment.Center;
        headerStamp.VerticalAlignment   = VerticalAlignment.Top;
        headerStamp.YIndent = 5; // top margin in points

        // Apply the stamp to every page of the document
        foreach (Page page in doc.Pages)
        {
            page.AddStamp(headerStamp);
        }

        // Save the modified PDF
        doc.Save(outputPdfPath);
        Console.WriteLine($"Header replaced and saved to '{outputPdfPath}'.");
    }
}
