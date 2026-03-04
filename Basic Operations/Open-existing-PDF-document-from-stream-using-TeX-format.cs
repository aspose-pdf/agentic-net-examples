using System;
using System.IO;
using Aspose.Pdf; // Aspose.Pdf namespace contains Document, TeXLoadOptions, etc.

class Program
{
    static void Main()
    {
        // Path to the source TeX file (could be any .tex file)
        const string texFilePath = "sample.tex";

        // Path where the resulting PDF will be saved
        const string pdfOutputPath = "output.pdf";

        // Verify that the source file exists
        if (!File.Exists(texFilePath))
        {
            Console.Error.WriteLine($"TeX source file not found: {texFilePath}");
            return;
        }

        // Open a FileStream for the TeX file. The stream will be passed to the Document constructor.
        using (FileStream texStream = File.OpenRead(texFilePath))
        {
            // Initialize TeXLoadOptions (default settings are sufficient for most cases)
            TeXLoadOptions texLoadOptions = new TeXLoadOptions();

            // Load the TeX content from the stream and convert it to a PDF Document.
            // The Document constructor that accepts (Stream, LoadOptions) is the correct way.
            using (Document pdfDocument = new Document(texStream, texLoadOptions))
            {
                // Save the resulting PDF to the specified output path.
                pdfDocument.Save(pdfOutputPath);
            }
        }

        Console.WriteLine($"TeX file '{texFilePath}' has been converted to PDF '{pdfOutputPath}'.");
    }
}