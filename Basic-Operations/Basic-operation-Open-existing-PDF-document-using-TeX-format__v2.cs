using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string texPath = "input.tex";
        const string pdfPath = "output.pdf";

        if (!File.Exists(texPath))
        {
            Console.Error.WriteLine($"File not found: {texPath}");
            return;
        }

        // Initialize TeX loading options (default settings)
        TeXLoadOptions texOptions = new TeXLoadOptions();

        // Load the TeX file into a PDF document using the specified options
        using (Document doc = new Document(texPath, texOptions))
        {
            // Save the resulting PDF
            doc.Save(pdfPath);
        }

        Console.WriteLine($"TeX file converted and saved as PDF at '{pdfPath}'.");
    }
}