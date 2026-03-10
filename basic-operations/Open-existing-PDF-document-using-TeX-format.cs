using System;
using System.IO;
using Aspose.Pdf; // TeXLoadOptions and Document are in this namespace

class Program
{
    static void Main()
    {
        // Paths – adjust as needed
        const string texPath = "input.tex";
        const string pdfPath = "output.pdf";

        // Verify the TeX source exists
        if (!File.Exists(texPath))
        {
            Console.Error.WriteLine($"TeX file not found: {texPath}");
            return;
        }

        // Initialize load options for TeX conversion
        TeXLoadOptions texLoadOptions = new TeXLoadOptions();

        // Load the TeX file and convert it to a PDF document
        using (Document pdfDocument = new Document(texPath, texLoadOptions))
        {
            // Save the resulting PDF
            pdfDocument.Save(pdfPath);
        }

        Console.WriteLine($"TeX file '{texPath}' successfully converted to PDF '{pdfPath}'.");
    }
}