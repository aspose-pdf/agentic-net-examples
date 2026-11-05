using System;
using System.IO;
using Aspose.Pdf; // All SaveOptions and load options reside in this namespace

class Program
{
    static void Main()
    {
        const string xslFoPath = "input.fo";   // XSL‑FO source file
        const string pdfPath   = "output.pdf"; // Destination PDF file

        // Verify the source file exists
        if (!File.Exists(xslFoPath))
        {
            Console.Error.WriteLine($"Source file not found: {xslFoPath}");
            return;
        }

        // Load the XSL‑FO document using the dedicated load options
        // The XslFoLoadOptions class tells Aspose.Pdf to interpret the input as XSL‑FO
        using (Document doc = new Document(xslFoPath, new XslFoLoadOptions()))
        {
            // Save the document as PDF with default options (no explicit SaveOptions needed)
            doc.Save(pdfPath);
        }

        Console.WriteLine($"XSL‑FO successfully converted to PDF: {pdfPath}");
    }
}