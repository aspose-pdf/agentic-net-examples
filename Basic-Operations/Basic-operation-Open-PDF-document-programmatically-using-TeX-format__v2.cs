using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Paths to the TeX source file and the resulting PDF.
        const string texPath = "input.tex";
        const string pdfPath = "output.pdf";

        // Verify the TeX file exists.
        if (!File.Exists(texPath))
        {
            Console.Error.WriteLine($"TeX file not found: {texPath}");
            return;
        }

        // Configure loading options for TeX. Default options are sufficient for most cases.
        TeXLoadOptions loadOptions = new TeXLoadOptions();

        // Open the TeX file as a Document using the specified load options.
        using (Document doc = new Document(texPath, loadOptions))
        {
            // Save the generated PDF.
            doc.Save(pdfPath);
        }

        Console.WriteLine($"TeX file converted to PDF: {pdfPath}");
    }
}