using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";
        const string xfdfPath = "annotations.xfdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF file not found: {pdfPath}");
            return;
        }

        if (!File.Exists(xfdfPath))
        {
            Console.Error.WriteLine($"XFDF file not found: {xfdfPath}");
            return;
        }

        using (FileStream pdfStream = new FileStream(pdfPath, FileMode.Open, FileAccess.Read))
        using (Document pdfDoc = new Document(pdfStream))
        using (FileStream xfdfStream = new FileStream(xfdfPath, FileMode.Open, FileAccess.Read))
        {
            pdfDoc.ImportAnnotationsFromXfdf(xfdfStream);
            pdfDoc.Save(outputPath);
        }

        Console.WriteLine($"Annotations imported and saved to '{outputPath}'.");
    }
}