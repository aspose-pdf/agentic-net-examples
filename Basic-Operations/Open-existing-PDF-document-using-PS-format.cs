using System;
using System.IO;
using Aspose.Pdf; // Document, PsLoadOptions, etc.

class Program
{
    static void Main()
    {
        const string psInputPath  = "input.ps";   // PostScript file to open
        const string pdfOutputPath = "output.pdf"; // Resulting PDF file

        if (!File.Exists(psInputPath))
        {
            Console.Error.WriteLine($"File not found: {psInputPath}");
            return;
        }

        try
        {
            // Load the PostScript file using PsLoadOptions.
            // The Document constructor with (string, LoadOptions) handles conversion to PDF.
            using (Document doc = new Document(psInputPath, new PsLoadOptions()))
            {
                // Save the converted document as PDF.
                doc.Save(pdfOutputPath);
            }

            Console.WriteLine($"Successfully converted '{psInputPath}' to PDF at '{pdfOutputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during conversion: {ex.Message}");
        }
    }
}