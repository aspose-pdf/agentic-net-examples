using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        string inputPdf = "input.pdf";
        string imagePath = "logo.png";
        string outputFile = "modified.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }
        if (!File.Exists(imagePath))
        {
            Console.Error.WriteLine($"Image file not found: {imagePath}");
            return;
        }

        // Initialize the facade and bind the source PDF
        PdfFileMend mend = new PdfFileMend();
        mend.BindPdf(inputPdf);

        // Add the image to page 1 at the specified rectangle (llx, lly, urx, ury)
        mend.AddImage(imagePath, 1, 100.0f, 500.0f, 300.0f, 700.0f);

        // Save the modified PDF into a memory stream
        MemoryStream outputStream = new MemoryStream();
        mend.Save(outputStream);
        outputStream.Position = 0; // Reset for reading

        // For demonstration purposes, write the memory stream to a file (simulating an HTTP response)
        using (FileStream fileStream = new FileStream(outputFile, FileMode.Create, FileAccess.Write))
        {
            outputStream.CopyTo(fileStream);
        }

        Console.WriteLine($"Modified PDF saved to '{outputFile}', size {outputStream.Length} bytes.");
    }
}
